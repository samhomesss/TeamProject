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


        public void DeleteRelic(PlayerController player) {
            player.PickupController.DeleteRelic(this);
            player.HaveRelicNumber--;
           // player.DestroyRelicEvent?.Invoke(RelicType.ToString() , () => player.PickupController.DeleteRelic(this) , () => player.HaveRelicNumber--);
        }

        public override void Pickup(PlayerController player) {
            SetRelic(player);  //이 아이템을 주으면 렐릭 할당
            player.HaveRelicNumber++;
        }

        [PunRPC]
        public override void PickupPhoton(int playerViewId)
        {
            PlayerController player;
            player = PhotonNetwork.GetPhotonView(playerViewId).GetComponent<PlayerController>();
            SetRelic(player);
        }

        public void SetRelic(PlayerController player) {
            player.SetRelicEvent?.Invoke(RelicType.ToString() , () => player.PickupController.SetRelic(this) , () => Managers.Resources.Destroy(gameObject));
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
