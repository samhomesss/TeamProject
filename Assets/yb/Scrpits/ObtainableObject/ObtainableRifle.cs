using Photon.Pun;
using UnityEngine;

namespace yb
{
    /// <summary>
    /// ȹ�� ������ Rifle������
    /// </summary>
    public class ObtainableRifle : MonoBehaviourPunCallbacks, IObtainableObject
    {//0411 07:56 ����� MonoBehaviour -> MonoBehaviourPunCallbacks ���� ����
        private PhotonView _photonView;//0411 08:55 ����� ����ȭ�� ���� ����� �߰�
        public string Name => gameObject.name;

        public PhotonView iObtainableObjectPhotonview => _photonView;


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
                //GetComponent<PhotonView>().TransferOwnership(player.GetComponent<PhotonView>().ViewID);
                //if (!PhotonNetwork.IsMasterClient)
                //{
                //    Debug.Log("Ŭ���̾�Ʈ ȣ��");
                //    return;
                //}
                //else
                //    Debug.Log("�����Ͱ� ������");
                //}
                //
                //{
                //if(player.PhotonView.IsMine)
                    PhotonNetwork.Destroy(gameObject);
            }
            else
            {
                Managers.Resources.Destroy(gameObject);
            }
        }
    }
}
