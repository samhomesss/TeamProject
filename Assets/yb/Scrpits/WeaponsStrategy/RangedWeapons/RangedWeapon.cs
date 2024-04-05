using UnityEngine;
using DG.Tweening;
namespace yb {
    public class  RangedWeapon{
        public RangedWeapon() {
            _data = Managers.Data;
            
        }
        
        protected Data _data;
        protected float _currentDelay;
        protected float _maxDelay;
        protected float _realodTime;
        protected float _projectileVelocity;
        protected int _defaultDamage;

        // 29 / 180 
        //29�� currentbullet
        //180�� _maxBullet

        //���� �Ѿ� ��
        protected int _currentBullet;

        //���� �� 
        protected int _remainBullet;

        //�ִ� ���� ��
        protected int _maxBullet;

        //���ݼӵ� ������ �Ծ��� �� �������� ���� ����
        protected float _bonusAttackDelay;

        //�߰� ����ü ������ �Ծ����� �߰��Ǵ� �߻�ü�� ��
        protected int _bonusProjectile;
        protected IProjectileCreator _projectileCreator;
        protected Transform _firePos;
        protected GameObject _weaponGameObject;
        protected bool isShot;
        protected bool[] _relics = new bool[(int)Define.RelicType.Count];

        
    }
}