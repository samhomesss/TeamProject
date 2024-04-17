using UnityEngine;

namespace yb {
    /// <summary>
    /// 플레이어 이동 상태
    /// </summary>
    public class PlayerState_Move : PlayerState, IPlayerState {
        public PlayerState_Move(PlayerController player)
        {
            player.ChangeBoolAnimation("Move");  
        }
        public void OnUpdate(PlayerController player) {
            if (Input.GetMouseButtonDown(0)) {
                //이동중 마우스 클릭시 MovenShotState로 변경
                player.StateController.ChangeState(new PlayerState_MovenShot(player));
                return;
            }

            if (!player.isMoving()) {
                //이동 종료시 Idle상태로 변경
                player.StateController.ChangeState(new PlayerState_Idle(player));
                return;
            }

            if (Input.GetKeyDown(KeyCode.R) && player.WeaponController.RangedWeapon.CanReload()) {
                //재장전 가능할 때 R키 입력시 ReloadState로 변경
                player.StateController.ChangeState(new PlayerState_Reload(player, player.WeaponController.RangedWeapon));
                return;
            }

            player.OnMoveUpdate(); //이동 관렵 업데이트
        }
    }
}