using ExitGames.Client.Photon.StructWrapping;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using yb;
using static Define;

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
        private IObtainableObjectPhoton _collideItemPhoton;//플레이어와 충돌중인 아이템 저장

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
            if (IsTestMode.Instance.CurrentUser == Define.User.Hw)//0411 07:42 이희웅 포톤동기화 테스트모드 추가
            {
                if (_collideItemPhoton == null) //0411 18:29 이희웅 포톤용 테스트 함수 추가
                    return;
            }
            else
            {
                if (_collideItem == null)
                    return;
            }

            //if (_collideItem == null) //0411 18:29 이희웅 포톤용 테스트 함수를 위해 주석처리
            //    return;

            if (IsTestMode.Instance.CurrentUser == Define.User.Hw)//0411 07:42 이희웅 포톤동기화 테스트모드 추가
            {
                if (Input.GetKeyDown(KeyCode.G))
                {
                    if (_player.PhotonView.IsMine)//플레이어가 줏으면 모든 플레이어가 줏을 수 있기 때문에 
                    {
                        if (_collideItemPhoton is IRelic)
                        {
                            var item = _collideItemPhoton as IRelic;
                            if (_haveRelic[(int)item.RelicType])
                            {
                                Debug.Log("이미 보유한 렐릭입니다");
                                return;
                            }
                            if (_player.HaveRelicNumber >= PlayerController.MaxRelicNumber)
                            {
                                Debug.Log("렐릭 창이 가득 찼습니다.");
                                return;
                            }
                        }
                        _player.StateController.ChangeState(new PlayerState_Pickup(_player));
                        _player.ItemEvent?.Invoke(_collideItemPhoton.NamePhoton);
                        _collideItemPhoton.IObtainableObjectPhotonView.RPC("PickupPhoton", RpcTarget.All, _player.IphotonView.ViewID);
                        _collideItemPhoton.HideName();
                        _collideItemPhoton = null;
                    }

                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.G))
                {
                    Debug.Log("누름");
                    if (_collideItem is IRelic)
                    {
                        var item = _collideItem as IRelic;
                        if (_haveRelic[(int)item.RelicType])
                        {
                            Debug.Log("이미 보유한 렐릭입니다");
                            return;
                        }
                        if (_player.HaveRelicNumber >= PlayerController.MaxRelicNumber)
                        {
                            Debug.Log("렐릭 창이 가득 찼습니다.");
                            return;
                        }
                    }
                    _player.StateController.ChangeState(new PlayerState_Pickup(_player));
                    _collideItem.Pickup(_player);
                    _player.ItemEvent?.Invoke(_collideItem.Name);
                    _collideItem.HideName();
                }
            }
        }

        public void SetItem(int slot, Define.ItemType type)
        {
            if (_player.ItemList.ContainsKey(slot))
            {
                if (_player.ItemList[slot].ItemType == type)
                {
                    _player.ItemList[slot].ItemType = type;
                    _player.ItemList[slot].ItemNumber++;
                }
            }
            else
            {
                _player.ItemList.Add(slot, new PlayerController.Item(type, 1));
            }

            _player.SetItemEvent.Invoke(slot, _player.ItemList[slot]);
            Debug.Log($"{type}아이템을 획득했습니다");
            Debug.Log($"{slot}번 슬롯에 {type}을 {_player.ItemList[slot].ItemNumber}개 추가");


        }
        /// <summary>
        /// 플레이어가 렐릭을 습득 시 렐릭 할당. 각 렐릭 클래스에서 호출
        /// </summary>
        /// <param name="relic"></param>

        public void SetRelic(IRelic relic)
        {

            _haveRelic[(int)relic.RelicType] = true;
            _player.WeaponController.SetRelic(relic);
            //_player.SetRelicEvent?.Invoke(RelicType.ToString());
            Debug.Log($"<color=red>{relic.RelicType.ToString()}렐릭을 습득</color>");
            switch (relic.RelicType)
            {
                case Define.RelicType.BonusResurrectionTimeRelic:
                    _player.Status.SetResurrectionTime(_data.BonusResurrectionTime);
                    break;
                case Define.RelicType.GuardRelic:
                    Debug.Log("가드렐릭 습득");
                    _player.SetGuard(true);
                    break;
                case Define.RelicType.ShieldRelic:
                    Debug.Log("쉴드렐릭 습득");
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
            _haveRelic[(int)relic.RelicType] = false;
            _player.WeaponController.SetRelic(relic);

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


        // 아이템 이름 띄우는 함수를 Interface로 구현 해서 넣어주는게 좋아보인다.
        private void OnTriggerEnter(Collider c)
        {
            if (c.CompareTag("ObtainableObject"))
            {
                if (IsTestMode.Instance.CurrentUser == Define.User.Hw) //0411 08:10 이희웅 포톤 동기화를 위한 분기 추가
                {
                    if (_player.GetComponent<PhotonView>().IsMine)
                    {
                     _collideItemPhoton = c.GetComponent<IObtainableObjectPhoton>();
                    c.GetComponent<IObtainableObject>().ShowName(_player);
                    return;
                    }
                }
                else
                {

                    _collideItem = c.GetComponent<IObtainableObject>();
                    c.GetComponent<IObtainableObject>().ShowName(_player);
                    return;

                }
            }
        }

        private void OnTriggerStay(Collider c)
        {

            if (c.CompareTag("ObtainableObject"))
            {
                if (IsTestMode.Instance.CurrentUser == Define.User.Hw) //0411 08:10 이희웅 포톤 동기화를 위한 분기 추가
                {
                    if (_player.GetComponent<PhotonView>().IsMine)
                    {
                        _collideItemPhoton = c.GetComponent<IObtainableObjectPhoton>();
                        c.GetComponent<IObtainableObject>().ShowName(_player);
                        return;
                    }
                }
                else
                {
                    _collideItem = c.GetComponent<IObtainableObject>();
                    c.GetComponent<IObtainableObject>().ShowName(_player);
                    return;
                }
            }
        }

        private void OnTriggerExit(Collider c)
        {
            if (c.CompareTag("ObtainableObject"))
            {
                if (IsTestMode.Instance.CurrentUser == Define.User.Hw) //0411 08:10 이희웅 포톤 동기화를 위한 분기 추가
                {
                    if (_player.GetComponent<PhotonView>().IsMine)
                    {
                        _collideItemPhoton = null;
                        c.GetComponent<IObtainableObject>().HideName();
                    }
                }
                else
                {
                    if (_collideItem != null)
                    {
                        _collideItem = null;
                        c.GetComponent<IObtainableObject>().HideName();
                    }

                }
            }
            if (_player.GetComponent<PhotonView>().IsMine)
                c.GetComponent<IObtainableObject>().HideName();
        }
    }
}
