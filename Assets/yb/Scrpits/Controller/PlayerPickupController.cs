using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using yb;

/// <summary>
/// 아이템 픽업관련 클래스
/// </summary>
namespace yb {
    public class PlayerPickupController : MonoBehaviour {
        private PlayerController _player;
        private Data _data;
        private IObtainableObject _collideItem;  //플레이어와 충돌중인 아이템 저장

        private bool[] _haveRelic = new bool[(int)Define.RelicType.Count];  //플레이어가 보유한 모든 렐릭

        public bool[] IsRelic() => _haveRelic;

        private void Awake() {
            _player = GetComponent<PlayerController>();
        }
        void Start() {
            _data = Managers.Data;
        }

        private void Update() {
            OnPickupUpdate();
        }

        /// <summary>
        /// 플레이어가 아이템과 충돌중일 때, 특정 키 입력시 아이템 습득
        /// </summary>
        private void OnPickupUpdate() {
            if (_collideItem == null)
                return;

            if (Input.GetKeyDown(KeyCode.G)) {
                _player.StateController.ChangeState(new PlayerState_Pickup(_player));
                _collideItem.Pickup(_player);
                _player.ItemEvent?.Invoke(_collideItem.Name);
                _collideItem.HideName();
            }
        }

        /// <summary>
        /// 플레이어가 렐릭을 습득 시 렐릭 할당. 각 렐릭 클래스에서 호출
        /// </summary>
        /// <param name="relic"></param>
        public void SetRelic(IRelic relic) {
            _haveRelic[(int)relic.RelicType] = true;
            _player.WeaponController.SetRelic(relic);
            _player.SetRelicEvent?.Invoke(relic.RelicType.ToString());
            Debug.Log($"{relic.RelicType.ToString()}렐릭을 습득");
            switch(relic.RelicType) {
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
        public void DeleteRelic(IRelic relic) {
            _haveRelic[(int)relic.RelicType] = true;
            _player.WeaponController.SetRelic(relic);
            _player.DestroyRelicEvent?.Invoke(relic.RelicType.ToString());

            switch (relic.RelicType) {
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
        private void OnTriggerEnter(Collider c) {
            if (c.CompareTag("ObtainableObject")) {
                _collideItem = c.GetComponent<IObtainableObject>();
                c.GetComponent<IObtainableObject>().ShowName(_player);
                return;
            }
        }

        private void OnTriggerExit(Collider c) {
            if (c.CompareTag("ObtainableObject"))
                if (_collideItem != null)
                {
                    _collideItem = null;
                    c.GetComponent<IObtainableObject>().HideName();
                    
                }
        }
    }
}

