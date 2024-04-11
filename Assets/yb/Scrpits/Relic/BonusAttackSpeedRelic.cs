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
    public class BonusAttackSpeedRelic : MonoBehaviour, IRelic, IObtainableObject {
        public string Name => gameObject.name;

        public Define.RelicType RelicType { get; } = Define.RelicType.BonusAttackSpeedRelic;


        public void DeleteRelic(PlayerController player) {
            player.PickupController.DeleteRelic(this);  //���� ����
        }

        public void Pickup(PlayerController player) {
            SetRelic(player);  //�� �������� ������ ���� �Ҵ�
        }

        public void SetRelic(PlayerController player) {
            player.PickupController.SetRelic(this);
            Managers.Resources.Destroy(gameObject);
        }
    }
}
