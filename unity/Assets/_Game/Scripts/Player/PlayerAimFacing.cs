using Helsing.Input;
using UnityEngine;

namespace Helsing.Player
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(PlayerInputReader))]
    public sealed class PlayerAimFacing : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private PlayerInputReader inputReader;
        [SerializeField] private Camera aimCamera;

        [Header("TUNING / OPEN")]
        [SerializeField, Min(0f)] private float rotationSpeed = 1080f;
        [SerializeField, Min(0f)] private float minimumAimDistance = 0.05f;
        [SerializeField, Min(0f)] private float manualAimReleaseHoldDuration = 0.25f;

        [Tooltip("WORKING — MOBA mobile reference: without a hovering pointer, facing follows " +
                 "movement while no manual aim is active. Disable to keep the last aim instead.")]
        [SerializeField] private bool faceMovementWhenNotAiming = true;

        private Vector3 aimDirection = Vector3.forward;
        private bool hasValidAim;
        private bool wasManualAimActive;
        private float pointerResumeTime;
        private bool hasReportedMissingCamera;

        public Vector3 AimDirection => aimDirection;
        public bool HasValidAim => hasValidAim;

        private void Awake()
        {
            inputReader ??= GetComponent<PlayerInputReader>();
            aimCamera ??= Camera.main;
            PreserveCurrentFacing();
        }

        private void OnEnable()
        {
            PreserveCurrentFacing();
            wasManualAimActive = false;
            pointerResumeTime = 0f;
        }

        private void Update()
        {
            UpdateAimIntent();
            RotateTowardsAim(Time.deltaTime);
        }

        private void UpdateAimIntent()
        {
            if (inputReader == null)
            {
                return;
            }

            bool isManualAimActive = inputReader.IsManualAimActive;

            if (isManualAimActive)
            {
                TrySetAimDirection(GetCameraRelativeAimDirection(inputReader.ManualAimIntent));
                pointerResumeTime = Time.unscaledTime + manualAimReleaseHoldDuration;
            }
            else
            {
                if (wasManualAimActive)
                {
                    pointerResumeTime = Time.unscaledTime + manualAimReleaseHoldDuration;
                }

                // The pointer fallback is desktop-only. Without this gate a touch build would
                // aim at the projection of screen (0,0) every frame, since there is no
                // hovering pointer to read.
                if (Time.unscaledTime >= pointerResumeTime)
                {
                    if (inputReader.HasPointerAim)
                    {
                        TrySetAimFromScreenPosition(inputReader.PointerPosition);
                    }
                    else if (faceMovementWhenNotAiming)
                    {
                        // Touch has no hover, so movement carries facing between drags — the
                        // MOBA mobile convention. A zero intent is rejected downstream, which
                        // preserves the last facing when the stick is released instead of
                        // snapping the Player back to a default direction.
                        TrySetAimDirection(GetCameraRelativeAimDirection(inputReader.MoveIntent));
                    }
                }
            }

            wasManualAimActive = isManualAimActive;
        }

        public bool TrySetAimFromScreenPosition(Vector2 screenPosition)
        {
            if (!HasAimCamera())
            {
                return false;
            }

            Ray pointerRay = aimCamera.ScreenPointToRay(screenPosition);
            Plane groundPlane = new Plane(Vector3.up, transform.position);

            if (!groundPlane.Raycast(pointerRay, out float distance) || distance < 0f)
            {
                return false;
            }

            return TrySetAimDirection(pointerRay.GetPoint(distance) - transform.position);
        }

        public bool TrySetAimDirection(Vector3 worldDirection)
        {
            worldDirection.y = 0f;

            if (worldDirection.sqrMagnitude < minimumAimDistance * minimumAimDistance)
            {
                return false;
            }

            aimDirection = worldDirection.normalized;
            hasValidAim = true;
            return true;
        }

        /// <summary>
        /// Reports a missing aim camera exactly once instead of failing silently every frame.
        /// The flag resets when a camera becomes available again, so a later loss is reported too.
        /// </summary>
        private bool HasAimCamera()
        {
            if (aimCamera != null)
            {
                hasReportedMissingCamera = false;
                return true;
            }

            if (!hasReportedMissingCamera)
            {
                hasReportedMissingCamera = true;
                Debug.LogError(
                    $"{nameof(PlayerAimFacing)} on '{name}' has no aim camera assigned and " +
                    $"'{nameof(Camera)}.main' resolved to null. Pointer aim is disabled and manual " +
                    "aim falls back to world-space axes until a camera is assigned.",
                    this);
            }

            return false;
        }

        private Vector3 GetCameraRelativeAimDirection(Vector2 screenDirection)
        {
            Vector3 forward = HasAimCamera() ? aimCamera.transform.forward : Vector3.forward;
            forward.y = 0f;

            if (forward.sqrMagnitude < 0.0001f)
            {
                forward = Vector3.forward;
            }

            forward.Normalize();
            Vector3 right = Vector3.Cross(Vector3.up, forward).normalized;
            return right * screenDirection.x + forward * screenDirection.y;
        }

        private void RotateTowardsAim(float deltaTime)
        {
            if (!hasValidAim || aimDirection.sqrMagnitude < 0.0001f)
            {
                return;
            }

            Quaternion targetRotation = Quaternion.LookRotation(aimDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                rotationSpeed * deltaTime);
        }

        private void PreserveCurrentFacing()
        {
            Vector3 currentFacing = transform.forward;
            currentFacing.y = 0f;

            if (currentFacing.sqrMagnitude < 0.0001f)
            {
                return;
            }

            aimDirection = currentFacing.normalized;
            hasValidAim = true;
        }

        private void Reset()
        {
            inputReader = GetComponent<PlayerInputReader>();
            aimCamera = Camera.main;
        }

        private void OnValidate()
        {
            rotationSpeed = Mathf.Max(0f, rotationSpeed);
            minimumAimDistance = Mathf.Max(0f, minimumAimDistance);
            manualAimReleaseHoldDuration = Mathf.Max(0f, manualAimReleaseHoldDuration);
        }
    }
}
