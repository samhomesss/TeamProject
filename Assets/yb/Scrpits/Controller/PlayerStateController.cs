using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using yb;

namespace yb {
    public class PlayerStateController : MonoBehaviour {
        private IPlayerState _playerState;
        private PlayerController _player;

        private void Awake() => _player = GetComponent<PlayerController>();
        void Start()=>_playerState = new PlayerState_Idle(_player);

        void Update() {
            _playerState.OnUpdate(_player);
        }

        public void ChangeState(IPlayerState playerState) => _playerState = playerState;
        public void OnPickupEvent() => ChangeState(new PlayerState_Idle(_player));
    }
}

