using UnityEngine;

namespace yb {
    /// <summary>
    /// �÷��̾� �̵� ����
    /// </summary>
    public class PlayerState_Move : PlayerState, IPlayerState {
        public PlayerState_Move(PlayerController player)
        {
            player.ChangeBoolAnimation("Move");  
        }
        public void OnUpdate(PlayerController player) {
            if (Input.GetMouseButtonDown(0)) {
                //�̵��� ���콺 Ŭ���� MovenShotState�� ����
                player.StateController.ChangeState(new PlayerState_MovenShot(player));
                return;
            }

            if (!player.isMoving()) {
                //�̵� ����� Idle���·� ����
                player.StateController.ChangeState(new PlayerState_Idle(player));
                return;
            }

            if (Input.GetKeyDown(KeyCode.R) && player.WeaponController.RangedWeapon.CanReload()) {
                //������ ������ �� RŰ �Է½� ReloadState�� ����
                player.StateController.ChangeState(new PlayerState_Reload(player, player.WeaponController.RangedWeapon));
                return;
            }

            player.OnMoveUpdate(); //�̵� ���� ������Ʈ
        }
    }
}