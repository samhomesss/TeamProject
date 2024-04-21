using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using yb;

/// <summary>
/// 플레이어 상태 관리 클래스
/// </summary>
namespace yb {
    public class PlayerStateController : MonoBehaviour {
        private IPlayerState _playerState;  //현재 플레이어의 상태
        private PlayerController _player;

        public IPlayerState State => _playerState;

        private void Awake() => _player = GetComponent<PlayerController>();
        void Start() => _playerState = new PlayerState_Idle(_player);  //플레이어의 기본 상태를 idle로 지정

        
        void Update() {
            //플레이어의 현재 상태에 맞는 Update함수 출력
            _playerState.OnUpdate(_player);
        }

        public void ChangeState(IPlayerState playerState) => _playerState = playerState;

        /// <summary>
        /// todo
        /// 이름 변경 필요
        /// 아이템 픽업이 끝나면 강제로 Idle상태로 바꾸는 이벤트
        /// 애니메이션에서 이벤트로 호출
        /// </summary>
        public void OnPickupEvent() => ChangeState(new PlayerState_Idle(_player));
    }
}

