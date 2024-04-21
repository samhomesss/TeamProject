using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;

namespace yb
{
    /// <summary>
    /// 게임 종료 씬 플레이어 상태
    /// </summary>
    public class PlayerState_Win : PlayerState, IPlayerState
    {
        public PlayerState_Win(PlayerController player)
        {
            player.ChangeTriggerAnimation(Define.PlayerState.Win);  //애니메이션 변경
        }
        public void OnUpdate(PlayerController player)
        {
        }
    }
}