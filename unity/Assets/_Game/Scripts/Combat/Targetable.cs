using System.Collections.Generic;
using UnityEngine;

namespace Helsing.Combat
{
    /// <summary>
    /// Marks something auto-target is allowed to select, and keeps the set of live
    /// candidates in one place. Targets register themselves instead of being discovered,
    /// so selection never scans the whole scene while the player is shooting.
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Health))]
    public sealed class Targetable : MonoBehaviour
    {
        private static readonly List<Targetable> RegisteredTargets = new List<Targetable>();

        public static IReadOnlyList<Targetable> Registered => RegisteredTargets;

        [Header("Dependencies")]
        [SerializeField] private Health health;

        [Header("TUNING / OPEN")]
        [Tooltip("Height above the pivot that weapons and targeting aim at.")]
        [SerializeField] private float aimHeightOffset = 1f;

        public Health Health => health;

        public Vector3 AimPoint => transform.position + Vector3.up * aimHeightOffset;

        /// <summary>
        /// A target is only valid while it is enabled and alive. Corpses must never be
        /// selected, which is the documented rule for auto-target.
        /// </summary>
        public bool IsValidTarget => isActiveAndEnabled && health != null && health.IsAlive;

        /// <summary>
        /// Entering Play Mode with domain reload disabled preserves statics, which would
        /// leave destroyed targets in the registry. Clearing on subsystem registration keeps
        /// the list honest in both reload modes.
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ClearRegistry()
        {
            RegisteredTargets.Clear();
        }

        private void Awake()
        {
            health ??= GetComponent<Health>();
        }

        private void OnEnable()
        {
            if (!RegisteredTargets.Contains(this))
            {
                RegisteredTargets.Add(this);
            }
        }

        private void OnDisable()
        {
            RegisteredTargets.Remove(this);
        }

        private void Reset()
        {
            health = GetComponent<Health>();
        }
    }
}
