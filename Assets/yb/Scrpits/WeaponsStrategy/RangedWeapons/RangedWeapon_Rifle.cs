using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using static yb.IRangedWeapon;

namespace yb {
    public class RangedWeapon_Rifle : RangedWeapon, IRangedWeapon {
        public RangedWeapon_Rifle(Transform parent, PlayerController player) : base() {
            DefaultScale = new Vector3(.4f, .4f, .4f);
            WeaponType = Define.WeaponType.Rifle;
            _projectileCreator = new RifleProjectileCreator();
            _weaponGameObject = Util.FindChild(parent.gameObject, "Rifle", false);
            _firePos = Util.FindChild(_weaponGameObject, "FirePos", false).transform;
            _player = player;
            _player.WeaponEvent?.Invoke(WeaponType.ToString());

            _realodTime = _data.DefaultWeaponRealodTime((int)WeaponType);
            _defaultDamage = _data.DefaultWeaponDamage((int)WeaponType);
            _projectileVelocity = _data.DefaultWeaponVelocity((int)WeaponType);
            _remainBullet = _data.DefaultWeaponRemainBullet((int)WeaponType);
            _maxBullet = _data.DefaultWeaponMaxBullet((int)WeaponType);
            _maxDelay = _data.DefaultWeaponDelay((int)WeaponType);
            _currentBullet = _remainBullet;

            OnUpdateRelic(player);
        }

        public Define.WeaponType WeaponType { get; set; }
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
            _player.WeaponEvent?.Invoke(WeaponType.ToString());

            player.StateController.ChangeState(new PlayerState_Idle(player));
        }
        public void OnUpdate() {
            _currentDelay += Time.deltaTime;
        }

        public bool CanShot() {
            if (_currentDelay >= _maxDelay + _bonusAttackDelay) {
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
                player.StateController.ChangeState(new PlayerState_Reload(player, this));
                return;
            }

            _currentBullet--;
          //  _player.PlayerEvent.Item2.Invoke(_currentBullet, _maxBullet);

            int projectileNumber = Random.Range(0, 1f) > _data.BonusProjectileChance((int)WeaponType) ? 1 : Mathf.Max(_bonusProjectile, 1);

            for (int i = 0; i < projectileNumber; i++)
                _projectileCreator.Create(_defaultDamage, _projectileVelocity, targetPos, _firePos.position, player);

            player.MyCamera.transform.DOShakeRotation(0.2f, 0.5f);

        }

        public void OnUpdateRelic(PlayerController player) {
            _relics = player.PickupController.IsRelic();

            for (int i = 0; i < _relics.Length; i++) {
                if (_relics[i]) {
                    //·¼¸¯ Ãß°¡
                    switch (i) {
                        case (int)Define.RelicType.BonusAttackSpeedRelic:
                            _bonusAttackDelay = -_data.BonusAttackDelay((int)WeaponType);
                            break;
                        case (int)Define.RelicType.BonusProjectileRelic:
                            _bonusProjectile = _data.BonusProjectile((int)WeaponType);
                            break;
                    }
                    continue;
                }

                if (!_relics[i]) {
                    //·¼¸¯ Á¦°Å
                    switch (i) {
                        case (int)Define.RelicType.BonusAttackSpeedRelic:
                            _bonusAttackDelay = 0f;
                            break;
                        case (int)Define.RelicType.BonusProjectileRelic:
                            _bonusProjectile = 0;
                            break;
                    }
                    continue;
                }
            }
        }
    }
}