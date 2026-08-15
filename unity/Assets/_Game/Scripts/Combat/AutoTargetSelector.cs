using UnityEngine;

namespace Helsing.Combat
{
    /// <summary>
    /// WORKING first pass at auto-target: prefers a live target inside a forward cone,
    /// scoring angle and distance together. The final algorithm is OPEN — this exists to
    /// make tap-to-attack testable, not to fix target selection.
    ///
    /// Selection is on demand, never per frame: the attack asks for a target when it fires,
    /// so nothing scans or allocates while the player is only moving.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class AutoTargetSelector : MonoBehaviour
    {
        [Header("TUNING / OPEN")]
        [SerializeField, Min(0f)] private float maxRange = 12f;
        [SerializeField, Range(1f, 180f)] private float maxAngle = 60f;
        [SerializeField] private float originHeightOffset = 1f;

        [Tooltip("Relative importance of aiming accuracy versus proximity when scoring.")]
        [SerializeField, Range(0f, 1f)] private float angleWeight = 0.7f;

        [Header("Development")]
        [SerializeField] private bool drawGizmos = true;

        private Targetable selectedTarget;

        public Vector3 Origin => transform.position + Vector3.up * originHeightOffset;

        /// <summary>
        /// The last selected target, or null once it dies or is disabled. Callers never have
        /// to re-check liveness themselves.
        /// </summary>
        public Targetable CurrentTarget =>
            selectedTarget != null && selectedTarget.IsValidTarget ? selectedTarget : null;

        /// <summary>
        /// Picks the best target in front of <paramref name="forward"/>, or null when the
        /// cone is empty. A null result is a valid outcome — the caller decides whether that
        /// means "do not fire" or "fire straight ahead".
        /// </summary>
        public Targetable SelectTarget(Vector3 forward)
        {
            forward.y = 0f;

            if (forward.sqrMagnitude < 0.0001f)
            {
                selectedTarget = null;
                return null;
            }

            forward.Normalize();
            Vector3 origin = Origin;

            Targetable bestTarget = null;
            float bestScore = float.MaxValue;

            for (int i = 0; i < Targetable.Registered.Count; i++)
            {
                Targetable candidate = Targetable.Registered[i];

                if (candidate == null || !candidate.IsValidTarget || IsSelf(candidate))
                {
                    continue;
                }

                Vector3 toTarget = candidate.AimPoint - origin;
                toTarget.y = 0f;

                float distance = toTarget.magnitude;

                if (distance > maxRange || distance < 0.0001f)
                {
                    continue;
                }

                float angle = Vector3.Angle(forward, toTarget / distance);

                if (angle > maxAngle)
                {
                    continue;
                }

                float score = (angle / maxAngle) * angleWeight
                            + (distance / maxRange) * (1f - angleWeight);

                if (score < bestScore)
                {
                    bestScore = score;
                    bestTarget = candidate;
                }
            }

            selectedTarget = bestTarget;
            return bestTarget;
        }

        public void ClearTarget()
        {
            selectedTarget = null;
        }

        private bool IsSelf(Targetable candidate)
        {
            return candidate.transform == transform || candidate.transform.IsChildOf(transform);
        }

        private void OnValidate()
        {
            maxRange = Mathf.Max(0f, maxRange);
            maxAngle = Mathf.Clamp(maxAngle, 1f, 180f);
            angleWeight = Mathf.Clamp01(angleWeight);
        }

        private void OnDrawGizmosSelected()
        {
            if (!drawGizmos)
            {
                return;
            }

            Vector3 origin = Origin;
            Vector3 forward = transform.forward;
            forward.y = 0f;

            if (forward.sqrMagnitude < 0.0001f)
            {
                return;
            }

            forward.Normalize();

            Gizmos.color = new Color(1f, 1f, 1f, 0.25f);
            Gizmos.DrawWireSphere(origin, maxRange);

            Gizmos.color = Color.yellow;
            Vector3 leftEdge = Quaternion.AngleAxis(-maxAngle, Vector3.up) * forward;
            Vector3 rightEdge = Quaternion.AngleAxis(maxAngle, Vector3.up) * forward;
            Gizmos.DrawLine(origin, origin + leftEdge * maxRange);
            Gizmos.DrawLine(origin, origin + rightEdge * maxRange);

            Targetable target = CurrentTarget;

            if (target != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(origin, target.AimPoint);
                Gizmos.DrawWireSphere(target.AimPoint, 0.25f);
            }
        }
    }
}
