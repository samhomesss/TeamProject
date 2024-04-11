using Photon.Pun;
using UnityEditor;
using UnityEngine;
using yb;

namespace yb {
    public class ShieldRelic : ObtainableObject, IRelic {

        public string Name => gameObject.name;
        private void Start() => _photonView = GetComponent<PhotonView>();
        public Define.RelicType RelicType { get; } = Define.RelicType.ShieldRelic;

        public void DeleteRelic(PlayerController player) {
            player.PickupController.DeleteRelic(this);
        }

        public override void Pickup(PlayerController player) {
            SetRelic(player);
        }

        [PunRPC]
        public override void PickupPhoton(int playerViewId)
        {
            PlayerController player;
            player = PhotonNetwork.GetPhotonView(playerViewId).GetComponent<PlayerController>();
            SetRelic(player);
        }

        public void SetRelic(PlayerController player) {
            player.PickupController.SetRelic(this);
            Managers.Resources.Destroy(gameObject);

        }

        public override void ShowName()
        {
            base.ShowName();
        }
    }
}
