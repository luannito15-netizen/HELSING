using System.Collections.Generic;
using UnityEngine;

namespace Helsing.Combat
{
    /// <summary>
    /// Development aid: brings dead dummies back after a delay so a test session does not run
    /// out of targets in the first minute. Not a gameplay system — real enemy spawning,
    /// encounters and Threat are OPEN and must not be inferred from this.
    ///
    /// It lives on the container, not on the dummies, on purpose: the dummies deactivate
    /// themselves on death, and a component on a disabled GameObject cannot time its own
    /// return.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class DummyRespawner : MonoBehaviour
    {
        [Header("TUNING / OPEN")]
        [SerializeField, Min(0f)] private float respawnDelay = 3f;

        private readonly List<Health> tracked = new List<Health>();
        private readonly List<PendingRespawn> pending = new List<PendingRespawn>();

        private struct PendingRespawn
        {
            public Health Health;
            public float DueTime;
        }

        private void OnEnable()
        {
            tracked.Clear();
            GetComponentsInChildren(true, tracked);

            for (int i = 0; i < tracked.Count; i++)
            {
                if (tracked[i] != null)
                {
                    tracked[i].Died += OnDummyDied;
                }
            }
        }

        private void OnDisable()
        {
            for (int i = 0; i < tracked.Count; i++)
            {
                if (tracked[i] != null)
                {
                    tracked[i].Died -= OnDummyDied;
                }
            }

            tracked.Clear();
            pending.Clear();
        }

        private void Update()
        {
            for (int i = pending.Count - 1; i >= 0; i--)
            {
                if (Time.time < pending[i].DueTime)
                {
                    continue;
                }

                Respawn(pending[i].Health);
                pending.RemoveAt(i);
            }
        }

        private void OnDummyDied(Health health)
        {
            if (health == null)
            {
                return;
            }

            pending.Add(new PendingRespawn
            {
                Health = health,
                DueTime = Time.time + respawnDelay,
            });
        }

        private static void Respawn(Health health)
        {
            if (health == null)
            {
                return;
            }

            // Health is restored before reactivating so the damage tint reads full life in its
            // OnEnable. Reversed, the dummy would come back looking wounded.
            health.ResetHealth();

            if (!health.gameObject.activeSelf)
            {
                health.gameObject.SetActive(true);
            }
        }

        private void OnValidate()
        {
            respawnDelay = Mathf.Max(0f, respawnDelay);
        }
    }
}
