using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor;
using Photon.Pun;

namespace yb {
    public class PlayerController : MonoBehaviour, ITakeDamage {
        private readonly float _animationFadeTime = .3f;
        private Rigidbody _rigid;
        private Data _data;
        private float moveX;
        private float moveZ;
        private Collider _collider;
        private Animator _animator;
        private Vector3 _mousePos;
        private Transform _rangedWeaponsParent;
        private IPlayerState _playerState;
        private IRangedWeapon _rangeWeapon;
        private IItemDroplable _droplable = new ItemDroplable();
        private IObtainableObject _collideItem;
        private bool[] _haveRelic = new bool[(int)Define.RelicType.Count];
        private PlayerStatus _status;
        private PhotonView _photonview; //0405 09:41분 이희웅 캐릭터간에 동기화를 위한 포톤 뷰 추가
        public PlayerStatus Status => _status;
        private RotateToMouseScript _rotateToMouseScript;
        public RotateToMouseScript RotateToMouseScript => _rotateToMouseScript;
        public IRangedWeapon RangedWeapon => _rangeWeapon;
        public Transform RangedWeaponsParent => _rangedWeaponsParent;


        private void Awake() {
            _rigid = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();
            _animator = GetComponent<Animator>();
            _status = GetComponent<PlayerStatus>();
            _rotateToMouseScript = GetComponent<RotateToMouseScript>();
            _photonview = GetComponent<PhotonView>();
        }

   

        private void Start() {
            _rangedWeaponsParent = Util.FindChild(gameObject, "RangedWeapons", true).transform;
             
            foreach(Transform t in _rangedWeaponsParent) {
                t.localScale = Vector3.zero;
            }

            _playerState = new PlayerState_Idle(this);
            _rangeWeapon = new RangedWeapon_Pistol(_rangedWeaponsParent, this);

            _data = Managers.Data;

            //test
            _droplable.Set("ObtainableRifle");
            _droplable.Set("ObtainablePistol");
            _droplable.Set("ObtainableShotgun");
        }

        private void Update() {
            moveX = Input.GetAxisRaw("Horizontal");
            moveZ = Input.GetAxisRaw("Vertical");

            OnPickupUpdate();
            _rangeWeapon.OnUpdate();
            _playerState.OnUpdate(this);
        }

        public void SetRelic(IRelic relic) {
            _haveRelic[(int)relic.RelicType] = true;
            _rangeWeapon.OnUpdateRelic(this);
            _status.SetResurrectionTime(_data.BonusResurrectionTime);
        }

        public void DeleteRelic(IRelic relic) {
            _haveRelic[(int)relic.RelicType] = false;
            _rangeWeapon.OnUpdateRelic(this);
            _status.SetResurrectionTime(_data.DefaultResurrectionTime);
        }

        public bool[] IsRelic() {
            return _haveRelic;
        }

        private void OnPickupUpdate() {
            if (_collideItem == null)
                return;

            if (Input.GetKeyDown(KeyCode.G)) {
                ChangeState(new PlayerState_Pickup(this));
                _collideItem.Pickup(this);
            }
        }

        public bool isMoving() {
            if (moveX == 0 && moveZ == 0)
                return false;

            return true;
        }
        
        public void OnPickupEvent() => ChangeState(new PlayerState_Idle(this));
        public void ChangeState(IPlayerState playerState) =>  _playerState = playerState;
        public void ChangeFadeAnimation(string animation) => _animator.CrossFade(animation, _animationFadeTime);
        public void ChangeIntigerAnimation(Define.PlayerState state) => _animator.SetInteger("State", (int)state);
        public void ChangeTriggerAnimation(Define.PlayerState state) => _animator.SetTrigger(state.ToString());

        public void ChangeRangedWeapon(IRangedWeapon weapon) {
            foreach(Transform t in _rangedWeaponsParent) {
                if (t.name == weapon.WeaponType.ToString())
                    t.localScale = weapon.DefaultScale;
                else
                t.localScale = Vector3.zero;
            }
            _rangeWeapon = weapon;
        }


        /// <summary>
        /// 플레이어 총 발사 로직
        /// </summary>
        public void OnShotUpdate() => _rangeWeapon.Shot(_mousePos, this);

        public void OnReloadUpdate() => _rangeWeapon.Reload(this);
        

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

        public void OnDieEvent() {
            Managers.Resources.Destroy(gameObject);
        }
        public void TakeDamage(int amout, GameObject attacker) {
            if (amout <= 0)
                return;

            int hp =_status.SetHp(-amout);

            if(hp <= 0) {
                _droplable.Drop(transform.position);
                ChangeState(new PlayerState_Die(this, attacker));
            }
        }

        private void OnTriggerEnter(Collider c) {
            if (c.CompareTag("ObtainableObject")) {
                _collideItem = c.GetComponent<IObtainableObject>();
                return;
                
            }
        }

        private void OnTriggerExit(Collider c) {
            if(c.CompareTag("ObtainableObject")) {
                if (_collideItem != null)
                    _collideItem = null;
            }
        }
    }
}

