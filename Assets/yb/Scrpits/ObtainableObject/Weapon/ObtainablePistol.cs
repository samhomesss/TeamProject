using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using Unity.VisualScripting;
using UnityEditor.Timeline;
using UnityEngine;

namespace yb {
    // Interface�� ��� �޴� �θ� Ŭ������ �޾ƿͼ� �ϴ°� ���� ���ƺ��̱���
    /// <summary>
    /// ȹ�� ������ Pistol������
    /// </summary>

    public class ObtainablePistol : ObtainableObject
    {//0411 07:49 ����� MonoBehaviour -> MonoBehaviourPunCallbacks ���� ����
     //0411 08:55 ����� ����ȭ�� ���� ����� �߰�

        /// <summary>
        /// ������ �Ⱦ� ��, �÷��̾��� ���⸦ �� ���������� ��ü
        /// </summary>
        /// <param name="player"></param>

        private void Start() => _photonView = GetComponent<PhotonView>();

        public override void Pickup(PlayerController player) {
            player.WeaponController.ChangeRangedWeapon(new RangedWeapon_Pistol(player.WeaponController.RangedWeaponsParent, player));

            if(IsTestMode.Instance.CurrentUser == Define.User.Hw)
            {
               PhotonNetwork.Destroy(gameObject);
            }
            else
            {
                Managers.Resources.Destroy(gameObject);
            }
        }

        [PunRPC]
        public override void PickupPhoton(int playerViewId)
        {
            PlayerController player;
            player = PhotonNetwork.GetPhotonView(playerViewId).GetComponent<PlayerController>();
            player.WeaponController.ChangeRangedWeapon(new RangedWeapon_Pistol(player.WeaponController.RangedWeaponsParent, player));
            if (PhotonNetwork.IsMasterClient)
                PhotonNetwork.Destroy(gameObject);
       
        }

        public override void ShowName(PlayerController player)
        {
            base.ShowName(player);
        }

        public override void HideName()
        {
            base.HideName();
        }
    }
}