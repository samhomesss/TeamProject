using UnityEngine;

namespace yb {
    /// <summary>
    /// �÷��̾� ��� ����
    /// </summary>
    public class PlayerState_Die : PlayerState, IPlayerState {
        public PlayerState_Die(PlayerController player, GameObject attacker){
            player.OnDieUpdate(attacker);  //�÷��̾� ��� ���� �Լ� ȣ��
        }
        public void OnUpdate(PlayerController player) {

        }
    }
}