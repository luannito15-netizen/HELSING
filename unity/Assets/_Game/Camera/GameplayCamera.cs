using UnityEngine;

namespace Helsing.Presentation
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(UnityEngine.Camera))]
    public sealed class GameplayCamera : MonoBehaviour
    {
        [Header("Dependency")]
        [SerializeField] private Transform target;

        [Header("TUNING / OPEN")]
        [SerializeField, Min(0.1f)] private float orbitalDistance = 14f;
        [SerializeField] private float heightOffset;
        [SerializeField, Range(25f, 80f)] private float pitch = 55f;
        [SerializeField, Range(-180f, 180f)] private float yaw = 45f;
        [SerializeField, Range(25f, 70f)] private float fieldOfView = 40f;
        [SerializeField, Min(0f)] private float dampingTime = 0.18f;
        [SerializeField] private Vector3 targetOffset = new Vector3(0f, 1f, 0f);
        [Tooltip("Camera-local composition adjustment applied after the orbital position is calculated.")]
        [SerializeField] private Vector3 compositionOffset;

        private UnityEngine.Camera cameraComponent;
        private Vector3 followVelocity;

        private void Awake()
        {
            cameraComponent = GetComponent<UnityEngine.Camera>();
            ApplyLens();
        }

        private void OnEnable()
        {
            SnapToTarget();
        }

        private void LateUpdate()
        {
            if (target == null)
            {
                return;
            }

            Quaternion fixedRotation = Quaternion.Euler(pitch, yaw, 0f);
            Vector3 desiredPosition = CalculateDesiredPosition(fixedRotation);

            transform.position = dampingTime <= 0f
                ? desiredPosition
                : Vector3.SmoothDamp(
                    transform.position,
                    desiredPosition,
                    ref followVelocity,
                    dampingTime,
                    Mathf.Infinity,
                    Time.deltaTime);
            transform.rotation = fixedRotation;
        }

        public void SetTarget(Transform newTarget, bool snap = true)
        {
            target = newTarget;

            if (snap)
            {
                SnapToTarget();
            }
        }

        private Vector3 CalculateDesiredPosition(Quaternion fixedRotation)
        {
            Vector3 anchor = target.position + targetOffset;
            Vector3 orbit = fixedRotation * (Vector3.back * orbitalDistance);
            Vector3 localComposition = fixedRotation * compositionOffset;
            return anchor + orbit + Vector3.up * heightOffset + localComposition;
        }

        private void SnapToTarget()
        {
            if (target == null)
            {
                return;
            }

            Quaternion fixedRotation = Quaternion.Euler(pitch, yaw, 0f);
            transform.SetPositionAndRotation(CalculateDesiredPosition(fixedRotation), fixedRotation);
            followVelocity = Vector3.zero;
        }

        private void ApplyLens()
        {
            cameraComponent ??= GetComponent<UnityEngine.Camera>();

            if (cameraComponent == null)
            {
                return;
            }

            cameraComponent.orthographic = false;
            cameraComponent.fieldOfView = fieldOfView;
        }

        private void OnValidate()
        {
            orbitalDistance = Mathf.Max(0.1f, orbitalDistance);
            dampingTime = Mathf.Max(0f, dampingTime);
            ApplyLens();
        }
    }
}
