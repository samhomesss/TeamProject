using Unity.VisualScripting;

namespace yb {
    /// <summary>
    /// �÷��̾� ������ �Ⱦ� ����
    /// </summary>
    public class PlayerState_Pickup : PlayerState, IPlayerState {
        public PlayerState_Pickup(PlayerController player)
        {
            player.ChangeTriggerAnimation(Define.PlayerState.Pickup);
        }
        public void OnUpdate(PlayerController player) {
        }
    }
}