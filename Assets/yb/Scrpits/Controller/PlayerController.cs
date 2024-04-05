using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor;
using Photon.Pun;
using System;

namespace yb {
    public class PlayerController : MonoBehaviour, ITakeDamage {
        private readonly float _animationFadeTime = .3f;
        private Rigidbody _rigid;
        private Data _data;
        private float moveX;
        private float moveZ;
        private Camera _myCamera;
        private Collider _collider;
        private Animator _animator;
        private PlayerPickupController _pickupController;
        private PlayerStateController _stateController;
        private PlayerWeaponController _weaponController;
        private RotateToMouseScript _rotateToMouseScript;
        private IItemDroplable _droplable = new ItemDroplable();
        private PlayerStatus _status;
        private PhotonView _photonview; //0405 09:41분 이희웅 캐릭터간에 동기화를 위한 포톤 뷰 추가

        //item1. 체력
        //item2. 총알
        //item3. 무기
        //item4. 렐릭
        //item5. 아이템
        //item6. 미니맵
        private Action<int, int> _hpEvent;
        private Action<int, int> _bulletEvent;
        private Action<int> _weaponEvent;
        private Action<int> _relicEvent;
        private Action<string> _itemEvent;
        private Action _miniMapEvent;

        private  Tuple<Action<int, int>, Action<int, int>, Action<int>,
            Action<int>, Action<string>, Action> _playerEvent;

        public  Tuple<Action<int, int>, Action<int, int>, Action<int>,
            Action<int>, Action<string>, Action> PlayerEvent => _playerEvent;

        /// <summary>
        /// 스탯,능력치 클래스
        /// 타고 들어가면 private로 플레이어의 정보들이 변수로 생성되어있음.
        /// 필요시 get 프로퍼티 생성 후 사용
        /// </summary>
        public PlayerStatus Status => _status;

        /// <summary>
        /// 플레이어 무기 클래스
        /// 내부의 IRangedWeapon 변수가 무기의 정보를 지니고있음
        /// 타고 들어가면 private로 무기의 정보들이 변수로 생성되어있음.
        /// 필요시 get 프로퍼티 생성 후 사용
        /// </summary>
        public PlayerWeaponController WeaponController => _weaponController;
        public PlayerPickupController PickupController => _pickupController;
        public PlayerStateController StateController => _stateController;
        public Camera MyCamera => _myCamera;
        public RotateToMouseScript RotateToMouseScript => _rotateToMouseScript;


        private void Awake() {
            _rigid = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();
            _animator = GetComponent<Animator>();
            _status = GetComponent<PlayerStatus>();
            _rotateToMouseScript = GetComponent<RotateToMouseScript>();
            _photonview = GetComponent<PhotonView>();
            _weaponController = GetComponent<PlayerWeaponController>();
            _stateController = GetComponent<PlayerStateController>();
            _pickupController = GetComponent<PlayerPickupController>();
        }


        private void Start() {
            _data = Managers.Data;

            //test
            //사망시 set해둔 아이템 드랍
            _droplable.Set("ObtainableRifle");
            _droplable.Set("ObtainablePistol");
            _droplable.Set("ObtainableShotgun");

            _playerEvent = Tuple.Create(_hpEvent, _bulletEvent, _weaponEvent, _relicEvent, _itemEvent, _miniMapEvent);
        }


        private void Update() {
            moveX = Input.GetAxisRaw("Horizontal");
            moveZ = Input.GetAxisRaw("Vertical");
        }
        public void SetCamera(Camera camera) => _myCamera = camera;

        public bool isMoving() {
            if (moveX == 0 && moveZ == 0)
                return false;

            return true;
        }
        
        public void ChangeFadeAnimation(string animation) => _animator.CrossFade(animation, _animationFadeTime);
        public void ChangeIntigerAnimation(Define.PlayerState state) => _animator.SetInteger("State", (int)state);
        public void ChangeTriggerAnimation(Define.PlayerState state) => _animator.SetTrigger(state.ToString());

        /// <summary>
        /// 플레이어 이동 로직
        /// </summary>
        public void OnMoveUpdate() {
            if (_photonview.IsMine)//0405 09:41분 캐릭터간에 동기화를 위한 포톤 이동 분리 로직 추가
            {
                Vector3 dir = new Vector3(moveX, 0f, moveZ);
                _rigid.MovePosition(_rigid.position + dir * (_status.MoveSpeed * _status.MoveSpeedDecrease) * Time.deltaTime);
            }
        }

        public void OnDieUpdate(GameObject attacker) {
            transform.LookAt(attacker.transform.position);
            _collider.enabled = false;
            _rigid.isKinematic = true;
            ChangeTriggerAnimation(Define.PlayerState.Die);
        }

        public void OnDieEvent() =>  Managers.Resources.Destroy(gameObject);

        public void TakeDamage(int amout, GameObject attacker) {
            if (amout <= 0)
                return;

            int hp =_status.SetHp(-amout);
            _hpEvent?.Invoke(_status.CurrentHp, _status.MaxHp);
            if(hp <= 0) {
                _droplable.Drop(transform.position);
                _stateController.ChangeState(new PlayerState_Die(this, attacker));
            }
        }
    }
}

