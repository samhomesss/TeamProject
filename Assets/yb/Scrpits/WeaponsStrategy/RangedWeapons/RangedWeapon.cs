using UnityEngine;
namespace yb {
    public class RangedWeapon {
        public RangedWeapon(Transform parent) {
            _currentShotDelay = 0;

        }
        protected static float _currentShotDelay;
        protected float _shotDelay;
        protected float _realodTime;
        protected float _bulletSpeed;
        protected int _defaultDamage;
        protected int _currentBullet;
        protected int _remainBullet;
        protected int _maxBullet;
        protected IProjectileCreator _projectileCreator;
        protected Transform _firePos;
        protected GameObject _weaponGameObject;
    }
}
