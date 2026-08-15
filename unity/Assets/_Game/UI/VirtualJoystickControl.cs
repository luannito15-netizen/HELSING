using Helsing.Input;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Helsing.UI
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(RectTransform))]
    public sealed class VirtualJoystickControl : MonoBehaviour,
        IPointerDownHandler,
        IDragHandler,
        IPointerUpHandler,
        ICancelHandler
    {
        private const int NoPointer = int.MinValue;

        [Header("Dependencies")]
        [SerializeField] private PlayerInputReader inputReader;
        [SerializeField] private RectTransform handle;

        [Tooltip("Joystick graphic that is repositioned under the thumb in dynamic mode. " +
                 "Leave empty to keep the stick fixed where it sits in the scene.")]
        [SerializeField] private RectTransform visualRoot;

        [Header("TUNING / OPEN")]
        [SerializeField, Min(1f)] private float movementRadius = 82f;
        [SerializeField, Range(0f, 0.95f)] private float deadZone = 0.15f;

        [Tooltip("WORKING — the stick is born wherever the thumb lands inside this area, " +
                 "instead of forcing the player to find a fixed square. Disable to restore " +
                 "the fixed stick.")]
        [SerializeField] private bool dynamicOrigin = true;

        [Tooltip("Hides the stick graphic while no finger is down. Only used in dynamic mode.")]
        [SerializeField] private bool hideWhenIdle = true;

        private RectTransform controlRect;
        private int activePointerId = NoPointer;
        private Vector2 pointerOrigin;

        public bool IsActive => activePointerId != NoPointer;

        private void Awake()
        {
            controlRect = (RectTransform)transform;
            ApplyIdleVisibility();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (activePointerId != NoPointer)
            {
                return;
            }

            activePointerId = eventData.pointerId;

            // The origin is captured before the first sample so the very first frame reads a
            // zero offset. Without this the stick would snap to full tilt the instant it is
            // touched anywhere off-centre of the area.
            if (dynamicOrigin && TryGetLocalPoint(eventData, out Vector2 localPoint))
            {
                pointerOrigin = localPoint;

                if (visualRoot != null)
                {
                    visualRoot.anchoredPosition = pointerOrigin;
                    visualRoot.gameObject.SetActive(true);
                }
            }
            else if (!dynamicOrigin)
            {
                pointerOrigin = Vector2.zero;
            }

            UpdatePointer(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (eventData.pointerId == activePointerId)
            {
                UpdatePointer(eventData);
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.pointerId == activePointerId)
            {
                ResetControl();
            }
        }

        public void OnCancel(BaseEventData eventData)
        {
            ResetControl();
        }

        public Vector2 EvaluateIntent(Vector2 localOffset)
        {
            Vector2 radialIntent = Vector2.ClampMagnitude(localOffset / Mathf.Max(1f, movementRadius), 1f);
            float magnitude = radialIntent.magnitude;

            if (magnitude <= deadZone)
            {
                return Vector2.zero;
            }

            float remappedMagnitude = Mathf.InverseLerp(deadZone, 1f, magnitude);
            return radialIntent.normalized * remappedMagnitude;
        }

        private bool TryGetLocalPoint(PointerEventData eventData, out Vector2 localPoint)
        {
            localPoint = Vector2.zero;

            return controlRect != null &&
                   RectTransformUtility.ScreenPointToLocalPointInRectangle(
                       controlRect,
                       eventData.position,
                       eventData.pressEventCamera,
                       out localPoint);
        }

        private void UpdatePointer(PointerEventData eventData)
        {
            if (!TryGetLocalPoint(eventData, out Vector2 localPoint))
            {
                return;
            }

            // Offset from where the thumb landed, not from the centre of the area, so a
            // dynamic stick tilts relative to its own origin.
            Vector2 offset = localPoint - pointerOrigin;
            Vector2 clampedOffset = Vector2.ClampMagnitude(offset, movementRadius);

            if (handle != null)
            {
                handle.anchoredPosition = clampedOffset;
            }

            inputReader?.SetVirtualMoveIntent(EvaluateIntent(offset));
        }

        private void ApplyIdleVisibility()
        {
            if (visualRoot != null && dynamicOrigin && hideWhenIdle)
            {
                visualRoot.gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// Releases the active pointer and zeroes the movement intent.
        /// Idempotent: calling it while no pointer is held is a no-op, so it never
        /// clears an intent this control does not own (for example the keyboard fallback).
        /// </summary>
        private void ResetControl()
        {
            if (activePointerId == NoPointer)
            {
                return;
            }

            activePointerId = NoPointer;
            pointerOrigin = Vector2.zero;

            if (handle != null)
            {
                handle.anchoredPosition = Vector2.zero;
            }

            ApplyIdleVisibility();

            inputReader?.ClearVirtualMoveIntent();
        }

        private void OnDisable()
        {
            ResetControl();
        }

        // The pointer-up event is not guaranteed when the application loses focus or is
        // suspended with a finger still down. Without these guards the intent stays latched
        // and the Player keeps moving after returning to the app.
        private void OnApplicationFocus(bool hasFocus)
        {
            if (!hasFocus)
            {
                ResetControl();
            }
        }

        private void OnApplicationPause(bool isPaused)
        {
            if (isPaused)
            {
                ResetControl();
            }
        }

        private void OnValidate()
        {
            movementRadius = Mathf.Max(1f, movementRadius);
            deadZone = Mathf.Clamp(deadZone, 0f, 0.95f);
        }
    }
}
