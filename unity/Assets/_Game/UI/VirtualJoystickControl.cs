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

        [Header("Dash — TUNING / OPEN")]
        [Tooltip("WORKING gesture: a flick of the thumb dashes in the direction it travelled. " +
                 "Speed in rect units per second that a drag must exceed to count as a swipe.")]
        [SerializeField, Min(1f)] private float swipeSpeedThreshold = 900f;

        [Tooltip("Minimum travel for a swipe, so a fast jitter in place never dashes.")]
        [SerializeField, Min(1f)] private float swipeMinDistance = 45f;

        [Tooltip("Idle time after a dash gesture before another can be recognised. Guards " +
                 "against one continuous flick registering several times.")]
        [SerializeField, Min(0f)] private float swipeRearmDelay = 0.25f;

        private RectTransform controlRect;
        private int activePointerId = NoPointer;
        private Vector2 pointerOrigin;
        private Vector2 lastSamplePoint;
        private float lastSampleTime;
        private float rearmTime;

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

            // The swipe baseline starts where the finger landed, so the very first drag
            // sample cannot be measured against a stale point from a previous gesture.
            if (TryGetLocalPoint(eventData, out Vector2 startPoint))
            {
                lastSamplePoint = startPoint;
            }

            lastSampleTime = Time.unscaledTime;

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
            Interrupt();
        }

        /// <summary>
        /// Full reset for interrupted gestures. Unlike a normal pointer-up it also drops a
        /// pending dash and forgets the first tap, so returning to the app cannot complete a
        /// double tap that started before leaving it.
        /// </summary>
        private void Interrupt()
        {
            rearmTime = 0f;
            lastSampleTime = 0f;
            inputReader?.ClearDashRequest();
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

        /// <summary>
        /// Requests a dash when the thumb flicks: fast enough and far enough in one direction.
        /// Both tests are needed — speed alone fires on a jitter in place, distance alone
        /// fires on any ordinary run across the pad.
        ///
        /// The direction travels with the request because a flick often ends before the dash
        /// is consumed, and the stick has zeroed by then.
        /// </summary>
        private void DetectSwipe(Vector2 localPoint)
        {
            float now = Time.unscaledTime;
            float dt = now - lastSampleTime;

            if (dt <= 0.0001f)
            {
                return;
            }

            Vector2 delta = localPoint - lastSamplePoint;
            float travelled = delta.magnitude;
            lastSamplePoint = localPoint;
            lastSampleTime = now;

            if (now < rearmTime)
            {
                return;
            }

            if (travelled < swipeMinDistance || travelled / dt < swipeSpeedThreshold)
            {
                return;
            }

            inputReader?.RequestDash(delta / travelled);
            rearmTime = now + swipeRearmDelay;
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

            DetectSwipe(localPoint);

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
                Interrupt();
            }
        }

        private void OnApplicationPause(bool isPaused)
        {
            if (isPaused)
            {
                Interrupt();
            }
        }

        private void OnValidate()
        {
            movementRadius = Mathf.Max(1f, movementRadius);
            deadZone = Mathf.Clamp(deadZone, 0f, 0.95f);
        }
    }
}
