using Photon.Pun;
using UnityEditor;
using UnityEngine;
using yb;

namespace yb {
    public class ShieldRelic : MonoBehaviour, IRelic, IObtainableObject {
        public string Name => gameObject.name;

        public Define.RelicType RelicType { get; } = Define.RelicType.ShieldRelic;

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
