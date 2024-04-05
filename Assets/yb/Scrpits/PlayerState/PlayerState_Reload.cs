namespace yb {
    public class PlayerState_Reload : PlayerState, IPlayerState {
        private RangedWeapon _rangedWeapon;
        public PlayerState_Reload(PlayerController player, IRangedWeapon rangedWeapon) {
            _rangedWeapon = rangedWeapon as RangedWeapon;
            player.ChangeTriggerAnimation(Define.PlayerState.Reload);
        }
        public void OnUpdate(PlayerController player) {

        }
    }
}