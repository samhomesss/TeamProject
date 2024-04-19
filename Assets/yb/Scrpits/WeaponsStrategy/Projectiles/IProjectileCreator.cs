using UnityEngine;
namespace yb {
    /// <summary>
    /// �߻�ü ���� �������̽�
    /// </summary>
    public interface IProjectileCreator {
        void Create(int defaultDamage, float projectileSpeed, Vector3 targetPos, Vector3 createPos, PlayerController player, float range);  //�߻�ü ����
    }
}
