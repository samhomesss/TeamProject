using UnityEditor;
using UnityEngine;
using yb;

namespace yb {
    /// <summary>
    /// todo �̸� ���� �ʿ�
    /// �� Ŭ��������, ���� PickUp�� ����ϰ� ����
    /// �߰� ����ü ���� Ŭ����
    /// </summary>
    public class BonusAttackSpeedRelic : ObtainableObject, IRelic {
        public string Name => gameObject.name;

        public Define.RelicType RelicType { get; } = Define.RelicType.BonusAttackSpeedRelic;

        public void DeleteRelic(PlayerController player) {
            player.PickupController.DeleteRelic(this);  //���� ����
            player.DestroyRelicEvent?.Invoke((int)RelicType);
        }

        public override void Pickup(PlayerController player) {
            SetRelic(player);  //�� �������� ������ ���� �Ҵ�
        }

        public void SetRelic(PlayerController player) {
            player.PickupController.SetRelic(this);
            player.SetRelicEvent?.Invoke((int)RelicType);
            Managers.Resources.Destroy(gameObject);
        }

        public override void ShowName(PlayerController player)
        {
            base.ShowName(player);
        }
    }
}
