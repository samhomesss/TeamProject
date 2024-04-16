using Photon.Pun;
using Photon.Realtime;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor;
using UnityEngine;
using yb;

namespace yb
{
    public class ShieldRelic : ObtainableObject, IRelic
    {

        public string Name => gameObject.name;
        private void Start() => _photonView = GetComponent<PhotonView>();
        public Define.RelicType RelicType { get; } = Define.RelicType.ShieldRelic;

        public Transform MyTransform => transform;
        public PhotonView PhotonView => _photonView;
        public void DeleteRelic(PlayerController player)
        {
            player.PickupController.DeleteRelic(this);
            player.HaveRelicNumber--;
            //player.DestroyRelicEvent?.Invoke(RelicType.ToString(), () => player.PickupController.DeleteRelic(this), () => player.HaveRelicNumber--);
        }

        public override void Pickup(PlayerController player)
        {
            SetRelic(player);
            player.HaveRelicNumber++;
        }

        [PunRPC]
        public override void PickupPhoton(int playerViewId)
        {
            Debug.Log("���巼�� ����");
            PlayerController player;
            player = PhotonNetwork.GetPhotonView(playerViewId).GetComponent<PlayerController>();
            player.PickupController.SetRelic(this);
            player.HaveRelicNumber++;
            if (player.PhotonView.IsMine)
                player.ChangeRelicIMGEvent.Invoke(RelicType.ToString(), () => { }, () => { });
            if (PhotonNetwork.IsMasterClient)
                PhotonNetwork.Destroy(gameObject);



        }

        public void SetRelic(PlayerController player)
        {
            #region ���� ��� ����
            //if (IsTestMode.Instance.CurrentUser == Define.User.Hw)
            //{
            //    Debug.Log("IsTestMode.Instance.CurrentUser == Define.User.Hw");
            //    //player.SetRelicEvent?.Invoke(RelicType.ToString(), () => player.PickupController.SetRelic(this), () => { });
            //    player.SetRelicEvent?.Invoke(RelicType.ToString(), () => player.PickupController.SetRelic(this), () =>
            //    {
            //        if (PhotonNetwork.IsMasterClient)
            //            PhotonNetwork.Destroy(gameObject);//���� ������ ������Ʈ ����
            //    });
            //}
            //else
            //{
            //   Debug.Log("else");
            //   player.SetRelicEvent?.Invoke(RelicType.ToString(), () => player.PickupController.SetRelic(this), () => Managers.Resources.Destroy(gameObject));
            //}
            #endregion
            player.ChangeRelicIMGEvent.Invoke(RelicType.ToString(), () => { }, () => { });

        }

        public override void ShowName(PlayerController player)
        {
            base.ShowName(player);
        }

        public override void HideName()
        {
            base.HideName();
        }

        [PunRPC]
        public void SetDropItemName(int dropObjectViewId)//0414 ����� ���� ��������� �̸�������
        {
            PhotonView _photonView = PhotonNetwork.GetPhotonView(dropObjectViewId);

            int index = _photonView.transform.gameObject.name.IndexOf("(Clone)");
            if (index > 0)
                _photonView.transform.gameObject.name = _photonView.transform.gameObject.name.Substring(0, index);
        }
    }
}
