using UnityEngine;

namespace yb {
    public class PlayerState_MovenShot : IPlayerState {
        public PlayerState_MovenShot(PlayerController player) {
            player.ChangeAnimation("Shot");
        }
        public void OnUpdate(PlayerController player) {
            if (Input.GetMouseButtonUp(0)) {
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
            player.OnShotUpdate();
        }
    }
}