using UnityEngine;

namespace yb {
    public class PlayerState_Attack : IPlayerState {
        public PlayerState_Attack(PlayerController player) {
            player.ChangeAnimation("Attack");
        }
        public void OnUpdate(PlayerController player) {
            if (Input.GetKeyUp(KeyCode.F)) {
                player.ChangeState(new PlayerState_Idle(player));
                return;
            }

            //todo
            //이벤트쪽으로 옮겨야됨
            player.OnAttackUpdate();
        }
    }
}