using UnityEngine;
using UnityEngine.InputSystem;

namespace Helsing.Input
{
    [DisallowMultipleComponent]
    public sealed class PlayerInputReader : MonoBehaviour
    {
        [Header("Input Actions")]
        [SerializeField] private InputActionAsset inputActions;
        [SerializeField] private string actionMapName = "Gameplay";
        [SerializeField] private string moveActionName = "Move";
        [SerializeField] private string pointerPositionActionName = "PointerPosition";

        private InputAction moveAction;
        private InputAction pointerPositionAction;
        private Vector2 virtualMoveIntent;
        private Vector2 manualAimIntent;
        private bool isVirtualMoveActive;
        private bool isManualAimActive;
        private bool attackTapRequested;

        public Vector2 MoveIntent => isVirtualMoveActive
            ? virtualMoveIntent
            : moveAction != null ? moveAction.ReadValue<Vector2>() : Vector2.zero;
        public Vector2 PointerPosition => pointerPositionAction != null
            ? pointerPositionAction.ReadValue<Vector2>()
            : Vector2.zero;
        public Vector2 ManualAimIntent => manualAimIntent;
        public bool IsVirtualMoveActive => isVirtualMoveActive;
        public bool IsManualAimActive => isManualAimActive;

        /// <summary>
        /// True only when a hover-capable pointer exists, which is what the desktop aim
        /// fallback assumes. A touchscreen keeps reporting the last touch position after the
        /// finger lifts, so letting it drive this fallback would make the Player face
        /// wherever the screen was touched last — including the movement joystick.
        /// </summary>
        public bool HasPointerAim =>
            pointerPositionAction != null && (Mouse.current != null || Pen.current != null);

        public void SetVirtualMoveIntent(Vector2 intent)
        {
            virtualMoveIntent = Vector2.ClampMagnitude(intent, 1f);
            isVirtualMoveActive = true;
        }

        public void ClearVirtualMoveIntent()
        {
            virtualMoveIntent = Vector2.zero;
            isVirtualMoveActive = false;
        }

        public void SetManualAimIntent(Vector2 intent)
        {
            if (intent.sqrMagnitude < 0.0001f)
            {
                return;
            }

            manualAimIntent = intent.normalized;
            isManualAimActive = true;
        }

        public void ClearManualAimIntent()
        {
            isManualAimActive = false;
        }

        /// <summary>
        /// Requests a single attack. Raised by the attack control when a press ended without
        /// ever becoming a drag; an interrupted press — cancel, focus loss or pause — must
        /// never reach this, otherwise leaving the app would fire a phantom shot.
        /// </summary>
        public void RequestAttackTap()
        {
            attackTapRequested = true;
        }

        /// <summary>
        /// Returns true exactly once per requested tap. Consuming the flag keeps the shot
        /// tied to the gesture instead of to whichever frame happens to read it.
        /// </summary>
        public bool ConsumeAttackTap()
        {
            if (!attackTapRequested)
            {
                return false;
            }

            attackTapRequested = false;
            return true;
        }

        private bool dashRequested;
        private Vector2 dashDirection;

        /// <summary>
        /// Direction captured when the dash was requested, in stick space. Zero means the
        /// gesture carried no usable direction and the consumer should fall back to facing.
        /// </summary>
        public Vector2 DashDirection => dashDirection;

        /// <summary>
        /// Requests a single dash in <paramref name="direction"/>. Raised by the movement
        /// control on a swipe. The direction travels with the request on purpose: a quick
        /// swipe can end before the dash is consumed, and by then the stick has already
        /// returned to zero — reading it later would dash the wrong way.
        /// </summary>
        public void RequestDash(Vector2 direction)
        {
            dashRequested = true;
            dashDirection = direction.sqrMagnitude > 0.0001f ? direction.normalized : Vector2.zero;
        }

        /// <summary>Returns true exactly once per requested dash.</summary>
        public bool ConsumeDash()
        {
            if (!dashRequested)
            {
                return false;
            }

            dashRequested = false;
            return true;
        }

        /// <summary>
        /// Drops a pending dash without consuming it as a real input. Used when a gesture is
        /// interrupted, so leaving the app with a half-formed double tap never dashes on return.
        /// </summary>
        public void ClearDashRequest()
        {
            dashRequested = false;
            dashDirection = Vector2.zero;
        }

        private void OnEnable()
        {
            if (!TryResolveActions())
            {
                return;
            }

            moveAction.Enable();
            pointerPositionAction.Enable();
        }

        private void OnDisable()
        {
            moveAction?.Disable();
            pointerPositionAction?.Disable();
            ClearVirtualMoveIntent();
            ClearManualAimIntent();
            attackTapRequested = false;
        }

        private bool TryResolveActions()
        {
            if (inputActions == null)
            {
                Debug.LogError($"{nameof(PlayerInputReader)} on '{name}' requires an Input Action Asset.", this);
                return false;
            }

            InputActionMap actionMap = inputActions.FindActionMap(actionMapName, false);
            moveAction = actionMap?.FindAction(moveActionName, false);
            pointerPositionAction = actionMap?.FindAction(pointerPositionActionName, false);

            if (moveAction != null && pointerPositionAction != null)
            {
                return true;
            }

            Debug.LogError(
                $"{nameof(PlayerInputReader)} requires actions '{actionMapName}/{moveActionName}' and " +
                $"'{actionMapName}/{pointerPositionActionName}' on '{name}'.",
                this);
            return false;
        }
    }
}
