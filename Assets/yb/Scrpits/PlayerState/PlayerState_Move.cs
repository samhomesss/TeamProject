using UnityEngine;

namespace yb {
    public class PlayerState_Move : IPlayerState {
        public PlayerState_Move(PlayerController player) {
            player.ChangeFadeAnimation("Move");
        }
        public void OnUpdate(PlayerController player) {
            if (Input.GetMouseButtonDown(0)) {
                player.ChangeState(new PlayerState_MovenShot(player));
                return;
            }
            if (!player.isMoving()) {
                player.ChangeState(new PlayerState_Idle(player));
                return;
            }

            if (Input.GetKeyDown(KeyCode.R) && player.RangedWeapon.CanReload()) {
                player.ChangeState(new PlayerState_Reload(player, player.RangedWeapon));
                return;
            }

            player.OnMoveUpdate();
        }
    }
}