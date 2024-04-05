using UnityEngine;

namespace yb {
    public class PlayerState_Idle : PlayerState, IPlayerState {
        public PlayerState_Idle(PlayerController player) {
            player.ChangeFadeAnimation("Idle");
        }
        public void OnUpdate(PlayerController player) {
            if (Input.GetMouseButtonDown(0)) {
                player.StateController.ChangeState(new PlayerState_Shot(player));
                return;
            }

            if(Input.GetKeyDown(KeyCode.R) && player.WeaponController.RangedWeapon.CanReload()) {
                player.StateController.ChangeState(new PlayerState_Reload(player, player.WeaponController.RangedWeapon));
                return;
            }

            if (player.isMoving()) {
                player.StateController.ChangeState(new PlayerState_Move(player));
                return;
            }
        }
    }
}