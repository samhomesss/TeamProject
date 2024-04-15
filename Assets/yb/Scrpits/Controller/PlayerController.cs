using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor;
using Photon.Pun;
using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine.Events;
using Photon.Realtime;
using System.Reflection;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEditor.Progress;
using UnityEngine.UIElements;

namespace yb
{


    public class PlayerController : MonoBehaviour, ITakeDamage, ITakeDamagePhoton
    {
        private readonly float _animationFadeTime = .3f;  //�ִϸ��̼� ���̵� �ð�
        public const int MaxRelicNumber = 2;
        private int _playerHandle;  //�÷��̾� ���� ��ȣ
        private Vector3 _playerMoveVelocity;
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
        private GameObject _attacker;
        private Texture2D _texture; //0415 12:04 ����� �߰�
        public int HaveItemNumber { get; set; }
        public int HaveRelicNumber { get; set; }

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
        public Action<string> WeaponEvent;

        /// <summary>
        /// ���� ����� ȣ��
        /// <define.relicType>
        /// </summary>
        public Action<string, UnityAction, UnityAction> SetRelicEvent;

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
        /// ���� ���Ž� ȣ��
        /// </summary>
        public Action<string, UnityAction, UnityAction> DestroyRelicEvent;

        /// <summary>
        /// ���� �ʿ� ������ �󸶳� ����� �ִ��� �Ǵ�
        /// �� �������� �ƴ� �������µ� 2�ʰ� ���� �ִٸ����� ����
        /// </summary>
        public Action ColorPercentEvent;// ���� ��� ����

        /// <summary>
        /// �������� ��ó�� ������ ����
        /// </summary>
        public Action ClosedItemEvent;

        float resetTimer = 0; // �̺�Ʈ�� ȣ���� ģ���� �ʱ�ȭ ��Ű�� ��
        #endregion

        public PlayerStatus Status => _status;
        public PhotonView PhotonView => _photonview;

        public Vector3 PlayerMoveVelocity => _playerMoveVelocity;

        public Animator Animator => _animator;
        public PlayerWeaponController WeaponController => _weaponController;
        public PlayerPickupController PickupController => _pickupController;
        public PlayerStateController StateController => _stateController;
        public Camera MyCamera => _myCamera;
        public RotateToMouseScript RotateToMouseScript => _rotateToMouseScript;  //�÷��̾� ȸ���� ����

        public PhotonView IphotonView { get => _photonview; }//0410 18:42 ����� ����� �������̽� �߰�

        

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
            //_texture = Map.MapObject.GetComponent<Texture2D>();

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
        public void SetGuard(bool trigger)
        {
            _guardController.gameObject.SetActive(trigger);
        }

        /// <summary>
        /// �ǵ� ���� ����or ���Ž� Ȱ��ȭ
        /// </summary>
        /// <param name="trigger"></param>
        public void SetShield(bool trigger)
        {
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

            //if (resetTimer >= 1) 
            //{
            //    ColorPercentEvent?.Invoke();
            //    resetTimer = 0;
            //}
            #endregion
            return true;
        }

        public void CallSetColorRPC(int xPos, int yPos, int xIndex, int yIndex)
        {
            _photonview.RPC("SetColor", RpcTarget.Others, xPos, yPos, xIndex, yIndex);
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
            if (_photonview.IsMine)
                _animator.SetInteger("State", (int)state);
        }

        /// <summary>
        /// trigger�Ķ���ͷ� �ִϸ��̼� ü����
        /// </summary>
        /// <param name="state"></param>
        public void ChangeTriggerAnimation(Define.PlayerState state)//0408 16:38�� ����� ������Ʈ �߰�
        {
            if(IsTestMode.Instance.CurrentUser == Define.User.Hw) {
                if (_photonview.IsMine)
                    _animator.SetTrigger(state.ToString());
            }
            else
                _animator.SetTrigger(state.ToString());

        }

        /// <summary>
        /// �÷��̾� �̵� ����
        /// </summary>
        public void OnMoveUpdate()
        {
            if (IsTestMode.Instance.CurrentUser == Define.User.Hw)
            {
                if (_photonview.IsMine)//0405 09:41�� ĳ���Ͱ��� ����ȭ�� ���� ���� �̵� �и� ���� �߰�
                {
                    Vector3 dir = new Vector3(moveX, 0f, moveZ);
                    //_rigid.MovePosition(_rigid.position + dir * (_status.MoveSpeed * _status.MoveSpeedDecrease) * Time.deltaTime);
                    //transform.parent.Translate(dir * (_status.MoveSpeed * _status.MoveSpeedDecrease) * Time.deltaTime); //0410 23:44 ����� ���� ����ȭ ������ ���� �ش��� �ּ�ó�� 

                    _rigid.MovePosition(_rigid.position + dir * (_status.MoveSpeed * _status.MoveSpeedDecrease) * Time.deltaTime);
                    _playerMoveVelocity = dir * (_status.MoveSpeed * _status.MoveSpeedDecrease) * Time.deltaTime;
                }
            }
            else {
                Vector3 dir = new Vector3(moveX, 0f, moveZ);
                _rigid.MovePosition(_rigid.position + dir * (_status.MoveSpeed * _status.MoveSpeedDecrease) * Time.deltaTime);
                _playerMoveVelocity = dir * (_status.MoveSpeed * _status.MoveSpeedDecrease) * Time.deltaTime;

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
        public void OnDieEvent() {
            RespawnManager.Instance.Respawn(_playerHandle, _status.ResurrectionTime);
            Managers.Resources.Destroy(transform.root.gameObject, _status.ResurrectionTime);
            GameObject go = MyCamera.gameObject;
            go.transform.parent = null;
            Managers.Resources.Destroy(go, _status.ResurrectionTime);
            _rotateToMouseScript.PlayerDead();
        }

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


        [PunRPC]
        public void SetDropItemName(int dropObjectViewId)
        {
            PhotonView _photonView = PhotonNetwork.GetPhotonView(dropObjectViewId);

            int index = _photonView.transform.gameObject.name.IndexOf("(Clone)");
            if (index > 0)
                _photonView.transform.gameObject.name = _photonView.transform.gameObject.name.Substring(0, index);
        }

        [PunRPC]//0413 03:46 ����� ����� �޼��� �߰� �÷��̾� �̸�����
        public void RenamePlayer(int PlayerViewId)
        {
            Debug.Log($"{PhotonNetwork.GetPhotonView(PlayerViewId)}�÷��̾ ���Խ��ϴ�{PhotonNetwork.LocalPlayer.ActorNumber}");
            PhotonNetwork.GetPhotonView(PlayerViewId).transform.parent.name = $"Player{PhotonNetwork.GetPhotonView(PlayerViewId).Owner.ActorNumber}";
        }
        [PunRPC]
        public void TakeDamagePhoton(int amout, int attackerViewNum)//0410 19:24 ����� ����� �޼��� �߰�  
        {
            if (amout <= 0)
                return;

            int hp = _status.SetHp(-amout);
            HpEvent?.Invoke(_status.CurrentHp, _status.MaxHp);
            if (hp <= 0)
            {
                _droplable.Drop(transform.position);
                _attacker = PhotonNetwork.GetPhotonView(attackerViewNum).gameObject; //PunRpc���� GameObject�� ����ȭ �ؼ� ���� �� ���⿡ ����ȭ �ؼ� ���� �� �ִ� attackerViewNum�� ������ �ش� attackerViewNum�� �����Ʈ��ũ���� ã�Ƽ� �־��ش�.
                _stateController.ChangeState(new PlayerState_Die(this, _attacker));
            }
        }
    }
}

