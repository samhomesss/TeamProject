using Photon.Pun;
using UnityEngine;

namespace yb
{
    /// <summary>
    /// 획득 가능한 Shotgun아이템
    /// </summary>
    /// 
    public class Shotgun : ObtainableObject
    {//0411 07:57 이희웅 MonoBehaviour -> MonoBehaviourPunCallbacks 으로 수정
       //0411 08:55 이희웅 동기화를 위한 포톤뷰 추가

        /// <summary>
        /// 아이템 픽업 시, 플레이어의 무기를 이 아이템으로 교체
        /// </summary>
        /// 
        private void Start() => _photonView = GetComponent<PhotonView>();
        public override void Pickup(PlayerController player)
        {
            player.WeaponController.ChangeRangedWeapon(new RangedWeapon_Shotgun(player.WeaponController.RangedWeaponsParent, player));
            if (IsTestMode.Instance.CurrentUser == Define.User.Hw)
            {
                    PhotonNetwork.Destroy(gameObject);
            }
            else
            {
                Managers.Resources.Destroy(gameObject);
            }
        }

        [PunRPC]
        public override void PickupPhoton(int playerViewId)
        {
            Debug.Log("줍는 RPC콜");
            PlayerController player;
            player = PhotonNetwork.GetPhotonView(playerViewId).GetComponent<PlayerController>();
            player.WeaponController.ChangeRangedWeapon(new RangedWeapon_Shotgun(player.WeaponController.RangedWeaponsParent, player));
            if (PhotonNetwork.IsMasterClient)
                PhotonNetwork.Destroy(gameObject);

        }

        public override void ShowName(PlayerController player)
        {
            base.ShowName(player);
        }

        public override void HideName()
        {
            base.HideName();
        }

    }
}