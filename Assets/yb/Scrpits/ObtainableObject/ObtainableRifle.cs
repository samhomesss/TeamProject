using Photon.Pun;
using UnityEngine;

namespace yb
{
    /// <summary>
    /// 획득 가능한 Rifle아이템
    /// </summary>
    public class ObtainableRifle : ObtainableObject
    {//0411 07:56 이희웅 MonoBehaviour -> MonoBehaviourPunCallbacks 으로 수정
        private PhotonView _photonView;//0411 08:55 이희웅 동기화를 위한 포톤뷰 추가
        public string Name => gameObject.name;
        public string NamePhoton => gameObject.name;
        public PhotonView IObtainableObjectPhotonView => _photonView;

        /// <summary>
        /// 아이템 픽업 시, 플레이어의 무기를 이 아이템으로 교체
        /// </summary>
        private void Start() => _photonView = GetComponent<PhotonView>();
        public void Pickup(PlayerController player)
        {
            player.WeaponController.ChangeRangedWeapon(new RangedWeapon_Rifle(player.WeaponController.RangedWeaponsParent, player));
            if (IsTestMode.Instance.CurrentUser == Define.User.Hw)
            {
                    PhotonNetwork.Destroy(gameObject);
            }
            else
            {
                player.WeaponEvent?.Invoke(51);
                Managers.Resources.Destroy(gameObject);
            }
        }
        [PunRPC]
        public void PickupPhoton(int playerViewId)
        {
            PlayerController player;
            player = PhotonNetwork.GetPhotonView(playerViewId).GetComponent<PlayerController>();
            player.WeaponController.ChangeRangedWeapon(new RangedWeapon_Rifle(player.WeaponController.RangedWeaponsParent, player));
            if (PhotonNetwork.IsMasterClient)
                PhotonNetwork.Destroy(gameObject);
        }

        public override void ShowName()
        {
            base.ShowName();
        }
    }
}
