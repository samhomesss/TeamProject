using UnityEditor;
using UnityEngine;
using yb;

namespace yb {
    public class BonusProjectileRelic : ObtainableObject, IRelic{
        public string Name => gameObject.name;

        public Define.RelicType RelicType { get; } = Define.RelicType.BonusProjectileRelic;

        public void DeleteRelic(PlayerController player) {
            player.PickupController.DeleteRelic(this);
            player.DestroyRelicEvent?.Invoke(RelicType.ToString());
        }

        public override void Pickup(PlayerController player) {
            SetRelic(player);
        }

        public void SetRelic(PlayerController player) {
            player.PickupController.SetRelic(this);
            player.SetRelicEvent?.Invoke(RelicType.ToString());
            Managers.Resources.Destroy(gameObject);

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
