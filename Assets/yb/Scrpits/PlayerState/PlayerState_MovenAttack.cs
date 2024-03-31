using UnityEngine;

namespace yb {
    public class PlayerState_MovenAttack : IPlayerState {
        public PlayerState_MovenAttack(PlayerController player) {
            player.ChangeAnimation("Attack");
        }
        public void OnUpdate(PlayerController player) {
            if (Input.GetKeyUp(KeyCode.F)) {
                if (!player.isMoving()) {
                    player.ChangeState(new PlayerState_Idle(player));
                    return;
                }
                player.ChangeState(new PlayerState_Move(player));
                return;
            }

            player.OnMoveUpdate();

            //todo
            //이벤트로
            player.OnAttackUpdate();
        }
    }
}