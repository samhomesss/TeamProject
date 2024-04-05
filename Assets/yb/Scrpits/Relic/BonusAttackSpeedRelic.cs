using UnityEditor;
using UnityEngine;
using yb;

namespace yb {
    public class BonusAttackSpeedRelic : MonoBehaviour, IRelic, IObtainableObject {
        public Define.RelicType RelicType { get; } = Define.RelicType.BonusAttackSpeedRelic;

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