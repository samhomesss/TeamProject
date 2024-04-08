using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;

namespace yb
{
    public class PlayerState_Idle : PlayerState, IPlayerState
    {
       PhotonView _photonView;
        public PlayerState_Idle(PlayerController player)
        {
            player.ChangeFadeAnimation("Idle");
            _photonView = player.PhotonView;
        }
        public void OnUpdate(PlayerController player)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if(_photonView.IsMine)
                player.StateController.ChangeState(new PlayerState_Shot(player));
                return;
            }



            if (Input.GetKeyDown(KeyCode.R) && player.WeaponController.RangedWeapon.CanReload())
            {
                player.StateController.ChangeState(new PlayerState_Reload(player, player.WeaponController.RangedWeapon));
                return;
            }

            if (player.isMoving())
            {
                player.StateController.ChangeState(new PlayerState_Move(player));
                return;
            }
        }
    }
}