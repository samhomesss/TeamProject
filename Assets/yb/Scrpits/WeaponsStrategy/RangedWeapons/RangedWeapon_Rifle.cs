using UnityEngine;

namespace yb {
    public class RangedWeapon_Rifle : RangedWeapon, IRangedWeapon {
        public RangedWeapon_Rifle(Transform parent) : base(parent) {
            _projectileCreator = new RifleProjectileCreator();
            _weaponGameObject = Util.FindChild(parent.gameObject, "Rifle", false);
            _firePos = Util.FindChild(_weaponGameObject, "FirePos", false).transform;

            //todo:test
            _defaultDamage = 5;
            _bulletSpeed = 10f;
            _shotDelay = 0.5f;
            _remainBullet = 30;
            _maxBullet = 120;
            _currentBullet = _remainBullet;
            //todo
            //대충 무기 바꾸는 애니메이션
        }
        public void OnUpdate() => _currentShotDelay += Time.deltaTime;

        public void Shot(Vector3 targetPos, PlayerController player) {
            if (_currentBullet == 0) {
                player.ChangeState(new PlayerState_Reload(player, this));
                return;
            }
            if (_currentShotDelay < _shotDelay)
                return;

            _currentShotDelay = 0f;
            _projectileCreator.Create(_defaultDamage, _bulletSpeed, targetPos, _firePos.position);
        }
    }
}