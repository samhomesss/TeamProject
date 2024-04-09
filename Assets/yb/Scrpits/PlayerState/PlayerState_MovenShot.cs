using UnityEngine;

namespace yb {
    /// <summary>
    /// 플레이어 이동&발사 상태
    /// </summary>
    public class PlayerState_MovenShot : PlayerState, IPlayerState {
        IRangedWeapon _weapon;  //현재 플레이어의 무기 저장

        public PlayerState_MovenShot(PlayerController player)
        {
            _weapon = player.WeaponController.RangedWeapon;
        }
        public void OnUpdate(PlayerController player) {
            if (_weapon.CanShot())
                player.ChangeTriggerAnimation(Define.PlayerState.Shot);  //사격이 가능한 상태면 사격 애니메이션 실행

            if (Input.GetMouseButtonUp(0)) {
                player.Status.SetMoveSpeedDecrease(1f);  //마우스 클릭을 중지 시 이동속도 원상복구

                if (!player.isMoving()) {
                    player.StateController.ChangeState(new PlayerState_Idle(player));  //이동이 중지되면 IdleState로 변경
                    return;
                }
                player.StateController.ChangeState(new PlayerState_Move(player));  //마우스 클릭만 중지하면 MoveState로 변경
                return;
            }

            player.Status.SetMoveSpeedDecrease(_data.MoveSpeedDecrease());  //이동사격 중엔 이동속도 감소
            player.OnMoveUpdate();  //이동 관련 함수 호출

        }
    }
}