<img width="1219" alt="스크린샷 2024-05-02 201050" src="https://github.com/samhomesss/TeamProject/assets/159544864/fc0728a4-56a8-4bb9-b9a3-070a4c2561d3">

## **📃핵심 기술**

### ・상태 패턴을 활용한 체계적인 플레이어 행동 관리

🤔**WHY?**

플레이어의 행동을 플레이어 쪽에서 모아서 관리해 가시성이 떨어지고, 다른 팀원이 수정을 하려할 때 코드의 내용을 전체적으로 파악하고 있어야 해 효율이 크게 떨어짐

🤔**HOW?**

 관련 코드

- PlayerStateController
    
    ```csharp
     public class PlayerStateController : MonoBehaviour {
             private IPlayerState _playerState;  //현재 플레이어의 상태
             
             void Update() {
                if(_playerState != null)
                     _playerState.OnUpdate(_player);
    
                
            }
            public void ChangeState(IPlayerState playerState) { 
    		    _playerState = playerState;
        
    		}
    }
    ```
    
- IPlayerState
    
    ```csharp
    public interface IPlayerState {
        void OnUpdate(PlayerController player);  //현재 상태의 맞는 행동 Update
    }
    ```
    
- PlayerState
    
    ```csharp
    public class PlayerState {
        protected Data _data;  //기본 정보 변수
    
        public PlayerState() {
            _data = Managers.Data;  //상태가 바뀔 시 기본 정보 호출
        }
    }
    ```
    
- PlayerState_~
    
    ```csharp
    public class PlayerState_Idle : PlayerState, IPlayerState
    {
        public PlayerState_Idle(PlayerController player)
        {
            player.ChangeBoolAnimation("Idle");  //애니메이션 변경
        }
        public void OnUpdate(PlayerController player)
        {
            if (player.StateController.State is PlayerState_Die or PlayerState_Win)
                return;
    
            if (Input.GetMouseButtonDown(0))
            {
                player.StateController.ChangeState(new PlayerState_Shot(player));  //마우스 클릭시 ShotState로 변경
                return;
            }
            if (Input.GetKeyDown(KeyCode.R) && player.WeaponController.RangedWeapon.CanReload())
            {
                //장전 가능한 상태일때 R버튼 입력 시 ReloadState로 변경
                player.StateController.ChangeState(new PlayerState_Reload(player, player.WeaponController.RangedWeapon));  
                return;
            }
    
            if (player.isMoving())
            {
                //이동중일 시 MoveState로 변경
                player.StateController.ChangeState(new PlayerState_Move(player));
                return;
            }
        }
    }
    ```
    

🤓**Result!**

플레이어의 각 상태에 맞는 행동을 분리해, 가시성이 높아지고 제3자의 수정, 추가, 제거가 원활해짐과 동시에 유지보수성이 크게 증가함

### ・플레이어와 관련된 모든 로직을 컴포지트 패턴을 이용해 분할

🤔**WHY?**

사운드 출력, 아이템 획득, 무기 등등 플레이어의 로직들을 플레이어 내부 코드에서 모두 처리해, 코드가 코드가 길어지고 가시성이 떨어지고, 다른 팀원의 코드 해석이 점점 힘들어짐

🤔**HOW?**

 관련 코드

- PlayerController
    
    ```csharp
     public class PlayerController : MonoBehaviour, ITakeDamage, ITakeDamagePhoton
    { // 플레이어의 모든 로직들을 별개의 클래스로 분할
    		private PlayerAudioController _audioController;
    		private PlayerPickupController _pickupController;
    		private PlayerStateController _stateController;
    		private PlayerWeaponController _weaponController;
    		private RotateToMouseScript _rotateToMouseScript;
    		private PlayerStatus _status; 
    		private PlayerGuardController _guardController;
    		private PlayerShieldController _shieldController;
    }
    ```
    

🤓**Result!**

플레이어에 관련된 모든 로직을 클래스별로 나누고 플레이어 쪽에서 선언하여 사용해, 가시성이 높아지고 클래스간의 결합도 하락 및 다른 팀원도 쉽게 수정이 가능

### ・옵저버 패턴을 이용한 UI 시스템

🤔**WHY?**

 데이터의 변경이 없음에도 주기적으로 UI에 데이터를 동기화해, 필요 없는 작업이 지속적으로 반복되어 결과적으로 퍼포먼스 하락

🤔**HOW?**

 관련 코드

- PlayerController
    
    ```csharp
    public class PlayerController : MonoBehaviour, ITakeDamage, ITakeDamagePhoton
    {
    	 public Action<int, Item> SetItemEvent;
    
    	 public Action<int, int> HpEvent;
    
    	 public Action<int, int> BulletEvent;
    
    	 public Action<string> WeaponEvent;
    
    	 public Action<string, UnityAction, UnityAction> SetRelicEvent;
    
    	 public Action<string, UnityAction, UnityAction> ChangeRelicIMGEvent;
    
    	 public Action<string> ItemEvent;
    
    	 public Action MapEvent;
    
    	 public Action<string, UnityAction, UnityAction> DestroyRelicEvent;
    
    	 public Action ColorPercentEvent;
    
    	 public Action ClosedItemEvent;
    }
    ```
    
- UI_Weapon
    
    ```csharp
    public class UI_Weapon : UI_Scene
    {
        
        public override void Init()
        {
            base.Init();
    
            map = Map.MapObject.GetComponent<Map>();
            _photonView = map.Player.PhotonView;
    
            if (_photonView.IsMine)
                SetPlayer(map.Player);
    
        }
           void SetPlayer(PlayerController player)
       {
           player.BulletEvent += BulletCount;
           player.WeaponEvent += ChangeWeapon;
       }
    }
    ```
    

🤓**Result!**

게임이 시작될 때, 각 플레이어가 자신의 UI에 자신의 데이터를 연동해,, 실질적인 데이터의 변경이 발생할 때에만 UI에 데이터를 동기화해, 필요 없는 작업을 배제해 퍼포먼스 상승

### ・컴포지트 패턴과 전략 패턴을 이용한 추상화된 무기 관리 시스템

🤔**WHY?**

무기의 종류가 많아지고, 각 무기를 각각 구현해 중복된 코드를 작성하는 일이 잦아지고, 무기의 종류가 늘어나거나 줄어들 때 마다 직접적으로 플레이어 쪽 코드의 수정이 필요, 유지보수가  굉장히 힘든 문제가 발생

🤔**HOW?**

 관련 코드

- IRangedWeapon
    
    ```csharp
    public interface IRangedWeapon {
        public Define.WeaponType WeaponType { get; set; }  //무기의 타입
    
        public Vector3 DefaultScale { get; set; }  //무기 오브젝트의 기본 크기
        void Shot(Vector3 targetPos,  PlayerController player);  //발사 함수
    
        void OnUpdateRelic(PlayerController player);  //무기 변경 시, 렐릭 효과 업데이트
    
        void Reload(PlayerController player);  //무기 재장전
    
        bool CanReload();  //무기가 재장전 가능한가?
    
        void OnUpdate();  //무기 관련 Update함수
    
        bool CanShot();  //발사가 가능한가?
    
    }
    ```
    
- RangedWeapon
    
    ```csharp
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
    
         public float LimitRange { get; protected set; }
         public int RemainBullet => _remainBullet;
         
    
         public int CurrentBullet => _currentBullet;
         public int MaxBullet => _maxBullet;
         //현재 총알 수
         protected int _currentBullet;
    
         //장전 수 
         protected int _remainBullet;
    
         //최대 장전 수
         protected int _maxBullet;
    
         protected int _saveMaxBullet;
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
    
         public void InitBullet() {
             _maxBullet = _saveMaxBullet;
             _currentBullet = RemainBullet;
         }
     }
    ```
    
- RangedWeapon_
    
    ```csharp
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
            _player.WeaponEvent?.Invoke(WeaponType.ToString()); // 아이템 생성 됨
    
            //각종 스탯을 기본 스탯애 맞게 할당
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
                _maxBullet -= _remainBullet;
            }
    
            if (_remainBullet < _maxBullet)
            {
                _currentBullet = _remainBullet;
                _maxBullet -= _remainBullet;
            }
    
            Debug.Log($"남은 총알 수{_maxBullet}");
            _player.BulletEvent?.Invoke(_currentBullet, _maxBullet);
            player.StateController.ChangeState(new PlayerState_Idle(player));
        }
    
        /// <summary>
        /// 무기 공격속도 계산
        /// </summary>
        public void OnUpdate()
        {
            _currentDelay += Time.deltaTime;
            //Debug.Log($"공격속도는 {_maxDelay + _bonusAttackDelay}초"); 0412 07:42 이희웅 찍어놓은 로그가 안보여서 주석처리 
            //Debug.Log($"현재 딜레이 {_currentDelay}"); 0412 07:42 이희웅 찍어놓은 로그가 안보여서 주석처리 
    
        }
    
        /// <summary>
        /// 발사가 가능한 상태인가?
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
                CoroutineHelper.Instance.ProjectileCreate(i, () => _projectileCreator.Create(DefaultDamage, _projectileVelocity, targetPos, _firePos.position, player, LimitRange));
    
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
    ```
    
- PlayerWeaponController
    
    ```csharp
    public class PlayerWeaponController: MonoBehaviour
    {
    		 private IRangedWeapon _rangeWeapon;  //현재 플레이어가 장착중인 무기
    
         private void Update() => _rangeWeapon.OnUpdate();  //장착중인 무기에 맞는 Update함수 호출
    }
    ```
    

🤓**Result!**

무기의 특징들만을 추상화해, 플레이어는 추상화된 무기의 특징을 호출하는 것 만으로 실질적인 무기의 기능을 사용할 수 있게 되어, 무기에 변화가 생겨도 플레이어 쪽 코드의 수정이 불필요하게 되어 유지보수가 용이해짐.

### ・풀링 오브젝트 시스템

🤔**WHY?**

각종 오브젝트를 필요할 때 마다 생성, 필요가 없어지면 제거해 짧은 시간 내에 다량의 객체를 생성하는 상황이 발생 시 심각한 퍼포먼스 하락 발생

🤔**HOW?**

 관련 코드

- PoolManager
    
    ```csharp
    using System.Collections.Generic;
    using Unity.VisualScripting;
    using UnityEngine;
    
    public class PoolManager
    {
        class Pool
        {
            public GameObject Original { get; private set; }
            public Transform Root { get; private set; }
            private Stack<Poolable> _poolStack = new Stack<Poolable>();
    
            public void Init(GameObject original, int count = 5)
            {
                Original = original;
                Root = new GameObject().transform;
                Root.name = original.name + "_root";
    
                for (int i = 0; i < count; i++)
                {
                    Release(Create());
                }
            }
    
            
    
            private Poolable Create()
            {
                GameObject go = Object.Instantiate(Original);
                go.name = Original.name;
                return go.GetOrAddComponent<Poolable>();
            }
    
            public void Release(Poolable poolable)
            {
                if (poolable == null)
                    return;
    
                poolable.transform.parent = Root;
                poolable.gameObject.SetActive(false);
                _poolStack.Push(poolable);
            }
    
            public Poolable Activation()
            {
                Poolable poolable;
    
                if (_poolStack.Count > 0)
                    poolable = _poolStack.Pop();
                else
                    poolable = Create();
                
                poolable.gameObject.SetActive(true);
    
                poolable.transform.parent = Managers.Scene.CurrentSceneManager.transform;
    
                poolable.transform.parent = Root;
    
                return poolable;
            }
    
        }
        
        private Transform _root;
        private Dictionary<string, Pool> _pools = new Dictionary<string, Pool>();
    
        public void Init()
        {
            if (_root == null)
            {
                _root = new GameObject("@Pool_root").transform;
                Object.DontDestroyOnLoad(_root.GameObject());
            }
        }
    
        public void Release(Poolable poolable)
        {
            string name = poolable.gameObject.name;
    
            if (_pools.ContainsKey(name) == false)
            {
                Object.Destroy(poolable.gameObject);
                return;
            }
            
            _pools[name].Release(poolable);
        }
    
        public void Clear() {
            foreach(Transform t in _root) {
                Managers.Resources.Destroy(t.gameObject);
            }
            _root = null;
            _pools.Clear();
        }
    
        public Poolable Activation(GameObject original)
        {
            if(_pools.ContainsKey(original.name) == false)
                CreatePool(original);
    
            return _pools[original.name].Activation();
        }
    
        private void CreatePool(GameObject original, int count = 5)
        {
            Pool pool = new Pool();
            pool.Init(original,count);
            pool.Root.parent = _root;
            
            _pools.Add(original.name, pool);
        }
    
        public GameObject GetOriginal(string name)
        {
            if (_pools.ContainsKey(name) == false)
                return null;
    
            return _pools[name].Original;
        }
    }
    ```
    

🤓**Result!**

  객체의 직접적인 생성 / 파괴를 최대한 피하고 풀링 시스템을 이용, 이미 생성된 객체를 재사용하는 과정을 통해 객체의 생성에 들어가는 비용을 줄여 퍼포먼스 상승
