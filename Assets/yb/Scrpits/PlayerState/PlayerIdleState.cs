using UnityEngine;

namespace yb {
    public class PlayerIdleState : IPlayerState {
        public PlayerIdleState(PlayerController player) {
            player.ChangeAnimation("Idle");
        }
        public void OnUpdate(PlayerController player) {
            if (Input.GetKeyDown(KeyCode.F)) {
                player.ChangeState(new PlayerAttackState(player));
                return;
            }

            if (Input.GetMouseButtonDown(0)) {
                player.ChangeState(new PlayerShotState(player));
                return;
            }

            if (player.isMoving()) {
                player.ChangeState(new PlayerMoveState(player));
                return;
            }
        }
    }
}