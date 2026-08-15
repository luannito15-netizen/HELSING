using UnityEngine;

namespace Helsing.Combat
{
    /// <summary>
    /// WORKING first pass at the Ghoul: idle until the Player is close, then chase and hit on
    /// a cadence. Deliberately dumb — no pathfinding, no group behaviour, no aggro sharing.
    /// It exists so the sandbox can answer "can the Player be killed", which is half of the
    /// P1 gate. The real enemy families, senses and encounter design remain OPEN.
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Health))]
    public sealed class EnemyChaseAttack : MonoBehaviour
    {
        public enum State
        {
            Idle,
            Chasing,
            Attacking,
        }

        [Header("Dependencies")]
        [SerializeField] private Health health;

        [Tooltip("Leave empty to resolve the Player by tag on first use.")]
        [SerializeField] private Transform target;

        [Header("TUNING / OPEN")]
        [SerializeField, Min(0f)] private float detectionRange = 14f;
        [SerializeField, Min(0f)] private float attackRange = 2.2f;
        [SerializeField, Min(0f)] private float moveSpeed = 2.6f;
        [SerializeField, Min(0f)] private float turnSpeed = 540f;
        [SerializeField, Min(0f)] private float damage = 12f;
        [SerializeField, Min(0.05f)] private float attackInterval = 1.2f;

        [Tooltip("Delay between starting the swing and the damage landing, so the hit is " +
                 "readable and can be avoided instead of being instant.")]
        [SerializeField, Min(0f)] private float attackWindUp = 0.35f;

        private State currentState = State.Idle;
        private float nextAttackTime;
        private float pendingHitTime;
        private bool hasPendingHit;
        private Health targetHealth;

        public State CurrentState => currentState;

        private void Awake()
        {
            health ??= GetComponent<Health>();
        }

        private void OnEnable()
        {
            currentState = State.Idle;
            hasPendingHit = false;
            nextAttackTime = 0f;
        }

        private void Update()
        {
            if (health != null && !health.IsAlive)
            {
                currentState = State.Idle;
                hasPendingHit = false;
                return;
            }

            if (!TryResolveTarget())
            {
                currentState = State.Idle;
                return;
            }

            Vector3 toTarget = target.position - transform.position;
            toTarget.y = 0f;
            float distance = toTarget.magnitude;

            ResolvePendingHit(distance);

            if (distance > detectionRange)
            {
                currentState = State.Idle;
                return;
            }

            FaceTarget(toTarget, distance);

            if (distance > attackRange)
            {
                currentState = State.Chasing;
                transform.position += toTarget.normalized * (moveSpeed * Time.deltaTime);
                return;
            }

            currentState = State.Attacking;
            TryStartAttack();
        }

        private void TryStartAttack()
        {
            if (Time.time < nextAttackTime || hasPendingHit)
            {
                return;
            }

            nextAttackTime = Time.time + attackInterval;
            pendingHitTime = Time.time + attackWindUp;
            hasPendingHit = true;
        }

        /// <summary>
        /// Lands a swing that already started. The range is re-checked at impact, so stepping
        /// out during the wind-up actually avoids the hit instead of it being guaranteed the
        /// moment the attack began.
        /// </summary>
        private void ResolvePendingHit(float distance)
        {
            if (!hasPendingHit || Time.time < pendingHitTime)
            {
                return;
            }

            hasPendingHit = false;

            if (distance <= attackRange && targetHealth != null && targetHealth.IsAlive)
            {
                targetHealth.TakeDamage(damage);
            }
        }

        private void FaceTarget(Vector3 toTarget, float distance)
        {
            if (distance < 0.001f)
            {
                return;
            }

            Quaternion desired = Quaternion.LookRotation(toTarget / distance, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation, desired, turnSpeed * Time.deltaTime);
        }

        private bool TryResolveTarget()
        {
            if (target == null)
            {
                GameObject found = GameObject.FindGameObjectWithTag("Player");

                if (found == null)
                {
                    return false;
                }

                target = found.transform;
                targetHealth = null;
            }

            if (targetHealth == null)
            {
                targetHealth = target.GetComponentInParent<Health>();
            }

            return true;
        }

        private void OnValidate()
        {
            detectionRange = Mathf.Max(0f, detectionRange);
            attackRange = Mathf.Clamp(attackRange, 0f, detectionRange);
            moveSpeed = Mathf.Max(0f, moveSpeed);
            damage = Mathf.Max(0f, damage);
            attackInterval = Mathf.Max(0.05f, attackInterval);
            attackWindUp = Mathf.Clamp(attackWindUp, 0f, attackInterval);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(1f, 0.5f, 0f, 0.35f);
            Gizmos.DrawWireSphere(transform.position, detectionRange);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
}
