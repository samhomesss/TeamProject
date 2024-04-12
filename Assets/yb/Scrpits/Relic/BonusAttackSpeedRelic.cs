using Photon.Pun;
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

        public Transform MyTransform => transform;

        public void DeleteRelic(PlayerController player) {
            player.PickupController.DeleteRelic(this);
            player.HaveRelicNumber--;
           // player.DestroyRelicEvent?.Invoke(RelicType.ToString() , () => player.PickupController.DeleteRelic(this) , () => player.HaveRelicNumber--);
        }

        public override void Pickup(PlayerController player) {
            SetRelic(player);  //�� �������� ������ ���� �Ҵ�
            player.HaveRelicNumber++;
        }

        public void SetRelic(PlayerController player) {
            player.SetRelicEvent?.Invoke(RelicType.ToString() , () => player.PickupController.SetRelic(this) , () => Managers.Resources.Destroy(gameObject));
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
