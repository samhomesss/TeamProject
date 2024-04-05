using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using yb;

namespace yb {
    public class PlayerPickupController : MonoBehaviour {
        private PlayerController _player;
        private Data _data;
        private IObtainableObject _collideItem;

        private bool[] _haveRelic = new bool[(int)Define.RelicType.Count];

        private void Awake() {
            _player = GetComponent<PlayerController>();
        }
        void Start() {
            _data = Managers.Data;
        }

        private void Update() {
            OnPickupUpdate();

        }

        private void OnPickupUpdate() {
            if (_collideItem == null)
                return;

            if (Input.GetKeyDown(KeyCode.G)) {
                _player.StateController.ChangeState(new PlayerState_Pickup(_player));
                _collideItem.Pickup(_player);
                _player.PlayerEvent.Item5?.Invoke(_collideItem.Name);
            }
        }
        public void SetRelic(IRelic relic) {
            _haveRelic[(int)relic.RelicType] = true;
            _player.WeaponController.SetRelic(relic);
            _player.Status.SetResurrectionTime(_data.BonusResurrectionTime);
            _player.PlayerEvent.Item4?.Invoke((int)relic.RelicType);
        }

        public void DeleteRelic(IRelic relic) {
            _haveRelic[(int)relic.RelicType] = true;
            _player.WeaponController.SetRelic(relic);
            _player.Status.SetResurrectionTime(_data.DefaultResurrectionTime);
            _player.PlayerEvent.Item4?.Invoke((int)relic.RelicType);
        }

        public bool[] IsRelic() => _haveRelic;

        private void OnTriggerEnter(Collider c) {
            if (c.CompareTag("ObtainableObject")) {
                _collideItem = c.GetComponent<IObtainableObject>();
                return;
            }
        }

        private void OnTriggerExit(Collider c) {
            if (c.CompareTag("ObtainableObject"))
                if (_collideItem != null)
                    _collideItem = null;
        }
    }
}

