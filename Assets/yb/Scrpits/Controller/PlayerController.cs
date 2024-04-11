using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor;
using Photon.Pun;
using System;

namespace yb
{
    public class PlayerController : MonoBehaviour, ITakeDamage
    {
        
        private readonly float _animationFadeTime = .3f;  //�ִϸ��̼� ���̵� �ð�
        private Rigidbody _rigid;
        private Data _data;  //�⺻ ������
        private float moveX;  //�̵���x
        private float moveZ;  //�̵���z
        private Camera _myCamera;  //�� ī�޶�(ī�޶� �ʿ��� �Ҵ���)
        private Collider _collider;
        private Animator _animator;
        private PlayerPickupController _pickupController;
        private PlayerStateController _stateController;
        private PlayerWeaponController _weaponController;
        private RotateToMouseScript _rotateToMouseScript;
        private IItemDroplable _droplable = new ItemDroplable();  //������ ��ӿ� ����. set�Լ��� ����� ������ ����. drop�Լ��� ������ ���
        private PlayerStatus _status;  //�÷��̾� �ɷ�ġ
        private PhotonView _photonview; //0405 09:41�� ����� ĳ���Ͱ��� ����ȭ�� ���� ���� �� �߰�
        private PlayerGuardController _guardController;
        private PlayerShieldController _shieldController;
        /// <summary>
        /// �÷��̾� hp����� ȣ��
        /// <���� hp, �ִ� hp>
        /// </summary>
        public Action<int, int> HpEvent;  

        /// <summary>
        /// ���� �Ѿ� ����� ȣ��
        /// <���� �Ѿ�, �ִ� �Ѿ�>
        /// </summary>
        public Action<int, int> BulletEvent;

        /// <summary>
        /// �÷��̾� ���� ����� ȣ��
        /// <define.weaponType>
        /// </summary>
        public Action<int> WeaponEvent;

        /// <summary>
        /// ���� ���� �� ���Ž� ȣ��
        /// <define.relicType>
        /// </summary>
        public Action<int> RelicEvent;

        /// <summary>
        /// ������ ���� �� ȣ��
        /// <�������� �̸��� ���ڿ��� ����>
        /// </summary>
        public Action<string> ItemEvent;

        /// <summary>
        /// �÷��̾ �����϶� 
        /// Map�� ��� ���� ĥ���ִ°� 
        /// </summary>
        public Action MapEvent;

        #region ���� �߰� 04.11
        /// <summary>
        /// ���� �ʿ� ������ �󸶳� ����� �ִ��� �Ǵ�
        /// �� �������� �ƴ� �������µ� 2�ʰ� ���� �ִٸ����� ����
        /// </summary>
        public Action ColorPercentEvent;

        /// <summary>
        /// �������� ��ó�� ������ ����
        /// </summary>
        public Action ClosedItemEvent;

        float resetTimer = 0; // �̺�Ʈ�� ȣ���� ģ���� �ʱ�ȭ ��Ű�� ��
        #endregion

        public PlayerStatus Status => _status;
        public PhotonView PhotonView => _photonview;
        
        public PlayerWeaponController WeaponController => _weaponController;
        public PlayerPickupController PickupController => _pickupController;
        public PlayerStateController StateController => _stateController;
        public Camera MyCamera => _myCamera;
        public RotateToMouseScript RotateToMouseScript => _rotateToMouseScript;  //�÷��̾� ȸ���� ����


        private void Awake()
        {
            _rigid = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();
            _animator = GetComponent<Animator>();
            _status = GetComponent<PlayerStatus>();
            _rotateToMouseScript = GetComponent<RotateToMouseScript>();
            _photonview = GetComponent<PhotonView>();
            _weaponController = GetComponent<PlayerWeaponController>();
            _stateController = GetComponent<PlayerStateController>();
            _pickupController = GetComponent<PlayerPickupController>();
            _guardController = transform.parent.GetComponentInChildren<PlayerGuardController>();
            _shieldController = transform.parent.GetComponentInChildren<PlayerShieldController>();
            _guardController.gameObject.SetActive(false);
            _shieldController.gameObject.SetActive(false);
        }


        private void Start()
        {
            _data = Managers.Data;

            //����� set�ص� ������ ���
            _droplable.Set("ObtainableRifle");
            _droplable.Set("ObtainablePistol");
            _droplable.Set("ObtainableShotgun");

        }

        private void Update()
        {
            moveX = Input.GetAxisRaw("Horizontal");
            moveZ = Input.GetAxisRaw("Vertical");

            #region 04.11 ���� �߰� 
            resetTimer += Time.deltaTime; // �� �������� �ƴ� �ʸ��� ���� ��ų ����
            #endregion  
        }

        public void SetCamera(Camera camera) => _myCamera = camera;

        /// <summary>
        /// ���� ���� ����or ���Ž� Ȱ��ȭ
        /// </summary>
        /// <param name="trigger"></param>
        public void SetGuard(bool trigger) {
            _guardController.gameObject.SetActive(trigger);
        }

        /// <summary>
        /// �ǵ� ���� ����or ���Ž� Ȱ��ȭ
        /// </summary>
        /// <param name="trigger"></param>
        public void SetShield(bool trigger) {
            _shieldController.gameObject.SetActive(trigger);
        }

        /// <summary>
        /// �÷��̾� �̵� ��Ȳ üũ
        /// </summary>
        /// <returns></returns>
        public bool isMoving()
        {
            if (moveX == 0 && moveZ == 0)
                return false;
            #region ���� �߰� 04.11
            MapEvent?.Invoke();
            ClosedItemEvent?.Invoke();
            if (resetTimer >= 1) // �ż��� ȣ�� �� ģ���� �ƴϱ⿡ 
            {
                ColorPercentEvent?.Invoke();
            }
            #endregion
            return true;
        }

        /// <summary>
        /// Bool�Ķ���ͷ� �ִϸ��̼� ü����
        /// </summary>
        /// <param name="animation"></param>
        public void ChangeBoolAnimation(string animation) //0408 ����� ������ �ִϸ��̼� �߰�
        {
            if (isMoving())
            {
                _animator.SetBool("Move", true);
                _animator.SetBool("Idle", false);
            }
            else
            {
                _animator.SetBool("Move", false);
                _animator.SetBool("Idle", true);
            }
        }

        /// <summary>
        /// int�Ķ���ͷ� �ִϸ��̼� ü����
        /// </summary>
        /// <param name="state"></param>
        public void ChangeIntigerAnimation(Define.PlayerState state)//0408 16:38�� ����� ������Ʈ �߰�
        {
            //if(state == Define.PlayerState.Shot)
                if(_photonview.IsMine)
                    _animator.SetInteger("State", (int)state);
        }

        /// <summary>
        /// trigger�Ķ���ͷ� �ִϸ��̼� ü����
        /// </summary>
        /// <param name="state"></param>
        public void ChangeTriggerAnimation(Define.PlayerState state)//0408 16:38�� ����� ������Ʈ �߰�
        {
            //if (state == Define.PlayerState.Shot)
                if (_photonview.IsMine)
                    _animator.SetTrigger(state.ToString());
        }

        /// <summary>
        /// �÷��̾� �̵� ����
        /// </summary>
        public void OnMoveUpdate()
        {
            if (_photonview.IsMine)//0405 09:41�� ĳ���Ͱ��� ����ȭ�� ���� ���� �̵� �и� ���� �߰�
            {
                Vector3 dir = new Vector3(moveX, 0f, moveZ);
                //_rigid.MovePosition(_rigid.position + dir * (_status.MoveSpeed * _status.MoveSpeedDecrease) * Time.deltaTime);
                transform.parent.Translate(dir * (_status.MoveSpeed * _status.MoveSpeedDecrease) * Time.deltaTime);
            }
        }

        /// <summary>
        /// �÷��̾� State������� ���� �� ��� �ִϸ��̼� ���
        /// </summary>
        /// <param name="attacker"></param>
        public void OnDieUpdate(GameObject attacker)
        {
            transform.LookAt(attacker.transform.position);
            _collider.enabled = false;
            _rigid.isKinematic = true;
            ChangeTriggerAnimation(Define.PlayerState.Die);
        }

        /// <summary>
        /// �÷��̾� ��� �̺�Ʈ(�ִϸ��̼ǿ��� �̺�Ʈ�� ȣ��)
        /// </summary>
        public void OnDieEvent() => Managers.Resources.Destroy(gameObject);

        /// <summary>
        /// �÷��̾� �ǰ� ����(�ǰ� ������, ������)
        /// </summary>
        /// <param name="amout"></param>
        /// <param name="attacker"></param>
        public void TakeDamage(int amout, GameObject attacker)
        {
            if (amout <= 0)
                return;

            int hp = _status.SetHp(-amout);
            Debug.Log($"�÷��̾ ��������{amout}��ŭ ����");
            HpEvent?.Invoke(_status.CurrentHp, _status.MaxHp);
            if (hp <= 0)
            {
                _droplable.Drop(transform.position);
                _stateController.ChangeState(new PlayerState_Die(this, attacker));
            }
        }
    }
}

