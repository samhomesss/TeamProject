using UnityEngine;

namespace yb {
    public class PlayerMovenShotState : IPlayerState {
        public PlayerMovenShotState(PlayerController player) {
            player.ChangeAnimation("Shot");
        }
        public void OnUpdate(PlayerController player) {
            if (Input.GetMouseButtonUp(0)) {
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
            player.OnShotUpdate();
        }
    }
}