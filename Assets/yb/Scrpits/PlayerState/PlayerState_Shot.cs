using UnityEngine;
namespace yb {
    public class PlayerState_Shot : PlayerState, IPlayerState {
        IRangedWeapon _weapon;
        public PlayerState_Shot(PlayerController player) {
            _weapon = player.WeaponController.RangedWeapon;
        }
        public void OnUpdate(PlayerController player) {
            if(_weapon.CanShot())
                player.ChangeTriggerAnimation(Define.PlayerState.Shot);

            if (Input.GetMouseButtonUp(0)) {
                player.StateController.ChangeState(new PlayerState_Idle(player));
                return;
            }

            if (player.isMoving()) {
                player.StateController.ChangeState(new PlayerState_MovenShot(player));
                return;
            }
        }
    }
}