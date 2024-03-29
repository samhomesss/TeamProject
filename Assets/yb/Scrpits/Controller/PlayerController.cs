using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using DG.Tweening;
using UnityEditor;
using Unity.VisualScripting;

namespace yb {
    public class PlayerController : MonoBehaviour {
        private readonly float _animationFadeTime = .3f;
        private Rigidbody _rigid;
        private float moveX;
        private float moveZ;
        private Collider _collider;
        private Animator _animator;
        private Vector3 _mousePos;
        private Transform _firePos;
        private Transform _swordMainPos;
        private IPlayerState _playerState;
        [SerializeField] float _moveSpeed;

        private void Awake() {
            _rigid = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();
            _animator = GetComponent<Animator>();
        }

        private void Start() {
            _playerState = new PlayerIdleState(this);
            _firePos = Util.FindChild(gameObject, "FirePos", true).transform;
            _swordMainPos = Util.FindChild(gameObject, "SwordMainPos", true).transform;
            _swordMainPos.gameObject.SetActive(false);
        }

        private void Update() {
            moveX = Input.GetAxisRaw("Horizontal");
            moveZ = Input.GetAxisRaw("Vertical");

            OnRotateUpdate();
            _playerState.OnUpdate(this);

            _currentShotDelay += Time.deltaTime;
            _currentAttackDelay += Time.deltaTime;
        }

        public bool isMoving() {
            if (moveX == 0 && moveZ == 0)
                return false;

            return true;
        }

        public void ChangeState(IPlayerState playerState) {
            _playerState = playerState;
        }

        public void ChangeAnimation(string animation) {
            _animator.CrossFade(animation, _animationFadeTime);
        }

        /// <summary>
        /// 플레이어 이동 로직
        /// </summary>
        public void OnMoveUpdate() {
            Vector3 dir = new Vector3(moveX, 0f, moveZ);
            _rigid.MovePosition(_rigid.position + dir * _moveSpeed * Time.deltaTime);
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

        [SerializeField] private float _shotDelay;
        private float _currentShotDelay;
        /// <summary>
        /// 플레이어 총 발사 로직
        /// </summary>
        public void OnShotUpdate() {
            if (_currentShotDelay < _shotDelay)
                return;

            _currentShotDelay = 0f;
            BulletController bullet = Managers.Resources.Instantiate("yb/Bullet/Bullet", null).GetComponent<BulletController>();
            bullet.Init(5f, 10f, _mousePos, _firePos.position);
        }

        [SerializeField] private float _attackDelay;
        private float _currentAttackDelay;
        /// <summary>
        /// 플레이어 공격(아마 근접) 로직
        /// </summary>
        public void OnAttackUpdate() {
            if (_currentAttackDelay < _attackDelay)
                return;

            _currentAttackDelay = 0f;
            _swordMainPos.gameObject.SetActive(true);
            Vector3 currentRotation = transform.eulerAngles;
            Vector3 newRotation = new Vector3(currentRotation.x, currentRotation.y + 45, currentRotation.z);
            _swordMainPos.DORotate(newRotation, .3f).SetEase(Ease.Linear).OnComplete(ExitSwordAttack);
        }

        /// <summary>
        /// 플레이어 공격 종료 함수
        /// 콜백으로 사용
        /// </summary>
        private void ExitSwordAttack() {
            Vector3 currentRotation = transform.eulerAngles;
            Vector3 newRotation = new Vector3(currentRotation.x, currentRotation.y - 45, currentRotation.z);
            _swordMainPos.rotation = Quaternion.Euler(newRotation);
            _swordMainPos.gameObject.SetActive(false);
        }
    }
}

