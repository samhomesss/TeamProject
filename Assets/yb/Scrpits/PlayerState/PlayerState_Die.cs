using UnityEngine;

namespace yb {
    public class PlayerState_Die : IPlayerState {
        public PlayerState_Die(PlayerController player, GameObject attacker) {
            player.OnDieUpdate(attacker);
        }
        public void OnUpdate(PlayerController player) {

        }
    }
}