using Helsing.Input;
using UnityEngine;

namespace Helsing.Player
{
    /// <summary>
    /// WORKING dash. Direction comes from the movement stick, per the approved rule that the
    /// left control only ever says where the Alucard goes — so dashing away while aiming
    /// forward stays possible, exactly like backing up while shooting.
    ///
    /// The double tap that triggers it is a test gesture, not a closed decision: a dedicated
    /// button remains the likely production control once skills and Liberação need HUD space.
    ///
    /// It costs nothing. Blood, Souls and any resource cost are economy decisions and stay OPEN.
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(PlayerInputReader))]
    [RequireComponent(typeof(CharacterController))]
    public sealed class DashController : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private PlayerInputReader inputReader;
        [SerializeField] private CharacterController characterController;
        [SerializeField] private Camera directionCamera;

        [Tooltip("Disabled for the duration of the dash so normal movement does not fight it.")]
        [SerializeField] private MonoBehaviour movementToSuspend;

        [Header("TUNING / OPEN")]
        [SerializeField, Min(0.01f)] private float distance = 4.5f;
        [SerializeField, Min(0.01f)] private float duration = 0.18f;
        [SerializeField, Min(0f)] private float cooldown = 0.9f;
        [SerializeField, Min(0f)] private float gravity = -25f;

        private float dashEndTime;
        private float nextDashTime;
        private Vector3 dashDirection;
        private bool isDashing;

        public bool IsDashing => isDashing;

        /// <summary>0 while ready, 1 right after a dash. For a future HUD indicator.</summary>
        public float CooldownFraction => cooldown <= 0f
            ? 0f
            : Mathf.Clamp01((nextDashTime - Time.time) / cooldown);

        private void Awake()
        {
            inputReader ??= GetComponent<PlayerInputReader>();
            characterController ??= GetComponent<CharacterController>();
            directionCamera ??= Camera.main;
        }

        private void OnDisable()
        {
            StopDash();
        }

        private void Update()
        {
            if (isDashing)
            {
                TickDash();
                return;
            }

            if (inputReader == null)
            {
                return;
            }

            bool requested = inputReader.ConsumeDash();

            // The request is consumed even while on cooldown, so a refused dash never sits
            // queued and fires by surprise once the cooldown ends.
            if (requested && Time.time >= nextDashTime)
            {
                StartDash();
            }
        }

        private void StartDash()
        {
            dashDirection = ResolveDirection();

            if (dashDirection.sqrMagnitude < 0.0001f)
            {
                return;
            }

            isDashing = true;
            dashEndTime = Time.time + duration;
            nextDashTime = Time.time + cooldown;

            if (movementToSuspend != null)
            {
                movementToSuspend.enabled = false;
            }
        }

        private void TickDash()
        {
            if (Time.time >= dashEndTime)
            {
                StopDash();
                return;
            }

            if (characterController == null)
            {
                StopDash();
                return;
            }

            float speed = distance / Mathf.Max(0.01f, duration);
            Vector3 step = dashDirection * (speed * Time.deltaTime);

            // Gravity is kept during the dash so it never becomes a way to hover off a ledge.
            step.y = gravity * Time.deltaTime;

            characterController.Move(step);
        }

        private void StopDash()
        {
            if (!isDashing)
            {
                return;
            }

            isDashing = false;

            if (movementToSuspend != null)
            {
                movementToSuspend.enabled = true;
            }
        }

        /// <summary>
        /// Stick direction in camera space, falling back to the current facing when the stick
        /// is idle — a standing dash still has to go somewhere.
        /// </summary>
        private Vector3 ResolveDirection()
        {
            // Direction captured with the gesture wins: a flick often ends before this runs,
            // and the live stick has already returned to zero by then.
            Vector2 intent = inputReader != null ? inputReader.DashDirection : Vector2.zero;

            if (intent.sqrMagnitude < 0.0001f && inputReader != null)
            {
                intent = inputReader.MoveIntent;
            }

            if (intent.sqrMagnitude < 0.0001f)
            {
                Vector3 facing = transform.forward;
                facing.y = 0f;
                return facing.sqrMagnitude < 0.0001f ? Vector3.zero : facing.normalized;
            }

            Vector3 forward = directionCamera != null ? directionCamera.transform.forward : Vector3.forward;
            forward.y = 0f;

            if (forward.sqrMagnitude < 0.0001f)
            {
                forward = Vector3.forward;
            }

            forward.Normalize();
            Vector3 right = Vector3.Cross(Vector3.up, forward).normalized;
            Vector3 direction = right * intent.x + forward * intent.y;
            direction.y = 0f;

            return direction.sqrMagnitude < 0.0001f ? Vector3.zero : direction.normalized;
        }

        private void Reset()
        {
            inputReader = GetComponent<PlayerInputReader>();
            characterController = GetComponent<CharacterController>();
            directionCamera = Camera.main;
        }

        private void OnValidate()
        {
            distance = Mathf.Max(0.01f, distance);
            duration = Mathf.Max(0.01f, duration);
            cooldown = Mathf.Max(0f, cooldown);
        }
    }
}
