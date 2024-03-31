using UnityEngine;
namespace yb {
    public abstract class MeleeWeapon {
        public MeleeWeapon(Transform _center, PlayerController player) {
            _centerTransform = _center;
            _player = player;
            _currentAttackDelay = 0f;
        }
        protected static float _currentAttackDelay;
        protected float _attackDelay;
        protected float _realodTime;
        protected float _swingSpeed;
        protected float _defaultDamage;
        protected Transform _centerTransform;
        protected GameObject _weaponObject;
        protected PlayerController _player;
        protected abstract void ExitSwordAttack();
    }
}