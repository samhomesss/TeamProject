using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using Unity.VisualScripting;
using UnityEngine;

namespace yb {
    /// <summary>
    /// ȹ�� ������ Pistol������
    /// </summary>
    public class ObtainablePistol : MonoBehaviourPunCallbacks, IObtainableObject
    {//0411 07:49 ����� MonoBehaviour -> MonoBehaviourPunCallbacks ���� ����
        private PhotonView _photonView; //0411 08:55 ����� ����ȭ�� ���� ����� �߰�
        public PhotonView IObtainableObjectPhotonView => _photonView;
        public string Name => gameObject.name;
        public string NamePhoton => gameObject.name;

        /// <summary>
        /// ������ �Ⱦ� ��, �÷��̾��� ���⸦ �� ���������� ��ü
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