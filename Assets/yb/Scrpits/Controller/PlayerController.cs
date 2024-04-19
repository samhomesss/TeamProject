using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
using UnityEngine.Events;
using System.Collections;

namespace yb
{
    public class PlayerController : MonoBehaviour, ITakeDamage, ITakeDamagePhoton
    {
        public string PlayerNickName { get; set; }
        private readonly float _animationFadeTime = .3f;  //애니메이션 페이드 시간
        private const float LimitLeftPosX = 1f;
        private const float LimitRightPosX = 62f;
        private const float LimitUpPosZ = 62f;
        private const float LimitDownPosZ = 1f;
        public const int MaxRelicNumber = 2;
        private const float PALYER_MAX_HP = 30;
        private int _playerHandle;  //플레이어 고유 번호
        public const int MaxItemNumber = 9;
        public const int MaxItemSlot = 4;
        private Vector3 _playerMoveVelocity;
        private Rigidbody _rigid;
        private Data _data;  //기본 데이터
        private float moveX;  //이동량x
        private float moveZ;  //이동량z
        private Camera _myCamera;  //내 카메라(카메라 쪽에서 할당중)
        private Collider _collider;
        private Animator _animator;
        private PlayerPickupController _pickupController;
        private PlayerStateController _stateController;
        private PlayerWeaponController _weaponController;
        private RotateToMouseScript _rotateToMouseScript;
        private IItemDroplable _droplable = new ItemDroplable();  //아이템 드롭용 변수. set함수로 드롭할 아이템 저장. drop함수로 아이템 드롭
        private PlayerStatus _status;  //플레이어 능력치
        private PhotonView _photonview; //0405 09:41분 이희웅 캐릭터간에 동기화를 위한 포톤 뷰 추가
        private PlayerGuardController _guardController;
        private PlayerShieldController _shieldController;
        private GameObject _attacker;
        private Texture2D _texture; //0415 12:04 이희웅 추가
        private Map _map; //0416 21:51 이희웅 추가
        
        
        public int HaveItemNumber { get; set; }

        public int NodeCount { get; set; }

        private Dictionary<int, Item> _itemList = new Dictionary<int, Item>();

        public Dictionary<int, Item> ItemList => _itemList;
        public int HaveRelicNumber { get; set; }

        public class Item
        {
            public Item(Define.ItemType type, int number)
            {
                ItemType = type;
                ItemNumber = number;
            }
            public Define.ItemType ItemType;
            public int ItemNumber;
        }

        /// <summary>
        /// 소비 아이템 변경시 호출
        /// int = 슬롯 번호(0,1,2,3)
        /// Item = 아이템 정보(아이템 타입, 아이템 갯수)
        /// </summary>
        public Action<int, Item> SetItemEvent;


        /// <summary>
        /// 플레이어 hp변경시 호출
        /// <현재 hp, 최대 hp>
        /// </summary>
        public Action<int, int> HpEvent;

        /// <summary>
        /// 무기 총알 변경시 호출
        /// <현재 총알, 최대 총알>
        /// </summary>
        public Action<int, int> BulletEvent;

        /// <summary>
        /// 플레이어 무기 변경시 호출
        /// <define.weaponType>
        /// </summary>
        public Action<string> WeaponEvent;

        /// <summary>
        /// 렐릭 습득시 호출
        /// <define.relicType>
        /// </summary>
        public Action<string, UnityAction, UnityAction> SetRelicEvent;

        /// <summary>
        /// 렐릭 습득시 이미지 변경
        /// </summary>
        public Action<string, UnityAction, UnityAction> ChangeRelicIMGEvent;

        /// <summary>
        /// 아이템 습득 시 호출
        /// <아이템의 이름을 문자열로 저장>
        /// </summary>
        public Action<string> ItemEvent;

        /// <summary>
        /// 플레이어가 움직일때 
        /// Map에 노드 색상 칠해주는거 
        /// </summary>
        public Action MapEvent;

        #region 승현 추가 04.11
        /// <summary>
        /// 렐릭 제거시 호출
        /// </summary>
        public Action<string, UnityAction, UnityAction> DestroyRelicEvent;

        /// <summary>
        /// 현재 맵에 색상이 얼마나 띄워져 있는지 판단
        /// 매 프레임이 아닌 움직였는데 2초가 지나 있다면으로 변경
        /// </summary>
        public Action ColorPercentEvent;// 현재 사용 안함

        /// <summary>
        /// 아이템이 근처에 있을때 판정
        /// </summary>
        public Action ClosedItemEvent;

        float resetTimer = 0; // 이벤트를 호출할 친구를 초기화 시키는 것
        #endregion

        public PlayerStatus Status => _status;
        public PhotonView PhotonView => _photonview;

        public Vector3 PlayerMoveVelocity => _playerMoveVelocity;

        public Animator Animator => _animator;
        public PlayerWeaponController WeaponController => _weaponController;
        public PlayerPickupController PickupController => _pickupController;
        public PlayerStateController StateController => _stateController;
        public Camera MyCamera => _myCamera;
        public RotateToMouseScript RotateToMouseScript => _rotateToMouseScript;  //플레이어 회전용 변수

        public PhotonView IphotonView { get => _photonview; }//0410 18:42 이희웅 포톤뷰 인터페이스 추가

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

            #region 04.11 승현 추가 
            resetTimer += Time.deltaTime; // 매 프레임이 아닌 초마다 실행 시킬 조건
            #endregion  
        }

        /// <summary>
        /// Result씬에서 플레이어를 생성했을 시 호출
        /// </summary>
        /// <param name="rank">순위</param>
        private void SetWinPlayer(int rank) {
            transform.position = _data.DefaultWinScenePosition(rank);
            transform.eulerAngles = _data.DefaultWinSceneRotation();
            transform.localScale = Vector3.one * .5f;
            _weaponController.AllWeaponFalse();
            _stateController.ChangeState(new PlayerState_Win(this));
        }

        public void SetCamera(Camera camera) => _myCamera = camera;

        /// <summary>
        /// 가드 렐릭 습득or 제거시 활성화
        /// </summary>
        /// <param name="trigger"></param>
        public void SetGuard(bool trigger)
        {
            _guardController.gameObject.SetActive(trigger);
        }

        /// <summary>
        /// 실드 렐릭 습득or 제거시 활성화
        /// </summary>
        /// <param name="trigger"></param>
        public void SetShield(bool trigger)
        {
            _shieldController.gameObject.SetActive(trigger);
        }

        /// <summary>
        /// 플레이어 이동 상황 체크
        /// </summary>
        /// <returns></returns>
        public bool isMoving()
        {
            if (moveX == 0 && moveZ == 0)
                return false;

            #region 승현 추가 04.11

            //MapEvent?.Invoke();
            //ClosedItemEvent?.Invoke();            

            #endregion
            return true;
        }

        /// <summary>
        /// Bool파라미터로 애니메이션 체인지
        /// </summary>
        /// <param name="animation"></param>
        public void ChangeBoolAnimation(string animation) //0408 이희웅 움직임 애니메이션 추가
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
        /// int파라미터로 애니메이션 체인지
        /// </summary>
        /// <param name="state"></param>
        public void ChangeIntigerAnimation(Define.PlayerState state)//0408 16:38분 이희웅 업데이트 추가
        {
            if (_photonview.IsMine)
                _animator.SetInteger("State", (int)state);
        }

        /// <summary>
        /// trigger파라미터로 애니메이션 체인지
        /// </summary>
        /// <param name="state"></param>
        public void ChangeTriggerAnimation(Define.PlayerState state)//0408 16:38분 이희웅 업데이트 추가
        {
            if (IsTestMode.Instance.CurrentUser == Define.User.Hw)
            {
                if (_photonview.IsMine)
                    _animator.SetTrigger(state.ToString());
            }
            else
                _animator.SetTrigger(state.ToString());

        }
        float _time;
        /// <summary>
        /// 플레이어 이동 로직
        /// </summary>
        public void OnMoveUpdate()
        {
            _time += Time.deltaTime;
            if (IsTestMode.Instance.CurrentUser == Define.User.Hw)
            {
                if (_photonview.IsMine)//0405 09:41분 캐릭터간에 동기화를 위한 포톤 이동 분리 로직 추가
                {
                    Vector3 dir = new Vector3(moveX, 0f, moveZ);
                    //_rigid.MovePosition(_rigid.position + dir * (_status.MoveSpeed * _status.MoveSpeedDecrease) * Time.deltaTime);
                    //transform.parent.Translate(dir * (_status.MoveSpeed * _status.MoveSpeedDecrease) * Time.deltaTime); //0410 23:44 이희웅 포톤 동기화 문제로 인해 해당기능 주석처리 
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
            else
            {
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
        /// 플레이어 State사망으로 변경 및 사망 애니메이션 출력
        /// </summary>
        /// <param name="attacker"></param>
        public void OnDieUpdate(GameObject attacker)
        {
            if (_photonview.IsMine)
            {
                transform.LookAt(attacker.transform.position);
                _collider.enabled = false;
                _rigid.isKinematic = true;
                _rotateToMouseScript.PlayerDead();
                transform.position += Vector3.up;
                _animator.SetBool("Move", false);
                _animator.SetBool("Idle", false);
                ChangeTriggerAnimation(Define.PlayerState.Die);
                StartCoroutine(PlayerRespawn());
            }
        }

        /// <summary>
        /// 플레이어 부활 재배치 로직
        /// </summary>
        IEnumerator PlayerRespawn()
        {
            yield return new WaitForSeconds(_status.ResurrectionTime);
            if (_photonview.IsMine)//0418 이희웅 추가 모든 플레이어가 실행되기 때문에 자기 플레이어만 실행되도록
            {
                _stateController.ChangeState(new PlayerState_Idle(this));
                Status.SetHp(Status.MaxHp - Status.CurrentHp);
                _collider.enabled = true;
                _rigid.isKinematic = false;
                _rotateToMouseScript.PlayerRespawn();
                Transform tr = RespawnManager.Instance.RespawnPoints;
                transform.position = tr.GetChild(UnityEngine.Random.Range(0, tr.childCount - 1)).position;

                HpEvent.Invoke(Status.CurrentHp, Status.MaxHp);
                RangedWeapon weapon = _weaponController.RangedWeapon as RangedWeapon;
                weapon.InitBullet();
                BulletEvent.Invoke(weapon.CurrentBullet, weapon.MaxBullet);
            }
        }

        /// <summary>
        /// 플레이어 사망 이벤트(애니메이션에서 이벤트로 호출)
        /// </summary>
        public void OnDieEvent()
        {

        }

        /// <summary>
        /// 플레이어 피격 판정(피격 데미지, 공격자)
        /// </summary>
        /// <param name="amout"></param>
        /// <param name="attacker"></param>
        public void TakeDamage(int amout, GameObject attacker)
        {
            if (amout <= 0)
                return;

            int hp = _status.SetHp(-amout);
            Debug.Log($"플레이어가 데미지를{amout}만큼 입음");
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
        public void SetDropItemName(int dropObjectViewId)//0414 이희웅 포톤 드랍아이템 이름재지정
        {
            PhotonView _photonView = PhotonNetwork.GetPhotonView(dropObjectViewId);

            int index = _photonView.transform.gameObject.name.IndexOf("(Clone)");
            if (index > 0)
                _photonView.transform.gameObject.name = _photonView.transform.gameObject.name.Substring(0, index);
        }

        [PunRPC]//0413 03:46 이희웅 포톤용 메서드 추가 플레이어 이름지정
        public void RenamePlayer(int PlayerViewId)
        {
            PhotonNetwork.GetPhotonView(PlayerViewId).transform.parent.name = $"Player{PhotonNetwork.GetPhotonView(PlayerViewId).Owner.ActorNumber}";
        }

        [PunRPC]
        public void TakeDamagePhoton(int amout, int attackerViewNum)//0410 19:24 이희웅 포톤용 메서드 추가  
        {
            if (amout <= 0)
                return;

            int hp = _status.SetHp(-amout);
            HpEvent?.Invoke(_status.CurrentHp, _status.MaxHp);
            if (hp <= 0)
            {
                _droplable.Drop(transform.position);
                _attacker = PhotonNetwork.GetPhotonView(attackerViewNum).gameObject; //PunRpc에서 GameObject를 직렬화 해서 보낼 수 없기에 직렬화 해서 보낼 수 있는 attackerViewNum을 보내고 해당 attackerViewNum을 포톤네트워크에서 찾아서 넣어준다.
                _stateController.ChangeState(new PlayerState_Die(this, _attacker));
            }
        }

        [PunRPC]
        public void Replacedweapon(string beforeItemID, int ViewID)
        {
            PhotonView controller = PhotonNetwork.GetPhotonView(ViewID);
            if (PhotonNetwork.IsMasterClient)
            {
                GameObject ChangeWeaponObject = PhotonNetwork.Instantiate($"Prefabs/yb/Weapon/{Managers.ItemDataBase.GetItemData(beforeItemID).itemName}", Vector3.zero, Quaternion.identity);
                ChangeWeaponObject.transform.position = controller.transform.position + Vector3.up;
                int index = ChangeWeaponObject.transform.gameObject.name.IndexOf("(Clone)");
                if (index > 0)
                    ChangeWeaponObject.transform.gameObject.name = ChangeWeaponObject.transform.gameObject.name.Substring(0, index);
            }
        }


        [PunRPC]
        public void SetItemBox(int ItemBoxPCS)
        {
            Transform itemBoxtransform = GameObject.Find("ItemBox").GetComponent<Transform>();
            Transform itemBoxOriginaltransform = GameObject.Find("@Obj_Root/Map/ItemBox").GetComponent<Transform>();
            for (int i = 0; i < ItemBoxPCS; i++)
            {
                if (PhotonNetwork.IsMasterClient)
                {
                    GameObject itembox = PhotonNetwork.Instantiate("Prefabs/yb/Object/DestructibleObject", itemBoxOriginaltransform.GetChild(i).transform.position,Quaternion.identity);
                    itembox.transform.SetParent(itemBoxtransform);
                }
            }
        }
    }
}

