namespace yb {
    public class PlayerState_Reload : IPlayerState {
        private RangedWeapon _rangedWeapon;
        public PlayerState_Reload(PlayerController player, RangedWeapon rangedWeapon) {
            player.ChangeAnimation("Reload");
            _rangedWeapon = rangedWeapon;
        }
        public void OnUpdate(PlayerController player) {

        }
    }
}