using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;

namespace yb
{
    /// <summary>
    /// ���� ���� �� �÷��̾� ����
    /// </summary>
    public class PlayerState_Win : PlayerState, IPlayerState
    {
        public PlayerState_Win(PlayerController player)
        {
            player.Animator.SetTrigger("Win");
            //player.ChangeTriggerAnimation(Define.PlayerState.Win);  //�ִϸ��̼� ����
        }
        public void OnUpdate(PlayerController player)
        {
        }
    }
}