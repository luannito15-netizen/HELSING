using Helsing.Input;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Helsing.UI
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(RectTransform))]
    public sealed class ManualAimDragControl : MonoBehaviour,
        IPointerDownHandler,
        IDragHandler,
        IPointerUpHandler,
        ICancelHandler
    {
        private const int NoPointer = int.MinValue;

        [Header("Dependencies")]
        [SerializeField] private PlayerInputReader inputReader;
        [SerializeField] private RectTransform visualRoot;
        [SerializeField] private RectTransform visualHandle;

        [Header("TUNING / OPEN")]
        [SerializeField, Min(0f)] private float dragThreshold = 24f;
        [SerializeField, Min(1f)] private float visualRadius = 92f;

        private RectTransform interactionRect;
        private Vector2 dragOrigin;
        private int activePointerId = NoPointer;
        private bool isDragging;

        public bool IsDragging => isDragging;

        private void Awake()
        {
            interactionRect = (RectTransform)transform;
            SetVisualActive(false);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (activePointerId != NoPointer || !TryGetLocalPoint(eventData, out dragOrigin))
            {
                return;
            }

            activePointerId = eventData.pointerId;
            isDragging = false;

            if (visualRoot != null)
            {
                visualRoot.anchoredPosition = dragOrigin;
            }

            if (visualHandle != null)
            {
                visualHandle.anchoredPosition = Vector2.zero;
            }

            SetVisualActive(true);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (eventData.pointerId != activePointerId || !TryGetLocalPoint(eventData, out Vector2 localPoint))
            {
                return;
            }

            Vector2 dragDelta = localPoint - dragOrigin;

            if (visualHandle != null)
            {
                visualHandle.anchoredPosition = Vector2.ClampMagnitude(dragDelta, visualRadius);
            }

            Vector2 aimIntent = EvaluateAimIntent(dragDelta);

            if (!isDragging && aimIntent.sqrMagnitude > 0f)
            {
                isDragging = true;
            }

            if (isDragging && aimIntent.sqrMagnitude > 0f)
            {
                inputReader?.SetManualAimIntent(aimIntent);
            }
        }

        /// <summary>
        /// A press that ends without ever crossing the drag threshold is a tap, which the
        /// primary attack uses for auto-target. Only a real pointer-up counts: cancel, focus
        /// loss and pause go straight to <see cref="ResetControl"/>, so an interrupted press
        /// never turns into a shot the player did not ask for.
        /// </summary>
        public void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.pointerId != activePointerId)
            {
                return;
            }

            if (!isDragging)
            {
                inputReader?.RequestAttackTap();
            }

            ResetControl();
        }

        public void OnCancel(BaseEventData eventData)
        {
            ResetControl();
        }

        public Vector2 EvaluateAimIntent(Vector2 dragDelta)
        {
            return dragDelta.sqrMagnitude < dragThreshold * dragThreshold
                ? Vector2.zero
                : dragDelta.normalized;
        }

        private bool TryGetLocalPoint(PointerEventData eventData, out Vector2 localPoint)
        {
            localPoint = Vector2.zero;
            return interactionRect != null && RectTransformUtility.ScreenPointToLocalPointInRectangle(
                interactionRect,
                eventData.position,
                eventData.pressEventCamera,
                out localPoint);
        }

        /// <summary>
        /// Releases the active pointer and hands aim control back to the pointer fallback.
        /// Idempotent: with no pointer held it is a no-op, so it never clears a manual aim
        /// this control does not own.
        /// </summary>
        private void ResetControl()
        {
            if (activePointerId == NoPointer && !isDragging)
            {
                return;
            }

            // Clearing is unconditional once a pointer was held: leaving isManualAimActive
            // latched would block the mouse fallback in PlayerAimFacing indefinitely.
            inputReader?.ClearManualAimIntent();

            activePointerId = NoPointer;
            isDragging = false;
            SetVisualActive(false);
        }

        private void SetVisualActive(bool isActive)
        {
            if (visualRoot != null)
            {
                visualRoot.gameObject.SetActive(isActive);
            }
        }

        private void OnDisable()
        {
            ResetControl();
        }

        // A drag that is interrupted by focus loss or suspension never receives its
        // pointer-up event. Without these guards isManualAimActive stays latched and the
        // mouse never regains aim control.
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
            dragThreshold = Mathf.Max(0f, dragThreshold);
            visualRadius = Mathf.Max(1f, visualRadius);
        }
    }
}
