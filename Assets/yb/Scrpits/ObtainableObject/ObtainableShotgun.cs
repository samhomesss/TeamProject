using Photon.Pun;
using UnityEngine;

namespace yb
{
    /// <summary>
    /// 획득 가능한 Shotgun아이템
    /// </summary>
    public class ObtainableShotgun : MonoBehaviourPunCallbacks, IObtainableObject, IObtainableObjectPhoton
    {//0411 07:57 이희웅 MonoBehaviour -> MonoBehaviourPunCallbacks 으로 수정
        private PhotonView _photonView;//0411 08:55 이희웅 동기화를 위한 포톤뷰 추가
        public PhotonView IObtainableObjectPhotonView => _photonView;
        public string Name => gameObject.name;
        public string NamePhoton => gameObject.name;

        /// <summary>
        /// 아이템 픽업 시, 플레이어의 무기를 이 아이템으로 교체
        /// </summary>
        /// 
        private void Start() => _photonView = GetComponent<PhotonView>();
        public void Pickup(PlayerController player)
        {
            player.WeaponController.ChangeRangedWeapon(new RangedWeapon_Shotgun(player.WeaponController.RangedWeaponsParent, player));
            Managers.Resources.Destroy(gameObject);
        }

        [PunRPC]
        public void PickupPhoton(int PlayerViewID)//0411 07:58 이희웅 포톤용 RPC메서드 추가
        {
            PlayerController player;
            player = PhotonNetwork.GetPhotonView(PlayerViewID).GetComponent<PlayerController>();
            player.WeaponController.ChangeRangedWeapon(new RangedWeapon_Shotgun(player.WeaponController.RangedWeaponsParent, player));
            Managers.Resources.Destroy(gameObject);
        }
    }
}