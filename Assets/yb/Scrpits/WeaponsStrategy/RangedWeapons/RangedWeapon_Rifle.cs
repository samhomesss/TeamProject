using DG.Tweening;
using UnityEngine;
using static yb.IRangedWeapon;

namespace yb {
    public class RangedWeapon_Rifle : RangedWeapon, IRangedWeapon {
        public RangedWeapon_Rifle(Transform parent) : base() {
            DefaultScale = new Vector3(.4f, .4f, .4f);
            WeaponType = Define.weaponType.Rifle;
            _projectileCreator = new RifleProjectileCreator();
            _weaponGameObject = Util.FindChild(parent.gameObject, "Rifle", false);
            _firePos = Util.FindChild(_weaponGameObject, "FirePos", false).transform;

            _realodTime = _data.DefaultWeaponRealodTime((int)WeaponType);
            _defaultDamage = _data.DefaultWeaponDamage((int)WeaponType);
            _projectileVelocity = _data.DefaultWeaponVelocity((int)WeaponType);
            _remainBullet = _data.DefaultWeaponRemainBullet((int)WeaponType);
            _maxBullet = _data.DefaultWeaponMaxBullet((int)WeaponType);
            _maxDelay = _data.DefaultWeaponDelay((int)WeaponType);
            _currentBullet = _remainBullet;

        }

        public Define.weaponType WeaponType { get; set; }
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

            _projectileCreator.Create(_defaultDamage, _projectileVelocity, targetPos, _firePos.position, player);
        }
    }
}