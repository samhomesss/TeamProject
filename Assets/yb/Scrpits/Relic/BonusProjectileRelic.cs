using Photon.Pun;
using UnityEditor;
using UnityEngine;
using yb;

namespace yb {
    public class BonusProjectileRelic : MonoBehaviour, IRelic, IObtainableObject {
        public string Name => gameObject.name;

        public Define.RelicType RelicType { get; } = Define.RelicType.BonusProjectileRelic;

        public PhotonView iObtainableObjectPhotonview => throw new System.NotImplementedException();

        public void DeleteRelic(PlayerController player) {
            player.PickupController.DeleteRelic(this);
        }

        public void Pickup(PlayerController player) {
            SetRelic(player);
        }

        public void SetRelic(PlayerController player) {
            player.PickupController.SetRelic(this);
            Managers.Resources.Destroy(gameObject);

        }
    }
}
