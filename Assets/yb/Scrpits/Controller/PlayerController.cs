using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using DG.Tweening;
using UnityEditor;
using Unity.VisualScripting;
using UnityEditor.Animations;

namespace yb {
    public class PlayerController : MonoBehaviour, ITakeDamage {
        private readonly float _animationFadeTime = .3f;
        private Rigidbody _rigid;
        private float moveX;
        private float moveZ;
        private Collider _collider;
        private Animator _animator;
        private Vector3 _mousePos;
        [SerializeField]private Transform _rangedWeaponsParent;
        private IPlayerState _playerState;
        private IRangedWeapon _rangeWeapon;
        private IItemDroplable _droplable = new ItemDroplable();
        private PlayerStatus _status;
        private RotateToMouseScript _rotateToMouseScript;
        private IObtainableObject _collideItem;
        public RotateToMouseScript RotateToMouseScript => _rotateToMouseScript;
        public IRangedWeapon RangedWeapon => _rangeWeapon;
        public Transform RangedWeaponsParent => _rangedWeaponsParent;

        public enum State { 
            Shot,
            Reload,
            Pickup,
            Die,
            Idle
        }
        private void Awake() {
            _rigid = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();
            _animator = GetComponent<Animator>();
            _status = GetComponent<PlayerStatus>();
            _rotateToMouseScript = GetComponent<RotateToMouseScript>();
        }

        private void Start() {
            foreach(Transform t in _rangedWeaponsParent) {
                t.localScale = Vector3.zero;
            }

            _playerState = new PlayerState_Idle(this);
            _rangeWeapon = new RangedWeapon_Pistol(_rangedWeaponsParent);

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
        public void ChangeIntigerAnimation(State state) => _animator.SetInteger("State", (int)state);
        public void ChangeTriggerAnimation(State state) => _animator.SetTrigger(state.ToString());

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
            Vector3 dir = new Vector3(moveX, 0f, moveZ);
            _rigid.MovePosition(_rigid.position + dir * _status._moveSpeed * Time.deltaTime);
        }

        public void OnDieUpdate(GameObject attacker) {
            transform.LookAt(attacker.transform.position);
            _collider.enabled = false;
            _rigid.isKinematic = true;
            ChangeTriggerAnimation(State.Die);
        }

        public void OnDieEvent() {
            Managers.Resources.Destroy(gameObject);
        }
        public void TakeDamage(int amout, GameObject attacker) {
            if (amout <= 0)
                return;

            _status._currentHp -= amout;

            if(_status._currentHp <= 0) {
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

