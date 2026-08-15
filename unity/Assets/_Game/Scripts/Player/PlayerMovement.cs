using Helsing.Input;
using UnityEngine;

namespace Helsing.Player
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(PlayerInputReader))]
    public sealed class PlayerMovement : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private PlayerInputReader inputReader;
        [Tooltip("Optional transform used only as a planar movement basis. The movement motor does not read camera settings.")]
        [SerializeField] private Transform movementReference;

        [Header("TUNING / OPEN")]
        [SerializeField, Min(0f)] private float moveSpeed = 6f;
        [SerializeField, Min(0f)] private float acceleration = 32f;
        [SerializeField, Min(0f)] private float deceleration = 40f;
        [SerializeField] private float gravity = -25f;
        [SerializeField] private float groundedVelocity = -2f;

        private CharacterController characterController;
        private Vector3 planarVelocity;
        private float verticalVelocity;

        public Vector3 PlanarVelocity => planarVelocity;
        public bool IsGrounded => characterController != null && characterController.isGrounded;

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
            inputReader ??= GetComponent<PlayerInputReader>();
        }

        private void Update()
        {
            Tick(Time.deltaTime);
        }

        private void Tick(float deltaTime)
        {
            Vector2 moveIntent = inputReader != null ? inputReader.MoveIntent : Vector2.zero;
            Vector3 desiredDirection = GetWorldDirection(moveIntent);
            Vector3 desiredVelocity = desiredDirection * (moveSpeed * Mathf.Clamp01(moveIntent.magnitude));
            float response = desiredVelocity.sqrMagnitude > planarVelocity.sqrMagnitude ? acceleration : deceleration;

            planarVelocity = Vector3.MoveTowards(planarVelocity, desiredVelocity, response * deltaTime);

            if (characterController.isGrounded && verticalVelocity < 0f)
            {
                verticalVelocity = groundedVelocity;
            }

            verticalVelocity += gravity * deltaTime;
            CollisionFlags collisionFlags = characterController.Move(
                (planarVelocity + Vector3.up * verticalVelocity) * deltaTime);

            if ((collisionFlags & CollisionFlags.Below) != 0 && verticalVelocity < 0f)
            {
                verticalVelocity = groundedVelocity;
            }
        }

        private Vector3 GetWorldDirection(Vector2 moveIntent)
        {
            Vector3 forward = movementReference != null ? movementReference.forward : Vector3.forward;
            forward.y = 0f;

            if (forward.sqrMagnitude < 0.0001f)
            {
                forward = Vector3.forward;
            }

            forward.Normalize();
            Vector3 right = Vector3.Cross(Vector3.up, forward).normalized;
            Vector3 direction = right * moveIntent.x + forward * moveIntent.y;

            return direction.sqrMagnitude > 1f ? direction.normalized : direction;
        }

        private void Reset()
        {
            inputReader = GetComponent<PlayerInputReader>();
        }

        private void OnValidate()
        {
            moveSpeed = Mathf.Max(0f, moveSpeed);
            acceleration = Mathf.Max(0f, acceleration);
            deceleration = Mathf.Max(0f, deceleration);
            gravity = Mathf.Min(0f, gravity);
            groundedVelocity = Mathf.Min(0f, groundedVelocity);
        }
    }
}
