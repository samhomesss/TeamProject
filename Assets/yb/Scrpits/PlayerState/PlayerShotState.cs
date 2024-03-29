using UnityEngine;
namespace yb {
    public class PlayerShotState : IPlayerState {
        public PlayerShotState(PlayerController player) {
            player.ChangeAnimation("Shot");
        }
        public void OnUpdate(PlayerController player) {
            if (Input.GetMouseButtonUp(0)) {
                player.ChangeState(new PlayerIdleState(player));
                return;
            }

            if (player.isMoving()) {
                player.ChangeState(new PlayerMovenShotState(player));
                return;
            }
                
            //todo
            //이벤트로
            player.OnShotUpdate();
        }
    }
}