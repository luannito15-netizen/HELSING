using System;
using UnityEngine;

namespace Helsing.Combat
{
    /// <summary>
    /// WORKING hitscan weapon. It owns cadence and damage application only: it does not read
    /// input, pick targets, or know about ammo, run state or rewards, so the Casull and the
    /// Jackal can diverge later without touching whoever pulls the trigger.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class HitscanWeapon : MonoBehaviour
    {
        [Header("TUNING / OPEN")]
        [SerializeField, Min(0f)] private float damage = 25f;
        [SerializeField, Min(0f)] private float range = 25f;
        [SerializeField, Min(0.01f)] private float fireInterval = 0.22f;

        [Header("Dependencies")]
        [Tooltip("Layers the shot can hit. Obstacles must be included so cover actually blocks.")]
        [SerializeField] private LayerMask hitMask = ~0;

        [Header("Development")]
        [SerializeField] private bool drawTracer = true;
        [SerializeField, Min(0f)] private float tracerDuration = 0.05f;

        private float nextFireTime;

        public float Damage => damage;
        public float Range => range;
        public float FireInterval => fireInterval;

        /// <summary>True when cadence allows another shot right now.</summary>
        public bool CanFire => Time.time >= nextFireTime;

        /// <summary>
        /// Raised for every shot actually fired, with the world origin and end point.
        /// Presentation subscribes here instead of living inside the weapon, so the tracer,
        /// muzzle flash or impact can be replaced without touching cadence or damage.
        /// </summary>
        public event Action<Vector3, Vector3> Fired;

        /// <summary>
        /// Fires along <paramref name="direction"/> if cadence allows. Returns false when the
        /// shot was refused by cooldown, so callers can tell "not yet" from "missed".
        /// </summary>
        public bool TryFire(Vector3 origin, Vector3 direction)
        {
            if (!CanFire)
            {
                return false;
            }

            direction.y = 0f;

            if (direction.sqrMagnitude < 0.0001f)
            {
                return false;
            }

            direction.Normalize();
            nextFireTime = Time.time + fireInterval;

            Vector3 endPoint = origin + direction * range;

            if (Physics.Raycast(origin, direction, out RaycastHit hit, range, hitMask, QueryTriggerInteraction.Ignore))
            {
                endPoint = hit.point;

                // The collider that stopped the ray may sit below the component holding Health.
                Health health = hit.collider.GetComponentInParent<Health>();

                if (health != null)
                {
                    health.TakeDamage(damage);
                }
            }

            if (drawTracer)
            {
                // Editor-only: Debug.DrawLine never renders in a player build. The visible
                // tracer in a build comes from a listener on Fired, not from this call.
                Debug.DrawLine(origin, endPoint, Color.red, tracerDuration);
            }

            Fired?.Invoke(origin, endPoint);

            return true;
        }

        private void OnValidate()
        {
            damage = Mathf.Max(0f, damage);
            range = Mathf.Max(0f, range);
            fireInterval = Mathf.Max(0.01f, fireInterval);
            tracerDuration = Mathf.Max(0f, tracerDuration);
        }
    }
}
