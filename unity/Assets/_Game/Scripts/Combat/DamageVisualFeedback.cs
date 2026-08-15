using UnityEngine;

namespace Helsing.Combat
{
    /// <summary>
    /// Greybox damage readout: tints the renderer from healthy to hurt as hit points drop
    /// and hides the object on death, so "hit" and "killed" are visible without a HUD.
    ///
    /// Development feedback only. It is deliberately not a health bar: a world-space canvas
    /// per enemy costs more on mobile and would invent HUD direction that is still OPEN.
    /// Uses a MaterialPropertyBlock so it never instantiates materials.
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Health))]
    public sealed class DamageVisualFeedback : MonoBehaviour
    {
        private static readonly int BaseColorId = Shader.PropertyToID("_BaseColor");

        [Header("Dependencies")]
        [SerializeField] private Health health;
        [SerializeField] private Renderer targetRenderer;

        [Header("TUNING / OPEN")]
        [SerializeField] private Color healthyColor = new Color(0.75f, 0.75f, 0.75f);
        [SerializeField] private Color hurtColor = new Color(0.85f, 0.1f, 0.1f);
        [SerializeField] private bool hideOnDeath = true;

        private MaterialPropertyBlock propertyBlock;

        private void Awake()
        {
            health ??= GetComponent<Health>();
            targetRenderer ??= GetComponentInChildren<Renderer>();
            propertyBlock ??= new MaterialPropertyBlock();
        }

        private void OnEnable()
        {
            if (health == null)
            {
                return;
            }

            health.Damaged += HandleDamaged;
            health.Died += HandleDied;
            health.Restored += HandleRestored;
            ApplyTint();
        }

        /// <summary>
        /// Re-applies the tint once every Awake has run. Doing it only in OnEnable is not
        /// enough: component order can put this before <see cref="Health"/> initialises, and
        /// the object would start painted as if it were nearly dead.
        /// </summary>
        private void Start()
        {
            ApplyTint();
        }

        private void OnDisable()
        {
            if (health == null)
            {
                return;
            }

            health.Damaged -= HandleDamaged;
            health.Died -= HandleDied;
            health.Restored -= HandleRestored;
        }

        private void HandleDamaged(Health source, float amount)
        {
            ApplyTint();
        }

        /// <summary>
        /// Repaints on revive. Required for anything that respawns without being deactivated —
        /// the Player does exactly that, so without this it comes back alive but still tinted
        /// with the colour it died in.
        /// </summary>
        private void HandleRestored(Health source)
        {
            if (hideOnDeath && !gameObject.activeSelf)
            {
                gameObject.SetActive(true);
            }

            ApplyTint();
        }

        private void HandleDied(Health source)
        {
            if (hideOnDeath)
            {
                gameObject.SetActive(false);
            }
        }

        private void ApplyTint()
        {
            if (targetRenderer == null || health == null)
            {
                return;
            }

            propertyBlock ??= new MaterialPropertyBlock();
            targetRenderer.GetPropertyBlock(propertyBlock);
            propertyBlock.SetColor(BaseColorId, Color.Lerp(hurtColor, healthyColor, health.NormalisedHealth));
            targetRenderer.SetPropertyBlock(propertyBlock);
        }

        private void Reset()
        {
            health = GetComponent<Health>();
            targetRenderer = GetComponentInChildren<Renderer>();
        }
    }
}
