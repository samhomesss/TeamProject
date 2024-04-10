using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace yb {
    /// <summary>
    /// ȹ�� ������ Pistol������
    /// </summary>
    public class ObtainablePistol : MonoBehaviourPunCallbacks, IObtainableObject, IObtainableObjectPhoton
    {//0411 07:49 ����� MonoBehaviour -> MonoBehaviourPunCallbacks ���� ����
        public string Name => gameObject.name;

        public string NamePhoton => gameObject.name;

        /// <summary>
        /// ������ �Ⱦ� ��, �÷��̾��� ���⸦ �� ���������� ��ü
        /// </summary>
        /// <param name="player"></param>
        public void Pickup(PlayerController player) {
            player.WeaponController.ChangeRangedWeapon(new RangedWeapon_Pistol(player.WeaponController.RangedWeaponsParent, player));
            Managers.Resources.Destroy(gameObject);
        }

        [PunRPC]
        public void PickupPhoton(int PlayerViewID)//0411 07:53 ����� ����� RPC�޼��� �߰�
        {
            PlayerController player;
            player = PhotonNetwork.GetPhotonView(PlayerViewID).GetComponent<PlayerController>();
            player.WeaponController.ChangeRangedWeapon(new RangedWeapon_Pistol(player.WeaponController.RangedWeaponsParent, player));
            Managers.Resources.Destroy(gameObject);
        }
    }
}