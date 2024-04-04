using UnityEditor;
using UnityEngine;
using yb;

namespace yb {
    public class GuardRelic : MonoBehaviour, IRelic, IObtainableObject {
        public Define.RelicType RelicType { get; } = Define.RelicType.GuardRelic;

        public void DeleteRelic(PlayerController player) {
            player.DeleteRelic(this);
        }

        public void Pickup(PlayerController player) {
            SetRelic(player);
        }

        public void SetRelic(PlayerController player) {
            player.SetRelic(this);
        }
    }
}
