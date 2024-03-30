using UnityEngine;
namespace yb {
    public interface IProjectileCreator {
        void Create(int defaultDamage, float projectileSpeed, Vector3 targetPos, Vector3 createPos);
    }
}
