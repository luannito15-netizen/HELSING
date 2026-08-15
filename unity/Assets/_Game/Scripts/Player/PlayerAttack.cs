using Helsing.Combat;
using Helsing.Input;
using UnityEngine;

namespace Helsing.Player
{
    /// <summary>
    /// Turns attack intents into shots. It is the only place that knows the approved
    /// contract "tap uses auto-target, drag uses manual aim": the UI only reports gestures,
    /// the selector only ranks targets and the weapon only fires, so any of the three can be
    /// replaced without moving that rule.
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(PlayerInputReader))]
    public sealed class PlayerAttack : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private PlayerInputReader inputReader;
        [SerializeField] private AutoTargetSelector targetSelector;
        [SerializeField] private HitscanWeapon weapon;

        [Header("TUNING / OPEN")]
        [Tooltip("Height the shot leaves from, above the pivot.")]
        [SerializeField] private float muzzleHeightOffset = 1f;

        private Vector3 Origin => transform.position + Vector3.up * muzzleHeightOffset;

        private void Awake()
        {
            inputReader ??= GetComponent<PlayerInputReader>();
            targetSelector ??= GetComponent<AutoTargetSelector>();
            weapon ??= GetComponent<HitscanWeapon>();
        }

        private void Update()
        {
            if (inputReader == null || weapon == null)
            {
                return;
            }

            // A tap is consumed even while a sustained burst is running, so a queued gesture
            // never leaks into the next frame and fires by surprise.
            bool tapped = inputReader.ConsumeAttackTap();

            if (inputReader.IsManualAimActive)
            {
                weapon.TryFire(Origin, transform.forward);
                return;
            }

            if (tapped)
            {
                FireAtAutoTarget();
            }
        }

        /// <summary>
        /// Fires at the best target in the cone, or straight ahead when the cone is empty.
        /// Shooting anyway is deliberate: a tap that silently does nothing reads as a broken
        /// control, and the shot itself tells the player there was no target.
        /// </summary>
        private void FireAtAutoTarget()
        {
            Vector3 origin = Origin;
            Vector3 direction = transform.forward;

            if (targetSelector != null)
            {
                Targetable target = targetSelector.SelectTarget(transform.forward);

                if (target != null)
                {
                    direction = target.AimPoint - origin;
                }
            }

            weapon.TryFire(origin, direction);
        }

        private void Reset()
        {
            inputReader = GetComponent<PlayerInputReader>();
            targetSelector = GetComponent<AutoTargetSelector>();
            weapon = GetComponent<HitscanWeapon>();
        }
    }
}
