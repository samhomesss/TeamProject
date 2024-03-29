using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace yb {
    public class PlayerController : MonoBehaviour {
        private Rigidbody _rigid;
        private Collider _collider;
        private Animator _animator;
        [SerializeField] float _moveSpeed;

        private void Awake() {
            _rigid = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();
            _animator = GetComponent<Animator>();
        }

        private void Start() {
            Managers.Input.OnKeyboardEvent += OnMoveUpdate;
        }

        private void Update() {

            //todo
            //현재 회전량 0일시 리턴

            //OnRotateUpdate();
        }
        /// <summary>
        /// 플레이어 이동 로직
        /// </summary>
        private void OnMoveUpdate() {
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveZ = Input.GetAxisRaw("Vertical");

            if (moveX < 1 && moveZ < 1)
                return;

            Vector3 dir = new Vector3(moveX, 0f, moveZ);

            _rigid.MovePosition(_rigid.position + dir * _moveSpeed * Time.deltaTime);
        }

        /// <summary>
        /// 플레이어 회전 로직
        /// </summary>
        private void OnRotateUpdate(float angle) {

        }
    }
}

