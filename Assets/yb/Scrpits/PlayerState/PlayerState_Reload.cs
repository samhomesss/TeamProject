namespace yb {
    public class PlayerState_Reload : IPlayerState {
        private RangedWeapon _rangedWeapon;
        public PlayerState_Reload(PlayerController player, IRangedWeapon rangedWeapon) {
            _rangedWeapon = rangedWeapon as RangedWeapon;
            player.ChangeTriggerAnimation(PlayerController.State.Reload);
        }
        public void OnUpdate(PlayerController player) {

        }
    }
}