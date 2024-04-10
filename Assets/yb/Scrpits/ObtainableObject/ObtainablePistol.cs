using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace yb {
    /// <summary>
    /// 획득 가능한 Pistol아이템
    /// </summary>
    public class ObtainablePistol : MonoBehaviourPunCallbacks, IObtainableObject, IObtainableObjectPhoton
    {//0411 07:49 이희웅 MonoBehaviour -> MonoBehaviourPunCallbacks 으로 수정
        public string Name => gameObject.name;

        public string NamePhoton => gameObject.name;

        /// <summary>
        /// 아이템 픽업 시, 플레이어의 무기를 이 아이템으로 교체
        /// </summary>
        /// <param name="player"></param>
        public void Pickup(PlayerController player) {
            player.WeaponController.ChangeRangedWeapon(new RangedWeapon_Pistol(player.WeaponController.RangedWeaponsParent, player));
            Managers.Resources.Destroy(gameObject);
        }

        [PunRPC]
        public void PickupPhoton(int PlayerViewID)//0411 07:53 이희웅 포톤용 RPC메서드 추가
        {
            PlayerController player;
            player = PhotonNetwork.GetPhotonView(PlayerViewID).GetComponent<PlayerController>();
            player.WeaponController.ChangeRangedWeapon(new RangedWeapon_Pistol(player.WeaponController.RangedWeaponsParent, player));
            Managers.Resources.Destroy(gameObject);
        }
    }
}