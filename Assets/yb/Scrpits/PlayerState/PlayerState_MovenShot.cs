using UnityEngine;

namespace yb {
    /// <summary>
    /// �÷��̾� �̵�&�߻� ����
    /// </summary>
    public class PlayerState_MovenShot : PlayerState, IPlayerState {
        IRangedWeapon _weapon;  //���� �÷��̾��� ���� ����

        public PlayerState_MovenShot(PlayerController player)
        {
            _weapon = player.WeaponController.RangedWeapon;
        }
        public void OnUpdate(PlayerController player) {
            if (_weapon.CanShot())
                player.ChangeTriggerAnimation(Define.PlayerState.Shot);  //����� ������ ���¸� ��� �ִϸ��̼� ����

            if (Input.GetMouseButtonUp(0)) {
                player.Status.SetMoveSpeedDecrease(1f);  //���콺 Ŭ���� ���� �� �̵��ӵ� ���󺹱�

                if (!player.isMoving()) {
                    player.StateController.ChangeState(new PlayerState_Idle(player));  //�̵��� �����Ǹ� IdleState�� ����
                    return;
                }
                player.StateController.ChangeState(new PlayerState_Move(player));  //���콺 Ŭ���� �����ϸ� MoveState�� ����
                return;
            }

            player.Status.SetMoveSpeedDecrease(_data.MoveSpeedDecrease());  //�̵���� �߿� �̵��ӵ� ����
            player.OnMoveUpdate();  //�̵� ���� �Լ� ȣ��

        }
    }
}