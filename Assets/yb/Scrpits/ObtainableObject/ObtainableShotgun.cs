using Photon.Pun;
using UnityEngine;

namespace yb
{
    /// <summary>
    /// ȹ�� ������ Shotgun������
    /// </summary>
    public class ObtainableShotgun : MonoBehaviourPunCallbacks, IObtainableObject
    {//0411 07:57 ����� MonoBehaviour -> MonoBehaviourPunCallbacks ���� ����
        private PhotonView _photonView;//0411 08:55 ����� ����ȭ�� ���� ����� �߰�
        public PhotonView IObtainableObjectPhotonView => _photonView;
        public string Name => gameObject.name;
        public string NamePhoton => gameObject.name;

        /// <summary>
        /// ������ �Ⱦ� ��, �÷��̾��� ���⸦ �� ���������� ��ü
        /// </summary>
        /// 
        private void Start() => _photonView = GetComponent<PhotonView>();
        public void Pickup(PlayerController player)
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
    }
}