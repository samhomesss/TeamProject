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
        private readonly float _animationFadeTime = .3f;  //애니메이션 페이드 시간
        public const int MaxRelicNumber = 2;
        private int _playerHandle;  //플레이어 고유 번호
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
        public int HaveItemNumber { get; set; }
        public int HaveRelicNumber { get; set; }

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

            //사망시 set해둔 아이템 드랍
            _droplable.Set("ObtainableRifle");
            _droplable.Set("ObtainablePistol");
            _droplable.Set("ObtainableShotgun");

        }

        private void Update()
        {
            moveX = Input.GetAxisRaw("Horizontal");
            moveZ = Input.GetAxisRaw("Vertical");

            #region 04.11 승현 추가 
            resetTimer += Time.deltaTime; // 매 프레임이 아닌 초마다 실행 시킬 조건
            #endregion  
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
            if(IsTestMode.Instance.CurrentUser == Define.User.Hw) {
                if (_photonview.IsMine)
                    _animator.SetTrigger(state.ToString());
            }
            else
                _animator.SetTrigger(state.ToString());

        }

        /// <summary>
        /// 플레이어 이동 로직
        /// </summary>
        public void OnMoveUpdate()
        {
            if (IsTestMode.Instance.CurrentUser == Define.User.Hw)
            {
                if (_photonview.IsMine)//0405 09:41분 캐릭터간에 동기화를 위한 포톤 이동 분리 로직 추가
                {
                    Vector3 dir = new Vector3(moveX, 0f, moveZ);
                    //_rigid.MovePosition(_rigid.position + dir * (_status.MoveSpeed * _status.MoveSpeedDecrease) * Time.deltaTime);
                    //transform.parent.Translate(dir * (_status.MoveSpeed * _status.MoveSpeedDecrease) * Time.deltaTime); //0410 23:44 이희웅 포톤 동기화 문제로 인해 해당기능 주석처리 

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
        /// 플레이어 State사망으로 변경 및 사망 애니메이션 출력
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
        /// 플레이어 사망 이벤트(애니메이션에서 이벤트로 호출)
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


        [PunRPC]
        public void SetDropItemName(int dropObjectViewId)
        {
            PhotonView _photonView = PhotonNetwork.GetPhotonView(dropObjectViewId);

            int index = _photonView.transform.gameObject.name.IndexOf("(Clone)");
            if (index > 0)
                _photonView.transform.gameObject.name = _photonView.transform.gameObject.name.Substring(0, index);
        }

        [PunRPC]//0413 03:46 이희웅 포톤용 메서드 추가 플레이어 이름지정
        public void RenamePlayer(int PlayerViewId)
        {
            Debug.Log($"{PhotonNetwork.GetPhotonView(PlayerViewId)}플레이어가 들어왔습니다{PhotonNetwork.LocalPlayer.ActorNumber}");
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
    }
}

