using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using yb;

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
        private IObtainableObjectPhoton _collideItemPhoton; //0411 07:46 ����� ���� ����ȭ�� ���� �������̽� �߰�


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
            //if (_collideItemPhoton == null) //0411 08:29 ����� ����� �׽�Ʈ �Լ� �߰�
            //    return;

            if (_collideItem == null) //0411 08:29 ����� ����� �׽�Ʈ �Լ��� ���� �ּ�ó��
                return;

            //if (IsTestMode.Instance.CurrentUser == Define.User.Hw)//0411 07:42 ����� ���浿��ȭ �׽�Ʈ��� �߰�
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
        /// �÷��̾ ������ ���� �� ���� �Ҵ�. �� ���� Ŭ�������� ȣ��
        /// </summary>
        /// <param name="relic"></param>
        public void SetRelic(IRelic relic)
        {
            _haveRelic[(int)relic.RelicType] = true;
            _player.WeaponController.SetRelic(relic);
            _player.RelicEvent?.Invoke((int)relic.RelicType);
            Debug.Log($"{relic.RelicType.ToString()}������ ����");
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
        /// �÷��̾ ������ ���������� �� ���� Ŭ�������� ȣ��
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

