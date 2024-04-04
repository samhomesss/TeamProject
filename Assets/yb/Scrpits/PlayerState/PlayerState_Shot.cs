using UnityEngine;
namespace yb {
    public class PlayerState_Shot : IPlayerState {
        IRangedWeapon _weapon;
        public PlayerState_Shot(PlayerController player) {
            _weapon = player.RangedWeapon;
        }
        public void OnUpdate(PlayerController player) {
            if(_weapon.CanShot())
                player.ChangeTriggerAnimation(Define.PlayerState.Shot);

            if (Input.GetMouseButtonUp(0)) {
                player.ChangeState(new PlayerState_Idle(player));
                return;
            }

            if (player.isMoving()) {
                player.ChangeState(new PlayerState_MovenShot(player));
                return;
            }
        }
    }
}