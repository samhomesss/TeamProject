using UnityEngine;
namespace yb {
    /// <summary>
    /// ������ ��� ���� �������̽�
    /// </summary>
    public interface IItemDroplable {
        void Set(string item);  //����� ������ ����
        void Drop(Vector3 pos);  //������ ������ ���
    }
}
