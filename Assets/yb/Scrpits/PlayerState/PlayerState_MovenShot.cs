using UnityEngine;

namespace yb {
    public class PlayerState_MovenShot : PlayerStatus, IPlayerState {
        IRangedWeapon _weapon;

        public PlayerState_MovenShot(PlayerController player) {
            _weapon = player.RangedWeapon;
        }
        public void OnUpdate(PlayerController player) {
            if (_weapon.CanShot())
                player.ChangeTriggerAnimation(Define.PlayerState.Shot);

            if (Input.GetMouseButtonUp(0)) {
                player.Status.SetMoveSpeedDecrease(1f);

                if (!player.isMoving()) {
                    player.ChangeState(new PlayerState_Idle(player));
                    return;
                }
                player.ChangeState(new PlayerState_Move(player));
                return;
            }

            player.Status.SetMoveSpeedDecrease(_data.MoveSpeedDecrease());
            player.OnMoveUpdate();

        }
    }
}