using Photon.Pun;
using UnityEngine;

namespace yb
{
    /// <summary>
    /// ȹ�� ������ Rifle������
    /// </summary>
    public class ObtainableRifle : MonoBehaviourPunCallbacks, IObtainableObject, IObtainableObjectPhoton
    {//0411 07:56 ����� MonoBehaviour -> MonoBehaviourPunCallbacks ���� ����
        private PhotonView _photonView;//0411 08:55 ����� ����ȭ�� ���� ����� �߰�
        public string Name => gameObject.name;
        public string NamePhoton => gameObject.name;
        public PhotonView IObtainableObjectPhotonView => _photonView;

     


        /// <summary>
        /// ������ �Ⱦ� ��, �÷��̾��� ���⸦ �� ���������� ��ü
        /// </summary>
        /// 

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
    }
}
