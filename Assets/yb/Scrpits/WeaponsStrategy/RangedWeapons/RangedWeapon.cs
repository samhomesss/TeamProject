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
        //29가 currentbullet
        //180이 _maxBullet

        //현재 총알 수
        protected int _currentBullet;

        //장전 수 
        protected int _remainBullet;

        //최대 장전 수
        protected int _maxBullet;

        //공격속도 렐릭을 먹었을 시 빨라지는 공격 딜레
        protected float _bonusAttackDelay;

        //추가 투사체 렐릭을 먹었을시 추가되는 발사체의 수
        protected int _bonusProjectile;
        protected IProjectileCreator _projectileCreator;
        protected PlayerController _player;
        protected Transform _firePos;
        protected GameObject _weaponGameObject;
        protected bool isShot;
        protected bool[] _relics = new bool[(int)Define.RelicType.Count];

        
    }
}
