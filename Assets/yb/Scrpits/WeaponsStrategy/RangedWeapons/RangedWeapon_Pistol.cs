using DG.Tweening;
using Photon.Realtime;
using UnityEngine;
using yb;
using static yb.IRangedWeapon;
namespace yb
{
    /// <summary>
    /// Pistol 클래스
    /// </summary>
    public class RangedWeapon_Pistol : RangedWeapon, IRangedWeapon
    {
        public RangedWeapon_Pistol(Transform parent, PlayerController player) : base()
        {
            DefaultScale = new Vector3(.4f, .4f, .4f);  //무기 기본 크기
            WeaponType = Define.WeaponType.Pistol;  //무기 타입 지정
            _projectileCreator = new PistolProjectileCreator();  //무기 발사체 생성 클래스 할당
            _weaponGameObject = Util.FindChild(parent.gameObject, "Pistol", false);  //무기 오브젝트 서치
            _firePos = Util.FindChild(_weaponGameObject, "FirePos", false).transform;  //발사체 발사 위치 서치
            _weaponGameObject.transform.localScale = DefaultScale;  //무기의 크기를 기본 크기로 할당
            _player = player;
            _player.WeaponEvent?.Invoke((int)WeaponType);

            //각종 스탯을 기본 스탯애 맞게 할당
            _realodTime = _data.DefaultWeaponRealodTime((int)WeaponType);
            _defaultDamage = _data.DefaultWeaponDamage((int)WeaponType);
            _projectileVelocity = _data.DefaultWeaponVelocity((int)WeaponType);
            _remainBullet = _data.DefaultWeaponRemainBullet((int)WeaponType);
            _maxBullet = _data.DefaultWeaponMaxBullet((int)WeaponType);
            _maxDelay = _data.DefaultWeaponDelay((int)WeaponType);
            _currentBullet = _remainBullet;

            OnUpdateRelic(player);  //보유중인 렐릭 효과 부여
        }

        public Define.WeaponType WeaponType { get; set; }  //무기 타입
        public Vector3 DefaultScale { get; set; }  //무기 기본 크기

        /// <summary>
        /// 재장전 가능한 상태인가?
        /// </summary>
        /// <returns></returns>
        public bool CanReload()
        {
            if (_currentBullet == _remainBullet)
                return false;

            if (_maxBullet == 0)
                return false;

            return true;
        }

        /// <summary>
        /// 재장전(애니메이션 이벤트로 호출)
        /// </summary>
        /// <param name="player"></param>
        public void Reload(PlayerController player)
        {
            if (_remainBullet >= _maxBullet)
            {
                _currentBullet = _remainBullet;
                _maxBullet -= 0;
                return;
            }

            if (_remainBullet < _maxBullet)
            {
                _currentBullet = _remainBullet;
                _maxBullet -= _remainBullet;
            }
            _player.BulletEvent?.Invoke(_currentBullet, _maxBullet);
            player.StateController.ChangeState(new PlayerState_Idle(player));
        }

        /// <summary>
        /// 무기 공격속도 계산
        /// </summary>
        public void OnUpdate()
        {
            _currentDelay += Time.deltaTime;
        }

        /// <summary>
        /// 발사가 가능한 상태인가?
        /// </summary>
        /// <returns></returns>
        public bool CanShot()
        {
            if (_currentDelay >= _maxDelay + _bonusAttackDelay && !_player.Animator.GetCurrentAnimatorStateInfo(0).IsName("Shot"))
            {
                _currentDelay = 0f;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 발사 함수(애니메이션 이벤트로 호출)
        /// </summary>
        /// <param name="targetPos"></param>
        /// <param name="player"></param>
        public void Shot(Vector3 targetPos, PlayerController player)
        {

            if (_currentBullet == 0)
            {
                player.StateController.ChangeState(new PlayerState_Reload(player, this));
                return;
            }

            _currentBullet--;
            _player.BulletEvent?.Invoke(_currentBullet, _maxBullet);

            int projectileNumber = Random.Range(0, 1f) > _data.BonusProjectileChance((int)WeaponType) ? 1 : Mathf.Max(_bonusProjectile, 1);

            for (int i = 0; i < projectileNumber; i++)
                _projectileCreator.Create(_defaultDamage, _projectileVelocity, targetPos, _firePos.position, player);


            player.MyCamera.transform.DOShakeRotation(0.2f, 1f);
        }

        /// <summary>
        /// 렐릭 습득 및 제거시 호출
        /// </summary>
        /// <param name="player"></param>
        public void OnUpdateRelic(PlayerController player)
        {
            _relics = player.PickupController.IsRelic();

            for (int i = 0; i < _relics.Length; i++)
            {
                if (_relics[i])
                {
                    //렐릭 추가
                    switch (i)
                    {
                        case (int)Define.RelicType.BonusAttackSpeedRelic:
                            _bonusAttackDelay = -_data.BonusAttackDelay((int)WeaponType);
                            break;
                        case (int)Define.RelicType.BonusProjectileRelic:
                            _bonusProjectile = _data.BonusProjectile((int)WeaponType);
                            break;
                    }
                    continue;
                }
                if (!_relics[i])
                {
                    //렐릭 제거
                    switch (i)
                    {
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
