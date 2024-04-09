using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;

namespace yb
{
    /// <summary>
    /// �÷��̾� Idle����
    /// </summary>
    public class PlayerState_Idle : PlayerState, IPlayerState
    {
        public PlayerState_Idle(PlayerController player)
        {
            player.ChangeBoolAnimation("Idle");  //�ִϸ��̼� ����
        }
        public void OnUpdate(PlayerController player)
        {
            if (Input.GetMouseButtonDown(0))
            {
                player.StateController.ChangeState(new PlayerState_Shot(player));  //���콺 Ŭ���� ShotState�� ����
                return;
            }
            if (Input.GetKeyDown(KeyCode.R) && player.WeaponController.RangedWeapon.CanReload())
            {
                //���� ������ �����϶� R��ư �Է� �� ReloadState�� ����
                player.StateController.ChangeState(new PlayerState_Reload(player, player.WeaponController.RangedWeapon));  
                return;
            }

            if (player.isMoving())
            {
                //�̵����� �� MoveState�� ����
                player.StateController.ChangeState(new PlayerState_Move(player));
                return;
            }
        }
    }
}