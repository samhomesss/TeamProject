using UnityEngine;

namespace yb {
    public class PlayerMoveState : IPlayerState {
        public PlayerMoveState(PlayerController player) {
            player.ChangeAnimation("Move");
        }
        public void OnUpdate(PlayerController player) {
            if (Input.GetKeyDown(KeyCode.F)) {
                player.ChangeState(new PlayerMovenAttackState(player));
                return;
            }
            if (Input.GetMouseButtonDown(0)) {
                player.ChangeState(new PlayerMovenShotState(player));
                return;
            }
            if (!player.isMoving()) {
                player.ChangeState(new PlayerIdleState(player));
                return;
            }

            player.OnMoveUpdate();
        }
    }
}