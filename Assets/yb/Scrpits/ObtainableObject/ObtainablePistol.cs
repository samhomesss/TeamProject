using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using Unity.VisualScripting;
using UnityEngine;

namespace yb {
    /// <summary>
    /// 획득 가능한 Pistol아이템
    /// </summary>
    public class ObtainablePistol : MonoBehaviourPunCallbacks, IObtainableObject
    {//0411 07:49 이희웅 MonoBehaviour -> MonoBehaviourPunCallbacks 으로 수정
        private PhotonView _photonView; //0411 08:55 이희웅 동기화를 위한 포톤뷰 추가
        public PhotonView IObtainableObjectPhotonView => _photonView;
        public string Name => gameObject.name;
        public string NamePhoton => gameObject.name;

        /// <summary>
        /// 아이템 픽업 시, 플레이어의 무기를 이 아이템으로 교체
        /// </summary>
        /// <param name="player"></param>

        private void Start()
        {
            _photonView = GetComponent<PhotonView>();
        }
        public void Pickup(PlayerController player) {
            player.WeaponController.ChangeRangedWeapon(new RangedWeapon_Pistol(player.WeaponController.RangedWeaponsParent, player));
            if(IsTestMode.Instance.CurrentUser == Define.User.Hw)
            {
                //GetComponent<PhotonView>().TransferOwnership(player.GetComponent<PhotonView>().ViewID);
                    PhotonNetwork.Destroy(gameObject);
            }
            else
            {
                Managers.Resources.Destroy(gameObject);
            }
        }

    }
}