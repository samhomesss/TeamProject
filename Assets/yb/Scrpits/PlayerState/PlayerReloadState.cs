namespace yb {
    public class PlayerReloadState : IPlayerState {
        public PlayerReloadState(PlayerController player) {
            player.ChangeAnimation("Reload");
        }
        public void OnUpdate(PlayerController player) {

        }
    }
}