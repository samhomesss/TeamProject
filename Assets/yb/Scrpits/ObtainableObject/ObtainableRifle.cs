using Photon.Pun;
using UnityEngine;

namespace yb
{
    /// <summary>
    /// ȹ�� ������ Rifle������
    /// </summary>
    public class ObtainableRifle : MonoBehaviourPunCallbacks, IObtainableObject , IObtainableObjectPhoton
    {//0411 07:56 ����� MonoBehaviour -> MonoBehaviourPunCallbacks ���� ����
        public string Name => gameObject.name;

        public string NamePhoton => gameObject.name;

        /// <summary>
        /// ������ �Ⱦ� ��, �÷��̾��� ���⸦ �� ���������� ��ü
        /// </summary>
        public void Pickup(PlayerController player)
        {
            player.WeaponController.ChangeRangedWeapon(new RangedWeapon_Rifle(player.WeaponController.RangedWeaponsParent, player));
            Managers.Resources.Destroy(gameObject);
        }

        [PunRPC]
        public void PickupPhoton(int PlayerViewID)//0411 07:56 ����� ����� RPC�޼��� �߰�
        {
            PlayerController player;
            player = PhotonNetwork.GetPhotonView(PlayerViewID).GetComponent<PlayerController>();
            player.WeaponController.ChangeRangedWeapon(new RangedWeapon_Rifle(player.WeaponController.RangedWeaponsParent, player));
            Managers.Resources.Destroy(gameObject);
        }
    }
}
