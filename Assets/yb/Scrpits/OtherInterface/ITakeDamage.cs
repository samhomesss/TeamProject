using UnityEngine;

namespace yb {
    /// <summary>
    /// �������� ���� �� �ִ� ��� ������Ʈ�� ��ӹ޾ƾ���.
    /// </summary>
    public interface ITakeDamage {
        void TakeDamage(int amout, GameObject attacker);
    }
}