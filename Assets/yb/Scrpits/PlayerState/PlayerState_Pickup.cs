using Unity.VisualScripting;

namespace yb {
    /// <summary>
    /// 플레이어 아이템 픽업 상태
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