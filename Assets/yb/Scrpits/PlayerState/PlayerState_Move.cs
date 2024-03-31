using UnityEngine;

namespace yb {
    public class PlayerState_Move : IPlayerState {
        public PlayerState_Move(PlayerController player) {
            player.ChangeAnimation("Move");
        }
        public void OnUpdate(PlayerController player) {
            if (Input.GetKeyDown(KeyCode.F)) {
                player.ChangeState(new PlayerState_MovenAttack(player));
                return;
            }
            if (Input.GetMouseButtonDown(0)) {
                player.ChangeState(new PlayerState_MovenShot(player));
                return;
            }
            if (!player.isMoving()) {
                player.ChangeState(new PlayerState_Idle(player));
                return;
            }

            player.OnMoveUpdate();
        }
    }
}