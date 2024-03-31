using UnityEngine;
namespace yb {
    public class PlayerState_Shot : IPlayerState {
        public PlayerState_Shot(PlayerController player) {
            player.ChangeAnimation("Shot");
        }
        public void OnUpdate(PlayerController player) {
            if (Input.GetMouseButtonUp(0)) {
                player.ChangeState(new PlayerState_Idle(player));
                return;
            }

            if (player.isMoving()) {
                player.ChangeState(new PlayerState_MovenShot(player));
                return;
            }
                
            //todo
            //이벤트로
            player.OnShotUpdate();
        }
    }
}