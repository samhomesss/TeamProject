using UnityEngine;
using DG.Tweening;
namespace yb {
    public class RangedWeapon {
        public RangedWeapon(Transform parent) {

        }
        protected float _currentDelay;
        protected float _maxDelay;
        protected float _realodTime;
        protected float _bulletSpeed;
        protected int _defaultDamage;
        protected int _currentBullet;
        protected int _remainBullet;
        protected int _maxBullet;
        protected IProjectileCreator _projectileCreator;
        protected Transform _firePos;
        protected GameObject _weaponGameObject;
        protected bool isShot;

    }
}
