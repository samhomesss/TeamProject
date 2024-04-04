using Unity.VisualScripting;

namespace yb {
    public class PlayerState_Pickup : IPlayerState {
        public PlayerState_Pickup(PlayerController player) {
            player.ChangeTriggerAnimation(Define.PlayerState.Pickup);
        }
        public void OnUpdate(PlayerController player) {
        }
    }
}