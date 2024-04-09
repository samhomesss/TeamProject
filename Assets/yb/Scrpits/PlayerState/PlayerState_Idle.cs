using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;

namespace yb
{
    /// <summary>
    /// 플레이어 Idle상태
    /// </summary>
    public class PlayerState_Idle : PlayerState, IPlayerState
    {
        public PlayerState_Idle(PlayerController player)
        {
            player.ChangeBoolAnimation("Idle");  //애니메이션 변경
        }
        public void OnUpdate(PlayerController player)
        {
            if (Input.GetMouseButtonDown(0))
            {
                player.StateController.ChangeState(new PlayerState_Shot(player));  //마우스 클릭시 ShotState로 변경
                return;
            }
            if (Input.GetKeyDown(KeyCode.R) && player.WeaponController.RangedWeapon.CanReload())
            {
                //장전 가능한 상태일때 R버튼 입력 시 ReloadState로 변경
                player.StateController.ChangeState(new PlayerState_Reload(player, player.WeaponController.RangedWeapon));  
                return;
            }

            if (player.isMoving())
            {
                //이동중일 시 MoveState로 변경
                player.StateController.ChangeState(new PlayerState_Move(player));
                return;
            }
        }
    }
}