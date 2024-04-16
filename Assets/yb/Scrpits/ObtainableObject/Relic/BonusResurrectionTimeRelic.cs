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
            SetRelic(player);
            player.HaveRelicNumber++;
        }


        public void SetRelic(PlayerController player) {
            if (IsTestMode.Instance.CurrentUser == Define.User.Hw)
            {
                player.SetRelicEvent?.Invoke(RelicType.ToString(), () => player.PickupController.SetRelic(this), () => PhotonManager.Destroy(gameObject));
            }
            else
            {
                player.SetRelicEvent?.Invoke(RelicType.ToString(), () => player.PickupController.SetRelic(this), () => Managers.Resources.Destroy(gameObject));
            }

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
