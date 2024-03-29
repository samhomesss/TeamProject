using UnityEngine;

namespace yb {
    public class PlayerMovenAttackState : IPlayerState {
        public PlayerMovenAttackState(PlayerController player) {
            player.ChangeAnimation("Attack");
        }
        public void OnUpdate(PlayerController player) {
            if (Input.GetKeyUp(KeyCode.F)) {
                if (!player.isMoving()) {
                    player.ChangeState(new PlayerIdleState(player));
                    return;
                }
                player.ChangeState(new PlayerMoveState(player));
                return;
            }

            player.OnMoveUpdate();

            //todo
            //이벤트로
            player.OnAttackUpdate();
        }
    }
}