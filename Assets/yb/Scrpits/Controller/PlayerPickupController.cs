using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using yb;

/// <summary>
/// 아이템 픽업관련 클래스
/// </summary>
namespace yb
{
    public class PlayerPickupController : MonoBehaviour
    {
        private PlayerController _player;
        private Data _data;
        private IObtainableObject _collideItem;  //플레이어와 충돌중인 아이템 저장
        private IObtainableObjectPhoton _collideItemPhoton; //0411 07:46 이희웅 포톤 동기화를 위한 인터페이스 추가


        private bool[] _haveRelic = new bool[(int)Define.RelicType.Count];  //플레이어가 보유한 모든 렐릭

        public bool[] IsRelic() => _haveRelic;

        private void Awake()
        {
            _player = GetComponent<PlayerController>();
        }
        void Start()
        {
            _data = Managers.Data;
        }

        private void Update()
        {
            OnPickupUpdate();
        }

        /// <summary>
        /// 플레이어가 아이템과 충돌중일 때, 특정 키 입력시 아이템 습득
        /// </summary>
        private void OnPickupUpdate()
        {
            //if (_collideItemPhoton == null) //0411 08:29 이희웅 포톤용 테스트 함수 추가
            //    return;

            if (_collideItem == null) //0411 08:29 이희웅 포톤용 테스트 함수를 위해 주석처리
                return;

            //if (IsTestMode.Instance.CurrentUser == Define.User.Hw)//0411 07:42 이희웅 포톤동기화 테스트모드 추가
            //{
            //    if (Input.GetKeyDown(KeyCode.G))
            //    {
            //        _player.StateController.ChangeState(new PlayerState_Pickup(_player));
            //        _collideItemPhoton.IObtainableObjectPhotonView.RPC("PickupPhoton", RpcTarget.All, _player.IphotonView.ViewID);
            //        _player.ItemEvent?.Invoke(_collideItemPhoton.NamePhoton);
            //    }
            //}
                if (Input.GetKeyDown(KeyCode.G))
                {
                    _player.StateController.ChangeState(new PlayerState_Pickup(_player));
                    _collideItem.Pickup(_player);
                    _player.ItemEvent?.Invoke(_collideItem.Name);
                }
            }


        /// <summary>
        /// 플레이어가 렐릭을 습득 시 렐릭 할당. 각 렐릭 클래스에서 호출
        /// </summary>
        /// <param name="relic"></param>
        public void SetRelic(IRelic relic)
        {
            _haveRelic[(int)relic.RelicType] = true;
            _player.WeaponController.SetRelic(relic);
            _player.RelicEvent?.Invoke((int)relic.RelicType);
            Debug.Log($"{relic.RelicType.ToString()}렐릭을 습득");
            switch (relic.RelicType)
            {
                case Define.RelicType.BonusResurrectionTimeRelic:
                    _player.Status.SetResurrectionTime(_data.BonusResurrectionTime);
                    break;
                case Define.RelicType.GuardRelic:
                    _player.SetGuard(true);
                    break;
                case Define.RelicType.ShieldRelic:
                    _player.SetShield(true);
                    break;
            }
        }

        /// <summary>
        /// 플레이어가 렐릭을 삭제했을시 각 렐릭 클래스에서 호출
        /// </summary>
        /// <param name="relic"></param>
        public void DeleteRelic(IRelic relic)
        {
            _haveRelic[(int)relic.RelicType] = true;
            _player.WeaponController.SetRelic(relic);
            _player.RelicEvent?.Invoke((int)relic.RelicType);

            switch (relic.RelicType)
            {
                case Define.RelicType.BonusResurrectionTimeRelic:
                    _player.Status.SetResurrectionTime(_data.DefaultResurrectionTime);
                    break;
                case Define.RelicType.GuardRelic:
                    _player.SetGuard(false);
                    break;
                case Define.RelicType.ShieldRelic:
                    _player.SetShield(false);
                    break;
            }
        }


        private void OnTriggerEnter(Collider c)
        {
            if (c.CompareTag("ObtainableObject"))
            {
                    _collideItem = c.GetComponent<IObtainableObject>();
                    return;
            }
        }

        private void OnTriggerExit(Collider c)
        {
            if (c.CompareTag("ObtainableObject"))
                    if (_collideItem != null)
                        _collideItem = null;
        }
    }
}

