using UnityEngine;
using DG.Tweening;
namespace yb {
    /// <summary>
    /// 무기 관련 클래스
    /// </summary>
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
        protected IProjectileCreator _projectileCreator;  //발사체 생성용 변수
        protected PlayerController _player;
        protected Transform _firePos;  //발사체 생성 위치 
        protected GameObject _weaponGameObject;  //무기 오브젝트 저장용 변수
        protected bool isShot;
        protected bool[] _relics = new bool[(int)Define.RelicType.Count];  //보유 렐릭

        
    }
}
