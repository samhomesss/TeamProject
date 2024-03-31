namespace yb {
    public class PlayerState_Die : IPlayerState {
        public PlayerState_Die(PlayerController player) {
            player.OnDieUpdate();
            Managers.Resources.Destroy(player.gameObject);
        }
        public void OnUpdate(PlayerController player) {

        }
    }
}