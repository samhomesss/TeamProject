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
        private PhotonView _photonview; //0405 09:41�� ����� ĳ���Ͱ��� ����ȭ�� ���� ���� �� �߰�

        //item1. ü��
        //item2. �Ѿ�
        //item3. ����
        //item4. ����
        //item5. ������
        //item6. �̴ϸ�
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
        /// ����,�ɷ�ġ Ŭ����
        /// Ÿ�� ���� private�� �÷��̾��� �������� ������ �����Ǿ�����.
        /// �ʿ�� get ������Ƽ ���� �� ���
        /// </summary>
        public PlayerStatus Status => _status;

        /// <summary>
        /// �÷��̾� ���� Ŭ����
        /// ������ IRangedWeapon ������ ������ ������ ���ϰ�����
        /// Ÿ�� ���� private�� ������ �������� ������ �����Ǿ�����.
        /// �ʿ�� get ������Ƽ ���� �� ���
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
            //����� set�ص� ������ ���
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
        /// �÷��̾� �̵� ����
        /// </summary>
        public void OnMoveUpdate() {
            if (_photonview.IsMine)//0405 09:41�� ĳ���Ͱ��� ����ȭ�� ���� ���� �̵� �и� ���� �߰�
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

