using Helsing.Combat;
using UnityEngine;

namespace Helsing.Player
{
    /// <summary>
    /// Sandbox death handling: returns the Player to the spawn point with full health after a
    /// delay. Development scaffolding only — it deliberately does not touch loadout, stash or
    /// loss of any kind, because death settlement belongs to RUN-002 in P2 and is still OPEN.
    /// Nothing here should be read as an economy decision.
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Health))]
    public sealed class PlayerRespawn : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private Health health;
        [SerializeField] private CharacterController characterController;

        [Header("TUNING / OPEN")]
        [SerializeField, Min(0f)] private float respawnDelay = 2f;

        [Tooltip("Components disabled while dead, so a corpse cannot move or shoot.")]
        [SerializeField] private MonoBehaviour[] disableWhileDead;

        private Vector3 spawnPosition;
        private Quaternion spawnRotation;
        private bool isDead;
        private float reviveTime;

        public bool IsDead => isDead;

        private void Awake()
        {
            health ??= GetComponent<Health>();
            characterController ??= GetComponent<CharacterController>();
            spawnPosition = transform.position;
            spawnRotation = transform.rotation;
        }

        private void OnEnable()
        {
            if (health != null)
            {
                health.Died += OnDied;
            }
        }

        private void OnDisable()
        {
            if (health != null)
            {
                health.Died -= OnDied;
            }
        }

        private void Update()
        {
            if (isDead && Time.time >= reviveTime)
            {
                Revive();
            }
        }

        private void OnDied(Health source)
        {
            if (isDead)
            {
                return;
            }

            isDead = true;
            reviveTime = Time.time + respawnDelay;
            SetControlEnabled(false);
        }

        private void Revive()
        {
            isDead = false;

            // The controller has to be off for the teleport: it owns the transform while
            // enabled and would otherwise fight the assignment or resolve it as a collision.
            bool hadController = characterController != null && characterController.enabled;

            if (hadController)
            {
                characterController.enabled = false;
            }

            transform.SetPositionAndRotation(spawnPosition, spawnRotation);

            if (hadController)
            {
                characterController.enabled = true;
            }

            health?.ResetHealth();
            SetControlEnabled(true);
        }

        private void SetControlEnabled(bool value)
        {
            if (disableWhileDead == null)
            {
                return;
            }

            for (int i = 0; i < disableWhileDead.Length; i++)
            {
                if (disableWhileDead[i] != null)
                {
                    disableWhileDead[i].enabled = value;
                }
            }
        }

        /// <summary>Moves the spawn anchor, for when the sandbox start point changes.</summary>
        public void SetSpawnPoint(Vector3 position, Quaternion rotation)
        {
            spawnPosition = position;
            spawnRotation = rotation;
        }

        private void OnValidate()
        {
            respawnDelay = Mathf.Max(0f, respawnDelay);
        }
    }
}
