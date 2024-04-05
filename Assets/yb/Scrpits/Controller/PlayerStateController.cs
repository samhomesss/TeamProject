using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using yb;

namespace yb {
    public class PlayerStateController : MonoBehaviour {
        private IPlayerState _playerState;
        private PlayerController _player;
        private IObtainableObject _collideItem;

        private void Awake() => _player = GetComponent<PlayerController>();
        void Start() => _playerState = new PlayerState_Idle(_player);

        void Update() {
            _playerState.OnUpdate(_player);
            OnPickupUpdate();
        }

        public void ChangeState(IPlayerState playerState) => _playerState = playerState;
        public void OnPickupEvent() => ChangeState(new PlayerState_Idle(_player));

        private void OnPickupUpdate() {
            if (_collideItem == null)
                return;

            if (Input.GetKeyDown(KeyCode.G)) {
                ChangeState(new PlayerState_Pickup(_player));
                _collideItem.Pickup(_player);
            }
        }

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

