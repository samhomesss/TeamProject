using ExitGames.Client.Photon;
using Photon.Pun;
using UnityEditor;
using UnityEngine;
using yb;

namespace yb {
    /// <summary>
    /// todo 이름 변경 필요
    /// 이 클래스에서, 렐릭 PickUp도 담당하고 있음
    /// 추가 투사체 렐릭 클래스
    /// </summary>
    public class BonusAttackSpeedRelic : ObtainableObject, IRelic {
        public string Name => gameObject.name;

        public Define.RelicType RelicType { get; } = Define.RelicType.BonusAttackSpeedRelic;

        private void Start() => _photonView = GetComponent<PhotonView>();
        public Transform MyTransform => transform;

        public PhotonView PhotonView => _photonView;

        public void DeleteRelic(PlayerController player) {
            player.PickupController.DeleteRelic(this);
            player.HaveRelicNumber--;
           // player.DestroyRelicEvent?.Invoke(RelicType.ToString() , () => player.PickupController.DeleteRelic(this) , () => player.HaveRelicNumber--);
        }

        public override void Pickup(PlayerController player) {
            SetRelic(player);  //이 아이템을 주으면 렐릭 할당
            player.HaveRelicNumber++;
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
        public override void PickupPhoton(int playerViewId)
        {
            PlayerController player;
            player = PhotonNetwork.GetPhotonView(playerViewId).GetComponent<PlayerController>();
            player.PickupController.SetRelic(this);
            player.HaveRelicNumber++;

            if (player.PhotonView.IsMine)
                player.ChangeRelicIMGEvent.Invoke(RelicType.ToString(), () => { }, () => { });
            if (PhotonNetwork.IsMasterClient)
                PhotonNetwork.Destroy(gameObject);
        }
    }
}
