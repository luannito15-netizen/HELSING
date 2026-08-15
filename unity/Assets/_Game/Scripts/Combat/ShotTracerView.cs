using UnityEngine;

namespace Helsing.Combat
{
    /// <summary>
    /// WORKING development tracer. Draws the last shot as a fading line that is visible in a
    /// player build, which <see cref="Debug.DrawLine"/> is not — it only renders in the Editor.
    /// Without this the shot is invisible on device and aim cannot be evaluated at all.
    ///
    /// Presentation only: it never decides direction, damage or cadence, it just listens to
    /// <see cref="HitscanWeapon.Fired"/>. This is greybox feedback, not art.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class ShotTracerView : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private HitscanWeapon weapon;

        [Header("TUNING / OPEN")]
        [SerializeField, Min(0f)] private float duration = 0.06f;
        [SerializeField, Min(0.001f)] private float width = 0.05f;
        [SerializeField] private Color startColour = new Color(1f, 0.85f, 0.4f, 1f);
        [SerializeField] private Color endColour = new Color(1f, 0.3f, 0.1f, 0f);

        private LineRenderer line;
        private float hideTime;

        private void Awake()
        {
            weapon ??= GetComponent<HitscanWeapon>();
            EnsureLine();
        }

        private void OnEnable()
        {
            if (weapon != null)
            {
                weapon.Fired += OnWeaponFired;
            }
        }

        private void OnDisable()
        {
            if (weapon != null)
            {
                weapon.Fired -= OnWeaponFired;
            }

            if (line != null)
            {
                line.enabled = false;
            }
        }

        private void LateUpdate()
        {
            if (line != null && line.enabled && Time.time >= hideTime)
            {
                line.enabled = false;
            }
        }

        private void OnWeaponFired(Vector3 origin, Vector3 endPoint)
        {
            EnsureLine();

            if (line == null)
            {
                return;
            }

            // World space, so the line stays put for its lifetime instead of being dragged
            // along by the Player while it fades.
            line.SetPosition(0, origin);
            line.SetPosition(1, endPoint);
            line.enabled = true;
            hideTime = Time.time + duration;
        }

        /// <summary>
        /// Builds the renderer on demand so the component works by simply being added, with no
        /// prefab, material or child object to wire up in the scene.
        /// </summary>
        private void EnsureLine()
        {
            if (line != null)
            {
                return;
            }

            line = gameObject.GetComponent<LineRenderer>();

            if (line == null)
            {
                line = gameObject.AddComponent<LineRenderer>();
            }

            line.useWorldSpace = true;
            line.positionCount = 2;
            line.startWidth = width;
            line.endWidth = width;
            line.startColor = startColour;
            line.endColor = endColour;
            line.numCapVertices = 0;
            line.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            line.receiveShadows = false;
            line.enabled = false;

            if (line.sharedMaterial == null)
            {
                Shader shader = Shader.Find("Sprites/Default") ?? Shader.Find("Unlit/Color");

                if (shader != null)
                {
                    line.sharedMaterial = new Material(shader);
                }
            }
        }

        private void OnValidate()
        {
            duration = Mathf.Max(0f, duration);
            width = Mathf.Max(0.001f, width);
        }
    }
}
