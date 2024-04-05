using UnityEngine;

namespace yb {
    public class PlayerState_Move : PlayerState, IPlayerState {
        public PlayerState_Move(PlayerController player) {
            player.ChangeFadeAnimation("Move");
        }
        public void OnUpdate(PlayerController player) {
            if (Input.GetMouseButtonDown(0)) {
                player.StateController.ChangeState(new PlayerState_MovenShot(player));
                return;
            }
            if (!player.isMoving()) {
                player.StateController.ChangeState(new PlayerState_Idle(player));
                return;
            }

            if (Input.GetKeyDown(KeyCode.R) && player.WeaponController.RangedWeapon.CanReload()) {
                player.StateController.ChangeState(new PlayerState_Reload(player, player.WeaponController.RangedWeapon));
                return;
            }

            player.OnMoveUpdate();
        }
    }
}