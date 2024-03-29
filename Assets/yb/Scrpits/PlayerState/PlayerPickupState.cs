namespace yb {
    public class PlayerPickState : IPlayerState {
        public PlayerPickState(PlayerController player) {
            player.ChangeAnimation("Pickup");
        }
        public void OnUpdate(PlayerController player) {

        }
    }
}