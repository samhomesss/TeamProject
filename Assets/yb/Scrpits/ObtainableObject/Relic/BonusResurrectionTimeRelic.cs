using Photon.Pun;
using UnityEditor;
using UnityEngine;
using yb;

namespace yb {
    public class BonusResurrectionTimeRelic : ObtainableObject, IRelic {
        public string Name => gameObject.name;


        public Define.RelicType RelicType { get; } = Define.RelicType.BonusResurrectionTimeRelic;

        private void Start() => _photonView = GetComponent<PhotonView>();
        public Transform MyTransform => transform;

        public void DeleteRelic(PlayerController player) {
            player.PickupController.DeleteRelic(this);
            player.HaveRelicNumber--;
            //player.DestroyRelicEvent?.Invoke(RelicType.ToString() , () => player.PickupController.DeleteRelic(this) , () => player.HaveRelicNumber--);   
        }

        public override void Pickup(PlayerController player) {
            SetRelic(player);
            player.HaveRelicNumber++;
        }
        [PunRPC]
        public override void PickupPhoton(int playerViewId)
        {
            PlayerController player;
            player = PhotonNetwork.GetPhotonView(playerViewId).GetComponent<PlayerController>();
            player.PickupController.SetRelic(this);
            player.HaveRelicNumber++;
            if (PhotonNetwork.IsMasterClient)
                PhotonNetwork.Destroy(gameObject);


            player.SetRelicEvent?.Invoke(RelicType.ToString(), () => { }, () => { });
        }

        public void SetRelic(PlayerController player)
        {
            #region 현재 사용 안함
            //if (IsTestMode.Instance.CurrentUser == Define.User.Hw)
            //{
            //    Debug.Log("IsTestMode.Instance.CurrentUser == Define.User.Hw");
            //    //player.SetRelicEvent?.Invoke(RelicType.ToString(), () => player.PickupController.SetRelic(this), () => { });
            //    player.SetRelicEvent?.Invoke(RelicType.ToString(), () => player.PickupController.SetRelic(this), () =>
            //    {
            //        if (PhotonNetwork.IsMasterClient)
            //            PhotonNetwork.Destroy(gameObject);//땅에 떨어진 오브젝트 삭제
            //    });
            //}
            //else
            //{
            //   Debug.Log("else");
            //   player.SetRelicEvent?.Invoke(RelicType.ToString(), () => player.PickupController.SetRelic(this), () => Managers.Resources.Destroy(gameObject));
            //}
            #endregion
            player.SetRelicEvent?.Invoke(RelicType.ToString(), () => player.PickupController.SetRelic(this), () => Managers.Resources.Destroy(gameObject));

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
