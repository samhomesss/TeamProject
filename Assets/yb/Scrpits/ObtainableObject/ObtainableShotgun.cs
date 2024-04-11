using Photon.Pun;
using UnityEngine;

namespace yb
{
    /// <summary>
    /// ȹ�� ������ Shotgun������
    /// </summary>
    public class ObtainableShotgun : ObtainableObject
    {//0411 07:57 ����� MonoBehaviour -> MonoBehaviourPunCallbacks ���� ����
       //0411 08:55 ����� ����ȭ�� ���� ����� �߰�

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
                player.WeaponEvent?.Invoke(52);
                Managers.Resources.Destroy(gameObject);
            }
        }

        [PunRPC]
        public override void PickupPhoton(int playerViewId)
        {
            PlayerController player;
            player = PhotonNetwork.GetPhotonView(playerViewId).GetComponent<PlayerController>();
            player.WeaponController.ChangeRangedWeapon(new RangedWeapon_Shotgun(player.WeaponController.RangedWeaponsParent, player));
            if (PhotonNetwork.IsMasterClient)
                PhotonNetwork.Destroy(gameObject);

        }

        public override void ShowName()
        {
            base.ShowName();
        }
    }
}