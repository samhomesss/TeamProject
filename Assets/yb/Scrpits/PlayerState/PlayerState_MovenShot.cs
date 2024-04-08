using UnityEngine;

namespace yb {
    public class PlayerState_MovenShot : PlayerState, IPlayerState {
        IRangedWeapon _weapon;

        public PlayerState_MovenShot(PlayerController player) : base(player)
        {
            _weapon = player.WeaponController.RangedWeapon;
        }
        public void OnUpdate(PlayerController player) {
            if (_weapon.CanShot())
                player.ChangeTriggerAnimation(Define.PlayerState.Shot);

            if (Input.GetMouseButtonUp(0)) {
                player.Status.SetMoveSpeedDecrease(1f);

                if (!player.isMoving()) {
                    player.StateController.ChangeState(new PlayerState_Idle(player));
                    return;
                }
                player.StateController.ChangeState(new PlayerState_Move(player));
                return;
            }

            player.Status.SetMoveSpeedDecrease(_data.MoveSpeedDecrease());
            player.OnMoveUpdate();

        }
    }
}