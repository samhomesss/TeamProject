using UnityEngine;
using yb;

namespace yb {
    public class RifleProjectileCreator : IProjectileCreator {
        public void Create(int defaultDamage, float projectileSpeed, Vector3 targetPos, Vector3 createPos) {
            ProjectileController projectile = Managers.Resources.Instantiate("yb/Projectile/RifleProjectile", null).GetComponent<ProjectileController>();
            projectile.Init(defaultDamage, projectileSpeed, targetPos, createPos);
        }
    }
}
