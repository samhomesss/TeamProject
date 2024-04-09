using UnityEditor;
using UnityEngine;
using yb;

namespace yb {
    /// <summary>
    /// todo 이름 변경 필요
    /// 이 클래스에서, 렐릭 PickUp도 담당하고 있음
    /// 추가 투사체 렐릭 클래스
    /// </summary>
    public class BonusAttackSpeedRelic : MonoBehaviour, IRelic, IObtainableObject {
        public string Name => gameObject.name;

        public Define.RelicType RelicType { get; } = Define.RelicType.BonusAttackSpeedRelic;

        public void DeleteRelic(PlayerController player) {
            player.PickupController.DeleteRelic(this);  //렐릭 삭제
        }

        public void Pickup(PlayerController player) {
            SetRelic(player);  //이 아이템을 주으면 렐릭 할당
        }

        public void SetRelic(PlayerController player) {
            player.PickupController.SetRelic(this);
            Managers.Resources.Destroy(gameObject);
        }
    }
}
