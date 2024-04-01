using UnityEngine;

namespace yb {
    public class PlayerState_Idle : IPlayerState {
        public PlayerState_Idle(PlayerController player) {
            player.ChangeFadeAnimation("Idle");
        }
        public void OnUpdate(PlayerController player) {
            if (Input.GetMouseButtonDown(0)) {
                player.ChangeState(new PlayerState_Shot(player));
                return;
            }

            if(Input.GetKeyDown(KeyCode.R) && player.RangedWeapon.CanReload()) {
                player.ChangeState(new PlayerState_Reload(player, player.RangedWeapon));
                return;
            }

            if (player.isMoving()) {
                player.ChangeState(new PlayerState_Move(player));
                return;
            }
        }
    }
}