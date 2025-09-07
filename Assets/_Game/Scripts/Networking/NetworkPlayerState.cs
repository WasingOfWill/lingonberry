using UnityEngine;
using Unity.Netcode;

namespace PolymindGames.Networking
{
    public class NetworkPlayerState : NetworkBehaviour
    {
        // Network Variables - these will sync automatically
        public NetworkVariable<float> Health = new(100f, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<float> Hunger = new(100f, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<float> Stamina = new(1f, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<float> Thirst = new(100f, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        // Event for when the host wants to save state
        public override void OnNetworkSpawn()
        {
            if (IsHost)
            {
                // Subscribe to any host-specific save events
                // You can add your save logic here
            }

            if (IsOwner)
            {
                // The client who owns this player can modify these values
            }
        }

        // Called by the client to update their state
        public void UpdateState(float health, float hunger, float stamina, float thirst)
        {
            if (!IsOwner) return;  // Only the owner can update their state
            
            Health.Value = health;
            Hunger.Value = hunger;
            Stamina.Value = stamina;
            Thirst.Value = thirst;
        }

        // Called by the host to save the current state
        [ServerRpc(RequireOwnership = false)]  // Allow the host to call this even if they don't own the player
        public void SaveStateServerRpc()
        {
            if (!IsHost) return;  // Only the host should save

            // Add your save logic here
            Debug.Log($"Saving state for player {OwnerClientId}: Health={Health.Value}, Hunger={Hunger.Value}, Stamina={Stamina.Value}, Thirst={Thirst.Value}");
        }
    }
}