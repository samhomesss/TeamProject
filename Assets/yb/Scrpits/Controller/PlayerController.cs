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
using System.Runtime.InteropServices;

namespace yb
{


    public class PlayerController : MonoBehaviour, ITakeDamage, ITakeDamagePhoton
    {
        private readonly float _animationFadeTime = .3f;  //�ִϸ��̼� ���̵� �ð�
        private const float LimitLeftPosX = 1f;
        private const float LimitRightPosX = 62f;
        private const float LimitUpPosZ = 62f;
        private const float LimitDownPosZ = 1f;
        public const int MaxRelicNumber = 2;
        private const float PALYER_MAX_HP = 30;
        private int _playerHandle;  //�÷��̾� ���� ��ȣ
        public const int MaxItemNumber = 9;
        public const int MaxItemSlot = 4;
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
        private Map _map; //0416 21:51 ����� �߰�
        public int HaveItemNumber { get; set; }

        private Dictionary<int, Item> _itemList = new Dictionary<int, Item>();

        public Dictionary<int, Item> ItemList => _itemList;
        public int HaveRelicNumber { get; set; }

        public class Item {
            public Item(Define.ItemType type, int number) {
                ItemType = type;
                ItemNumber = number;
            }
            public Define.ItemType ItemType;
            public int ItemNumber;
        }

        /// <summary>
        /// �Һ� ������ ����� ȣ��
        /// int = ���� ��ȣ(0,1,2,3)
        /// Item = ������ ����(������ Ÿ��, ������ ����)
        /// </summary>
        public Action<int, Item> SetItemEvent;


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
        /// ���� ����� �̹��� ����
        /// </summary>
        public Action<string, UnityAction, UnityAction> ChangeRelicIMGEvent;

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

        public int PlayerHandle { get; set; }
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
            PlayerHandle = PhotonNetwork.LocalPlayer.ActorNumber;
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
            
            //MapEvent?.Invoke();
            //ClosedItemEvent?.Invoke();            

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
        float _time;
        /// <summary>
        /// �÷��̾� �̵� ����
        /// </summary>
        public void OnMoveUpdate()
        {
            _time += Time.deltaTime;
            if (IsTestMode.Instance.CurrentUser == Define.User.Hw)
            {
                if (_photonview.IsMine)//0405 09:41�� ĳ���Ͱ��� ����ȭ�� ���� ���� �̵� �и� ���� �߰�
                {
                    Vector3 dir = new Vector3(moveX, 0f, moveZ);
                    //_rigid.MovePosition(_rigid.position + dir * (_status.MoveSpeed * _status.MoveSpeedDecrease) * Time.deltaTime);
                    //transform.parent.Translate(dir * (_status.MoveSpeed * _status.MoveSpeedDecrease) * Time.deltaTime); //0410 23:44 ����� ���� ����ȭ ������ ���� �ش��� �ּ�ó�� 
                    _rigid.MovePosition(_rigid.position + dir * (_status.MoveSpeed * _status.MoveSpeedDecrease) * Time.deltaTime);
                    _playerMoveVelocity = dir * (_status.MoveSpeed * _status.MoveSpeedDecrease) * Time.deltaTime;
                    if (_rigid.position.x <= LimitLeftPosX)
                        _rigid.position = new Vector3(LimitLeftPosX, _rigid.position.y, _rigid.position.z);
                    if (_rigid.position.x >= LimitRightPosX)
                        _rigid.position = new Vector3(LimitRightPosX, _rigid.position.y, _rigid.position.z);
                    if (_rigid.position.z <= LimitDownPosZ)
                        _rigid.position = new Vector3(_rigid.position.x, _rigid.position.y, LimitDownPosZ);
                    if (_rigid.position.z >= LimitUpPosZ)
                        _rigid.position = new Vector3(_rigid.position.x, _rigid.position.y, LimitUpPosZ);

                }
            }
            else {
                Vector3 dir = new Vector3(moveX, 0f, moveZ);
                _rigid.MovePosition(_rigid.position + dir * (_status.MoveSpeed * _status.MoveSpeedDecrease) * Time.deltaTime);
                _playerMoveVelocity = dir * (_status.MoveSpeed * _status.MoveSpeedDecrease) * Time.deltaTime;

                if (_rigid.position.x <= LimitLeftPosX)
                    _rigid.position = new Vector3(LimitLeftPosX, _rigid.position.y, _rigid.position.z);
                if (_rigid.position.x >= LimitRightPosX)
                    _rigid.position = new Vector3(LimitRightPosX, _rigid.position.y, _rigid.position.z);
                if (_rigid.position.z <= LimitDownPosZ)
                    _rigid.position = new Vector3(_rigid.position.x, _rigid.position.y, LimitDownPosZ);
                if (_rigid.position.z >= LimitUpPosZ)
                    _rigid.position = new Vector3(_rigid.position.x, _rigid.position.y, LimitUpPosZ);
            }
            if (_time >= 0.1f)
            {
                MapEvent?.Invoke();
                _time = 0;
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
            _rotateToMouseScript.PlayerDead();
            ChangeTriggerAnimation(Define.PlayerState.Die);
            Invoke("PlayerRespawn", _status.ResurrectionTime);
        }

        /// <summary>
        /// �÷��̾� ��Ȱ ���ġ ����
        /// </summary>
        private void PlayerRespawn() {
            _stateController.ChangeState(new PlayerState_Idle(this));
            Status.SetHp(Status.MaxHp - Status.CurrentHp);
            ChangeTriggerAnimation(Define.PlayerState.Respawn);
            _collider.enabled = true;
            _rigid.isKinematic = false;
            _rotateToMouseScript.PlayerRespawn();
            Transform tr = RespawnManager.Instance.RespawnPoints;
            transform.position = tr.GetChild(UnityEngine.Random.Range(0, tr.childCount - 1)).position;
        }

        /// <summary>
        /// �÷��̾� ��� �̺�Ʈ(�ִϸ��̼ǿ��� �̺�Ʈ�� ȣ��)
        /// </summary>
        public void OnDieEvent() {

            //if(IsTestMode.Instance.CurrentUser == Define.User.Hw)
            //{
            //    if (_photonview.IsMine)
            //    {
            //        GameObject go = MyCamera.gameObject;
            //        go.transform.parent = null;
            //        StartCoroutine(CoroutineHelper.Instance.CoDelayPhotonObjectDelete(go, _status.ResurrectionTime));
            //        StartCoroutine(CoroutineHelper.Instance.CoDelayPhotonObjectSpawn(_status.ResurrectionTime, SetUI));
            //        StartCoroutine(CoroutineHelper.Instance.CoDelayPhotonObjectDelete(transform.root.gameObject, _status.ResurrectionTime));
                   

            //       _rotateToMouseScript.PlayerDead();
            //    }
            //}
            //else
            //{
            //    RespawnManager.Instance.Respawn(PlayerHandle, _status.ResurrectionTime);
            //    Managers.Resources.Destroy(transform.root.gameObject, _status.ResurrectionTime);
            //    GameObject go = MyCamera.gameObject;
            //    go.transform.parent = null;
            //    Managers.Resources.Destroy(go, _status.ResurrectionTime);
            //    _rotateToMouseScript.PlayerDead();
            //}
        }
        public void SetUI()// 0416 ����� �÷��̾� ������ �ɶ� UI �ʱ�ȭ
        {
            GameObject.Find("@UI_Root").GetComponentInChildren<UI_Hp>().HpSlider = PALYER_MAX_HP;
            _map = Map.MapObject.GetComponent<Map>();
            _map.SetPlayer(GameObject.Find($"Player{PhotonNetwork.LocalPlayer.ActorNumber}").GetComponent<PlayerController>());
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




        Color GetColorFromNodeColor(NodeColor nodeColor)
        {
            switch (nodeColor)
            {
                case NodeColor.Red: return Color.red;
                case NodeColor.Yellow: return Color.yellow;
                case NodeColor.Blue: return Color.blue;
                case NodeColor.Magenta: return Color.magenta;
                case NodeColor.Cyan: return Color.cyan;
                case NodeColor.Gray: return Color.grey;
                case NodeColor.Green: return Color.green;
                case NodeColor.Black: return Color.black;
                default: return Color.white;
            }
        }
        [PunRPC]
        public void SetColorRPC(int xPos, int yPos, int nodeXIndex, int nodeYIndex, int colorEnumIndex)
        {
            //Color PlayerColor = Map.PlayerColor(gameObject.transform.parent.gameObject);
            Map.MapObject.GetComponent<Map>().MapSetColor(xPos, yPos, nodeXIndex, nodeYIndex, GetColorFromNodeColor((NodeColor)colorEnumIndex));
            //Debug.Log("<color=red>SetColorRPC</color>");
        }

        [PunRPC]
        public void SetDropItemName(int dropObjectViewId)//0414 ����� ���� ��������� �̸�������
        {
            PhotonView _photonView = PhotonNetwork.GetPhotonView(dropObjectViewId);

            int index = _photonView.transform.gameObject.name.IndexOf("(Clone)");
            if (index > 0)
                _photonView.transform.gameObject.name = _photonView.transform.gameObject.name.Substring(0, index);
        }

        [PunRPC]//0413 03:46 ����� ����� �޼��� �߰� �÷��̾� �̸�����
        public void RenamePlayer(int PlayerViewId)
        {
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

