using UnityEngine;

namespace yb {
    public class PlayerAttackState : IPlayerState {
        public PlayerAttackState(PlayerController player) {
            player.ChangeAnimation("Attack");
        }
        public void OnUpdate(PlayerController player) {
            if (Input.GetKeyUp(KeyCode.F)) {
                player.ChangeState(new PlayerIdleState(player));
                return;
            }

            //todo
            //�̺�Ʈ������ �Űܾߵ�
            player.OnAttackUpdate();
        }
    }
}