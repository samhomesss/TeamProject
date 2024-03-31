using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using DG.Tweening;
using UnityEditor;
using Unity.VisualScripting;

namespace yb {
    public class PlayerController : MonoBehaviour, ITakeDamage {
        private readonly float _animationFadeTime = .3f;
        private Rigidbody _rigid;
        private float moveX;
        private float moveZ;
        private Collider _collider;
        private Animator _animator;
        private Vector3 _mousePos;
        private Transform _rangedWeaponsParent;
        private Transform _meleeWeaponsParent;
        private IPlayerState _playerState;
        private IMeleeWeapon _meleeWeapon;
        private IRangedWeapon _rangeWeapon;
        private IItemDroplable _droplable = new ItemDroplable();
        private PlayerStatus _status;

        public Transform RangedWeaponsParent => _rangedWeaponsParent;
        public Transform MeleeWeaponsParent => _meleeWeaponsParent;
        private void Awake() {
            _rigid = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();
            _animator = GetComponent<Animator>();
            _status = GetComponent<PlayerStatus>();
        }

        private void Start() {
            _rangedWeaponsParent = Util.FindChild(gameObject, "RangedWeapons", false).transform;
            _meleeWeaponsParent = Util.FindChild(gameObject, "MeleeWeapons", false).transform;

            _playerState = new PlayerState_Idle(this);
            _meleeWeapon = new MeleeWeapon_Club(_meleeWeaponsParent, this);
            _rangeWeapon = new RangedWeapon_Pistol(_rangedWeaponsParent);

            //test
            _droplable.Set("Club");
            _droplable.Set("Pistol");
        }

        private void Update() {
            moveX = Input.GetAxisRaw("Horizontal");
            moveZ = Input.GetAxisRaw("Vertical");

            OnRotateUpdate();

            _playerState.OnUpdate(this);
            _rangeWeapon.OnUpdate();
            _meleeWeapon.OnUpdate();
        }

        public bool isMoving() {
            if (moveX == 0 && moveZ == 0)
                return false;

            return true;
        }

        public void ChangeState(IPlayerState playerState) =>  _playerState = playerState;

        public void ChangeAnimation(string animation) => _animator.CrossFade(animation, _animationFadeTime);

        public void ChangeRangedWeapon(IRangedWeapon weapon) => _rangeWeapon = weapon;

        public void ChangeMeleeWeapon(IMeleeWeapon weapon) => _meleeWeapon = weapon;

        /// <summary>
        /// 플레이어 총 발사 로직
        /// </summary>
        public void OnShotUpdate() => _rangeWeapon.Shot(_mousePos, this);

        /// <summary>
        /// 플레이어 근접공격 로직
        /// </summary>
        public void OnAttackUpdate() => _meleeWeapon.MeleeAttack();

        /// <summary>
        /// 플레이어 이동 로직
        /// </summary>
        public void OnMoveUpdate() {
            Vector3 dir = new Vector3(moveX, 0f, moveZ);
            _rigid.MovePosition(_rigid.position + dir * _status._moveSpeed * Time.deltaTime);
        }

        /// <summary>
        /// 플레이어 회전 로직
        /// </summary>
        private void OnRotateUpdate() {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool hit = Physics.Raycast(ray, out var target, float.MaxValue);

            if (!hit)
                return;

            _mousePos = target.point;
            _mousePos.y = 1f;

            transform.LookAt(_mousePos);
        }

        public void OnDieUpdate() {
            _collider.enabled = false;
            _rigid.isKinematic = true;
            ChangeAnimation("Die");
        }

        public void TakeDamage(int amout) {
            if (amout <= 0)
                return;

            _status._currentHp -= amout;

            if(_status._currentHp <= 0) {
                _droplable.Drop(transform.position);
                ChangeState(new PlayerState_Die(this));
            }
        }

        private void OnTriggerStay(Collider c) {
            if (c.CompareTag("ObtainableObject")) {
                //todo
                //여기에 아이템 ui표기 필요

                if (Input.GetKeyDown(KeyCode.G)) {
                    ChangeState(new PlayerState_Pickup(this));
                    c.GetComponent<IObtainableObject>().Pickup(this);
                }
            }
        }
    }
}

