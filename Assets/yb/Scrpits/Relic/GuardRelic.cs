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
            // 아래에 있는 이벤트 새로 만들기 
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
