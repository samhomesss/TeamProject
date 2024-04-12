using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using Unity.VisualScripting;
using UnityEditor.Timeline;
using UnityEngine;

namespace yb {
    // Interface를 상속 받는 부모 클래스를 받아와서 하는게 제일 좋아보이긴함
    /// <summary>
    /// 획득 가능한 Pistol아이템
    /// </summary>

    public class ObtainablePistol : ObtainableObject
    {//0411 07:49 이희웅 MonoBehaviour -> MonoBehaviourPunCallbacks 으로 수정
     //0411 08:55 이희웅 동기화를 위한 포톤뷰 추가

        /// <summary>
        /// 아이템 픽업 시, 플레이어의 무기를 이 아이템으로 교체
        /// </summary>
        /// <param name="player"></param>

        private void Start() => _photonView = GetComponent<PhotonView>();

        public override void Pickup(PlayerController player) {
            player.WeaponController.ChangeRangedWeapon(new RangedWeapon_Pistol(player.WeaponController.RangedWeaponsParent, player));

            if(IsTestMode.Instance.CurrentUser == Define.User.Hw)
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
            PlayerController player;
            player = PhotonNetwork.GetPhotonView(playerViewId).GetComponent<PlayerController>();
            player.WeaponController.ChangeRangedWeapon(new RangedWeapon_Pistol(player.WeaponController.RangedWeaponsParent, player));
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