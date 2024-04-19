using DG.Tweening;
using Photon.Realtime;
using UnityEngine;
using yb;
using static yb.IRangedWeapon;
namespace yb
{
    /// <summary>
    /// Pistol Ŭ����
    /// </summary>
    public class RangedWeapon_Pistol : RangedWeapon, IRangedWeapon
    {
        public RangedWeapon_Pistol(Transform parent, PlayerController player) : base()
        {
            DefaultScale = new Vector3(.4f, .4f, .4f);  //���� �⺻ ũ��
            WeaponType = Define.WeaponType.Pistol;  //���� Ÿ�� ����
            _projectileCreator = new PistolProjectileCreator();  //���� �߻�ü ���� Ŭ���� �Ҵ�
            _weaponGameObject = Util.FindChild(parent.gameObject, "Pistol", false);  //���� ������Ʈ ��ġ
            _firePos = Util.FindChild(_weaponGameObject, "FirePos", false).transform;  //�߻�ü �߻� ��ġ ��ġ
            _weaponGameObject.transform.localScale = DefaultScale;  //������ ũ�⸦ �⺻ ũ��� �Ҵ�
            _player = player;
            _player.WeaponEvent?.Invoke(WeaponType.ToString()); // ������ ���� ��

            //���� ������ �⺻ ���Ⱦ� �°� �Ҵ�
            LimitRange = _data.DefaultProjectileRnage((int)WeaponType);
            _realodTime = _data.DefaultWeaponRealodTime((int)WeaponType);
            DefaultDamage = _data.DefaultWeaponDamage((int)WeaponType);
            _projectileVelocity = _data.DefaultWeaponVelocity((int)WeaponType);
            _remainBullet = _data.DefaultWeaponRemainBullet((int)WeaponType);
            _maxBullet = _data.DefaultWeaponMaxBullet((int)WeaponType);
            MaxDelay = _data.DefaultWeaponDelay((int)WeaponType);
            _currentBullet = _remainBullet;

            _player.BulletEvent?.Invoke(_currentBullet, _maxBullet);


            _saveMaxBullet = _maxBullet;
            OnUpdateRelic(player);  //�������� ���� ȿ�� �ο�
        }

        public Define.WeaponType WeaponType { get; set; }  //���� Ÿ��
        public Vector3 DefaultScale { get; set; }  //���� �⺻ ũ��

        /// <summary>
        /// ������ ������ �����ΰ�?
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
        /// ������(�ִϸ��̼� �̺�Ʈ�� ȣ��)
        /// </summary>
        /// <param name="player"></param>
        public void Reload(PlayerController player)
        {
            if (_remainBullet >= _maxBullet)
            {
                _currentBullet = _remainBullet;
                _maxBullet -= _remainBullet;
            }

            if (_remainBullet < _maxBullet)
            {
                _currentBullet = _remainBullet;
                _maxBullet -= _remainBullet;
            }

            Debug.Log($"���� �Ѿ� ��{_maxBullet}");
            _player.BulletEvent?.Invoke(_currentBullet, _maxBullet);
            player.StateController.ChangeState(new PlayerState_Idle(player));
        }

        /// <summary>
        /// ���� ���ݼӵ� ���
        /// </summary>
        public void OnUpdate()
        {
            _currentDelay += Time.deltaTime;
            //Debug.Log($"���ݼӵ��� {_maxDelay + _bonusAttackDelay}��"); 0412 07:42 ����� ������ �αװ� �Ⱥ����� �ּ�ó�� 
            //Debug.Log($"���� ������ {_currentDelay}"); 0412 07:42 ����� ������ �αװ� �Ⱥ����� �ּ�ó�� 

        }

        /// <summary>
        /// �߻簡 ������ �����ΰ�?
        /// </summary>
        /// <returns></returns>
        public bool CanShot()
        {
            if (_currentDelay >= MaxDelay + _bonusAttackDelay && !_player.Animator.GetCurrentAnimatorStateInfo(0).IsName("Shot"))
            {
                _currentDelay = 0f;
                return true;
            }
            return false;
        }

        /// <summary>
        /// �߻� �Լ�(�ִϸ��̼� �̺�Ʈ�� ȣ��)
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
                CoroutineHelper.Instance.ProjectileCreate(i, () => _projectileCreator.Create(DefaultDamage, _projectileVelocity, targetPos, _firePos.position, player, LimitRange));

            player.MyCamera.transform.DOShakeRotation(0.2f, 1f);
        }

        /// <summary>
        /// ���� ���� �� ���Ž� ȣ��
        /// </summary>
        /// <param name="player"></param>
        public void OnUpdateRelic(PlayerController player)
        {
            _relics = player.PickupController.IsRelic();

            for (int i = 0; i < _relics.Length; i++)
            {
                if (_relics[i])
                {
                    //���� �߰�
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
                    //���� ����
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
