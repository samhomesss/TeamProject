using Photon.Pun;
using UnityEditor;
using UnityEngine;
using yb;

namespace yb {
    public class GuardRelic : ObtainableObject, IRelic {
        public string Name => gameObject.name;

        public Define.RelicType RelicType { get; } = Define.RelicType.GuardRelic;


        public void DeleteRelic(PlayerController player) {
            player.PickupController.DeleteRelic(this);
            player.HaveRelicNumber--;
        }

        public override void Pickup(PlayerController player) {
            SetRelic(player);
            player.HaveRelicNumber++;
        }

        public void SetRelic(PlayerController player) {
            player.PickupController.SetRelic(this);
            player.RelicEvent?.Invoke(1001);
            Managers.Resources.Destroy(gameObject);

        }

        public override void ShowName()
        {
            base.ShowName();
        }
    }
}
