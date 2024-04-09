using UnityEngine;

namespace yb {
    /// <summary>
    /// 플레이어 사망 상태
    /// </summary>
    public class PlayerState_Die : PlayerState, IPlayerState {
        public PlayerState_Die(PlayerController player, GameObject attacker){
            player.OnDieUpdate(attacker);  //플레이어 사망 관련 함수 호출
        }
        public void OnUpdate(PlayerController player) {

        }
    }
}