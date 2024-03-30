using Unity.VisualScripting;

namespace yb {
    public class PlayerState_Pickup : IPlayerState {
        public PlayerState_Pickup(PlayerController player) {
            player.ChangeAnimation("Pickup");
        }
        public void OnUpdate(PlayerController player) {
            player.ChangeState(new PlayerState_Idle(player));
        }
    }
}