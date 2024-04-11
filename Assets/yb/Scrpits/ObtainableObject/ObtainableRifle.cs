using Photon.Pun;
using UnityEngine;

namespace yb
{
    /// <summary>
    /// 획득 가능한 Rifle아이템
    /// </summary>
    public class ObtainableRifle : MonoBehaviourPunCallbacks, IObtainableObject , IObtainableObjectPhoton
    {//0411 07:56 이희웅 MonoBehaviour -> MonoBehaviourPunCallbacks 으로 수정
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
            player.WeaponController.ChangeRangedWeapon(new RangedWeapon_Rifle(player.WeaponController.RangedWeaponsParent, player));
            Managers.Resources.Destroy(gameObject);
        }

        [PunRPC]
        public void PickupPhoton(int PlayerViewID)//0411 07:56 이희웅 포톤용 RPC메서드 추가
        {
            PlayerController player;
            player = PhotonNetwork.GetPhotonView(PlayerViewID).gameObject.GetComponent<PlayerController>();
            player.WeaponController.ChangeRangedWeapon(new RangedWeapon_Rifle(player.WeaponController.RangedWeaponsParent, player));
            Managers.Resources.Destroy(gameObject);
        }
    }
}
