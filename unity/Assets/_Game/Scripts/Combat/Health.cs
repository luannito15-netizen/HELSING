using System;
using UnityEngine;

namespace Helsing.Combat
{
    /// <summary>
    /// Minimal hit point holder. It owns life state and nothing else: it does not decide
    /// damage rules, resource costs or rewards, so weapons, abilities and run state stay
    /// replaceable without rewriting what it means to be alive.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class Health : MonoBehaviour
    {
        [Header("TUNING / OPEN")]
        [SerializeField, Min(1f)] private float maxHealth = 100f;

        private float currentHealth;

        public float MaxHealth => maxHealth;
        public float CurrentHealth => currentHealth;
        public bool IsAlive => currentHealth > 0f;
        public float NormalisedHealth => maxHealth > 0f ? currentHealth / maxHealth : 0f;

        /// <summary>Raised after damage is applied, with the amount actually taken.</summary>
        public event Action<Health, float> Damaged;

        /// <summary>Raised exactly once, on the transition from alive to dead.</summary>
        public event Action<Health> Died;

        /// <summary>
        /// Raised when health is restored to full. Listeners that only react to damage would
        /// otherwise keep showing the last damaged state after a revive, since nothing else
        /// tells them the value changed upwards.
        /// </summary>
        public event Action<Health> Restored;

        private void Awake()
        {
            ResetHealth();
        }

        public void ResetHealth()
        {
            currentHealth = maxHealth;
            Restored?.Invoke(this);
        }

        /// <summary>
        /// Applies damage and reports death once. Already-dead targets absorb nothing:
        /// without this guard a second hit in the same frame would raise <see cref="Died"/>
        /// twice and let a corpse keep consuming shots.
        /// </summary>
        public void TakeDamage(float amount)
        {
            if (!IsAlive || amount <= 0f)
            {
                return;
            }

            float appliedDamage = Mathf.Min(amount, currentHealth);
            currentHealth -= appliedDamage;

            Damaged?.Invoke(this, appliedDamage);

            if (!IsAlive)
            {
                Died?.Invoke(this);
            }
        }

        private void OnValidate()
        {
            maxHealth = Mathf.Max(1f, maxHealth);
        }
    }
}
