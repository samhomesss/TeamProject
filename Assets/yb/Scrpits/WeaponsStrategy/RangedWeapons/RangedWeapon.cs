using UnityEngine;
using DG.Tweening;
namespace yb {
    /// <summary>
    /// ���� ���� Ŭ����
    /// </summary>
    public class  RangedWeapon{
        public RangedWeapon() {
            _data = Managers.Data;
        }
        
        protected Data _data;
        protected float _currentDelay;
        public float MaxDelay { get; set; }
        protected float _realodTime;
        protected float _projectileVelocity;
        public int DefaultDamage { get; set; }

        public int RemainBullet => _remainBullet;
        

        public int CurrentBullet => _currentBullet;
        public int MaxBullet => _maxBullet;
        //���� �Ѿ� ��
        protected int _currentBullet;

        //���� �� 
        protected int _remainBullet;

        //�ִ� ���� ��
        protected int _maxBullet;

        protected int _saveMaxBullet;
        //���ݼӵ� ������ �Ծ��� �� �������� ���� ����
        protected float _bonusAttackDelay;

        //�߰� ����ü ������ �Ծ����� �߰��Ǵ� �߻�ü�� ��
        protected int _bonusProjectile;
        protected IProjectileCreator _projectileCreator;  //�߻�ü ������ ����
        protected PlayerController _player;
        protected Transform _firePos;  //�߻�ü ���� ��ġ 
        protected GameObject _weaponGameObject;  //���� ������Ʈ ����� ����
        protected bool isShot;
        protected bool[] _relics = new bool[(int)Define.RelicType.Count];  //���� ����

        public void InitBullet() {
            _maxBullet = _saveMaxBullet;
            _currentBullet = RemainBullet;
        }
    }
}
