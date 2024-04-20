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
/// ������ �Ⱦ����� Ŭ����
/// </summary>
namespace yb
{
    public class PlayerPickupController : MonoBehaviour
    {
        private PlayerController _player;
        private Data _data;
        private IObtainableObject _collideItem;  //�÷��̾�� �浹���� ������ ����
        private IObtainableObjectPhoton _collideItemPhoton;//�÷��̾�� �浹���� ������ ����

        private bool[] _haveRelic = new bool[(int)Define.RelicType.Count];  //�÷��̾ ������ ��� ����

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
        /// �÷��̾ �����۰� �浹���� ��, Ư�� Ű �Է½� ������ ����
        /// </summary>
        private void OnPickupUpdate()
        {
            if (IsTestMode.Instance.CurrentUser == Define.User.Hw)//0411 07:42 ����� ���浿��ȭ �׽�Ʈ��� �߰�
            {
                if (_collideItemPhoton == null) //0411 18:29 ����� ����� �׽�Ʈ �Լ� �߰�
                    return;
            }
            else
            {
                if (_collideItem == null)
                    return;
            }

            //if (_collideItem == null) //0411 18:29 ����� ����� �׽�Ʈ �Լ��� ���� �ּ�ó��
            //    return;

            if (IsTestMode.Instance.CurrentUser == Define.User.Hw)//0411 07:42 ����� ���浿��ȭ �׽�Ʈ��� �߰�
            {
                if (Input.GetKeyDown(KeyCode.G))
                {
                    if (_player.PhotonView.IsMine)//�÷��̾ ������ ��� �÷��̾ ���� �� �ֱ� ������ 
                    {
                        if (_collideItemPhoton is IRelic)
                        {
                            var item = _collideItemPhoton as IRelic;
                            if (_haveRelic[(int)item.RelicType])
                            {
                                Debug.Log("�̹� ������ �����Դϴ�");
                                return;
                            }
                            if (_player.HaveRelicNumber >= PlayerController.MaxRelicNumber)
                            {
                                Debug.Log("���� â�� ���� á���ϴ�.");
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
                    Debug.Log("����");
                    if (_collideItem is IRelic)
                    {
                        var item = _collideItem as IRelic;
                        if (_haveRelic[(int)item.RelicType])
                        {
                            Debug.Log("�̹� ������ �����Դϴ�");
                            return;
                        }
                        if (_player.HaveRelicNumber >= PlayerController.MaxRelicNumber)
                        {
                            Debug.Log("���� â�� ���� á���ϴ�.");
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
            Debug.Log($"{type}�������� ȹ���߽��ϴ�");
            Debug.Log($"{slot}�� ���Կ� {type}�� {_player.ItemList[slot].ItemNumber}�� �߰�");


        }
        /// <summary>
        /// �÷��̾ ������ ���� �� ���� �Ҵ�. �� ���� Ŭ�������� ȣ��
        /// </summary>
        /// <param name="relic"></param>

        public void SetRelic(IRelic relic)
        {

            _haveRelic[(int)relic.RelicType] = true;
            _player.WeaponController.SetRelic(relic);
            //_player.SetRelicEvent?.Invoke(RelicType.ToString());
            Debug.Log($"<color=red>{relic.RelicType.ToString()}������ ����</color>");
            switch (relic.RelicType)
            {
                case Define.RelicType.BonusResurrectionTimeRelic:
                    _player.Status.SetResurrectionTime(_data.BonusResurrectionTime);
                    break;
                case Define.RelicType.GuardRelic:
                    Debug.Log("���巼�� ����");
                    _player.SetGuard(true);
                    break;
                case Define.RelicType.ShieldRelic:
                    Debug.Log("���巼�� ����");
                    _player.SetShield(true);
                    break;
            }


        }

        /// <summary>
        /// �÷��̾ ������ ���������� �� ���� Ŭ�������� ȣ��
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


        // ������ �̸� ���� �Լ��� Interface�� ���� �ؼ� �־��ִ°� ���ƺ��δ�.
        private void OnTriggerEnter(Collider c)
        {
            if (c.CompareTag("ObtainableObject"))
            {
                if (IsTestMode.Instance.CurrentUser == Define.User.Hw) //0411 08:10 ����� ���� ����ȭ�� ���� �б� �߰�
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
                if (IsTestMode.Instance.CurrentUser == Define.User.Hw) //0411 08:10 ����� ���� ����ȭ�� ���� �б� �߰�
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
                if (IsTestMode.Instance.CurrentUser == Define.User.Hw) //0411 08:10 ����� ���� ����ȭ�� ���� �б� �߰�
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
