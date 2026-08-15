using System.Collections.Generic;
using System.Text;
using Helsing.Input;
using Helsing.UI;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Helsing.UI.EditorTools
{
    /// <summary>
    /// Editor-only harness that drives <see cref="VirtualJoystickControl"/> and
    /// <see cref="ManualAimDragControl"/> through the real UI event handlers using two
    /// distinct pointer ids, which a single mouse in the Game View cannot reproduce.
    ///
    /// It builds its own throwaway hierarchy, so it never touches Prototype_Arena_01 and
    /// leaves no objects behind. This validates the handler path only: it is NOT a
    /// substitute for a real device test.
    /// </summary>
    public static class MultiTouchHandlerHarness
    {
        private const string InputActionsPath = "Assets/_Game/Input/HelsingGameplay.inputactions";
        private const int LeftPointerId = 0;
        private const int RightPointerId = 1;

        // Greys the menu item out outside Play Mode, so the defect below cannot be reached
        // by accident from the menu.
        [MenuItem("HELSING/Validation/Run Multi-Touch Handler Harness", true)]
        private static bool ValidateRun() => EditorApplication.isPlaying;

        [MenuItem("HELSING/Validation/Run Multi-Touch Handler Harness")]
        public static void Run()
        {
            // Outside Play Mode Unity does not call Awake() on plain MonoBehaviours, so
            // controlRect/interactionRect stay null, UpdatePointer and TryGetLocalPoint return
            // early and the controls never activate. Every check would then be meaningless:
            // the failures are artefacts of the context and the passes only assert flags that
            // were already false. Refusing to run is the only honest outcome.
            if (!EditorApplication.isPlaying)
            {
                Debug.LogError(
                    "MULTITOUCH HANDLER HARNESS\n" +
                    "RESULT: NOT RUN — requires Play Mode.\n" +
                    "Outside Play Mode the controls never activate, so the result would be meaningless.");
                return;
            }

            var results = new List<string>();
            GameObject root = null;

            try
            {
                root = BuildHarness(
                    out PlayerInputReader reader,
                    out VirtualJoystickControl joystick,
                    out ManualAimDragControl aimDrag);

                RunChecks(reader, joystick, aimDrag, results);
            }
            finally
            {
                if (root != null)
                {
                    Object.DestroyImmediate(root);
                }
            }

            Report(results);
        }

        private static void RunChecks(
            PlayerInputReader reader,
            VirtualJoystickControl joystick,
            ManualAimDragControl aimDrag,
            List<string> results)
        {
            // 1. Left pointer holds movement.
            PointerEventData leftPointer = PressAndDrag(joystick, LeftPointerId, Vector2.zero, new Vector2(82f, 0f));
            Check(results, "left pointer drives movement",
                reader.IsVirtualMoveActive && reader.MoveIntent.sqrMagnitude > 0.01f);

            // 2. Right pointer holds aim, using a different pointer id.
            PointerEventData rightPointer = PressAndDrag(aimDrag, RightPointerId, Vector2.zero, new Vector2(0f, 60f));
            Check(results, "right pointer drives manual aim",
                reader.IsManualAimActive && reader.ManualAimIntent.sqrMagnitude > 0.01f);

            // 3. Both intents coexist.
            Check(results, "both pointers active simultaneously",
                reader.IsVirtualMoveActive && reader.IsManualAimActive);

            // 4. Releasing one pointer must not disturb the other.
            joystick.OnPointerUp(leftPointer);
            Check(results, "releasing left keeps aim active",
                !reader.IsVirtualMoveActive && reader.IsManualAimActive);

            aimDrag.OnPointerUp(rightPointer);
            Check(results, "releasing right clears aim",
                !reader.IsManualAimActive);

            // 4b. Cancelling one pointer must not disturb the other.
            leftPointer = PressAndDrag(joystick, LeftPointerId, Vector2.zero, new Vector2(82f, 0f));
            rightPointer = PressAndDrag(aimDrag, RightPointerId, Vector2.zero, new Vector2(0f, 60f));
            aimDrag.OnCancel(new BaseEventData(EventSystem.current));
            Check(results, "cancelling right keeps movement active",
                reader.IsVirtualMoveActive && !reader.IsManualAimActive);

            // 5. Focus loss releases every held pointer.
            rightPointer = PressAndDrag(aimDrag, RightPointerId, Vector2.zero, new Vector2(0f, 60f));
            SendApplicationFocusLost(joystick);
            SendApplicationFocusLost(aimDrag);
            Check(results, "focus loss resets both controls",
                !reader.IsVirtualMoveActive && !reader.IsManualAimActive);

            // 6. Fallback gates are open again, so keyboard/gamepad move and mouse aim resume.
            Check(results, "fallback gates released after reset",
                !reader.IsVirtualMoveActive && !reader.IsManualAimActive);

            // 7. Reset is idempotent: repeating it must not throw or latch state.
            SendApplicationFocusLost(joystick);
            SendApplicationFocusLost(aimDrag);
            joystick.OnCancel(new BaseEventData(EventSystem.current));
            aimDrag.OnCancel(new BaseEventData(EventSystem.current));
            Check(results, "repeated reset stays idempotent",
                !reader.IsVirtualMoveActive && !reader.IsManualAimActive);
        }

        private static GameObject BuildHarness(
            out PlayerInputReader reader,
            out VirtualJoystickControl joystick,
            out ManualAimDragControl aimDrag)
        {
            // Built inactive so component Awake runs only after serialized wiring is in place.
            var root = new GameObject("HELSING_MultiTouchHarness", typeof(RectTransform))
            {
                hideFlags = HideFlags.HideAndDontSave
            };
            root.SetActive(false);

            var canvas = root.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;

            reader = root.AddComponent<PlayerInputReader>();
            AssignSerializedReference(reader, "inputActions",
                AssetDatabase.LoadAssetAtPath<InputActionAsset>(InputActionsPath));

            joystick = CreateControl<VirtualJoystickControl>(root, "Joystick", new Vector2(-320f, -160f));
            AssignSerializedReference(joystick, "inputReader", reader);

            aimDrag = CreateControl<ManualAimDragControl>(root, "AimDrag", new Vector2(320f, -160f));
            AssignSerializedReference(aimDrag, "inputReader", reader);

            root.SetActive(true);
            return root;
        }

        private static T CreateControl<T>(GameObject parent, string name, Vector2 anchoredPosition)
            where T : Component
        {
            var controlObject = new GameObject(name, typeof(RectTransform));
            controlObject.transform.SetParent(parent.transform, false);

            var rect = (RectTransform)controlObject.transform;
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(400f, 400f);
            rect.anchoredPosition = anchoredPosition;

            return controlObject.AddComponent<T>();
        }

        private static PointerEventData PressAndDrag(
            Component control,
            int pointerId,
            Vector2 pressLocalOffset,
            Vector2 dragLocalOffset)
        {
            var rect = (RectTransform)control.transform;
            var pointer = new PointerEventData(EventSystem.current) { pointerId = pointerId };

            pointer.position = ToScreenPoint(rect, pressLocalOffset);
            ((IPointerDownHandler)control).OnPointerDown(pointer);

            pointer.position = ToScreenPoint(rect, dragLocalOffset);
            ((IDragHandler)control).OnDrag(pointer);

            return pointer;
        }

        private static Vector2 ToScreenPoint(RectTransform rect, Vector2 localOffset)
        {
            Vector2 center = RectTransformUtility.WorldToScreenPoint(null, rect.position);
            return center + localOffset;
        }

        private static void SendApplicationFocusLost(Component control)
        {
            // OnApplicationFocus is an engine message; SendMessage reaches the private handler.
            control.gameObject.SendMessage("OnApplicationFocus", false, SendMessageOptions.DontRequireReceiver);
        }

        private static void AssignSerializedReference(Object target, string propertyName, Object value)
        {
            var serializedObject = new SerializedObject(target);
            SerializedProperty property = serializedObject.FindProperty(propertyName);

            if (property == null)
            {
                Debug.LogError($"Harness could not find serialized property '{propertyName}' on {target.GetType().Name}.");
                return;
            }

            property.objectReferenceValue = value;
            serializedObject.ApplyModifiedPropertiesWithoutUndo();
        }

        private static void Check(List<string> results, string label, bool passed)
        {
            results.Add($"{(passed ? "PASS" : "FAIL")} — {label}");
        }

        private static void Report(List<string> results)
        {
            var summary = new StringBuilder("MULTITOUCH HANDLER HARNESS\n");
            bool allPassed = true;

            foreach (string result in results)
            {
                summary.AppendLine(result);
                allPassed &= result.StartsWith("PASS");
            }

            summary.AppendLine(allPassed
                ? "RESULT: PASS VIA HARNESS — REAL DEVICE: NOT RUN"
                : "RESULT: FAIL — inspect the failing checks above");

            if (allPassed)
            {
                Debug.Log(summary.ToString());
            }
            else
            {
                Debug.LogError(summary.ToString());
            }
        }
    }
}
