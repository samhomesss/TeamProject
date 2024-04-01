using DG.Tweening;
using UnityEngine;
using static yb.IRangedWeapon;

namespace yb {
    public class RangedWeapon_Rifle : RangedWeapon, IRangedWeapon {
        public RangedWeapon_Rifle(Transform parent) : base(parent) {
            DefaultScale = new Vector3(.4f, .4f, .4f);
            WeaponType = weaponType.Rifle;
            _projectileCreator = new RifleProjectileCreator();
            _weaponGameObject = Util.FindChild(parent.gameObject, "Rifle", false);
            _firePos = Util.FindChild(_weaponGameObject, "FirePos", false).transform;

            _defaultDamage = 5;
            _bulletSpeed = 10f;
            _remainBullet = 30;
            _maxBullet = 120;
            _maxDelay = .5f;

            _currentBullet = _remainBullet;

        }

        public weaponType WeaponType { get; set; }
        public Vector3 DefaultScale { get; set; }

        public void Reload(PlayerController player) {
            if (_remainBullet >= _maxBullet) {
                _currentBullet = _remainBullet;
                _maxBullet -= 0;
                return;
            }

            if (_remainBullet < _maxBullet) {
                _currentBullet = _remainBullet;
                _maxBullet -= _remainBullet;
            }

            player.ChangeState(new PlayerState_Idle(player));
        }
        public void OnUpdate() {
            _currentDelay += Time.deltaTime;
        }

        public bool CanShot() {
            if (_currentDelay >= _maxDelay) {
                _currentDelay = 0f;
                return true;
            }
            return false;
        }

        public bool CanReload() {
            if (_currentBullet == _remainBullet)
                return false;

            if (_maxBullet == 0)
                return false;

            return true;
        }

        public void Shot(Vector3 targetPos, PlayerController player) {
            if (_currentBullet == 0) {
                player.ChangeState(new PlayerState_Reload(player, this));
                return;
            }

            _currentBullet--;
            Camera.main.transform.DOShakeRotation(0.2f, 0.5f);

            _projectileCreator.Create(_defaultDamage, _bulletSpeed, targetPos, _firePos.position, player);
        }
    }
}