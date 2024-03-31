using UnityEngine;

namespace yb {
    public class PlayerState_Idle : IPlayerState {
        public PlayerState_Idle(PlayerController player) {
            player.ChangeAnimation("Idle");
        }
        public void OnUpdate(PlayerController player) {
            if (Input.GetKeyDown(KeyCode.F)) {
                player.ChangeState(new PlayerState_Attack(player));
                return;
            }

            if (Input.GetMouseButtonDown(0)) {
                player.ChangeState(new PlayerState_Shot(player));
                return;
            }

            if (player.isMoving()) {
                player.ChangeState(new PlayerState_Move(player));
                return;
            }
        }
    }
}