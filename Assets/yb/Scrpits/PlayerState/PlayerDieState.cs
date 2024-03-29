namespace yb {
    public class PlayerDieState : IPlayerState {
        public PlayerDieState(PlayerController player) {
            player.ChangeAnimation("Die");
        }
        public void OnUpdate(PlayerController player) {

        }
    }
}