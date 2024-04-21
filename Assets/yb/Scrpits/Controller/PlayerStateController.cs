using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using yb;

/// <summary>
/// �÷��̾� ���� ���� Ŭ����
/// </summary>
namespace yb {
    public class PlayerStateController : MonoBehaviour {
        private IPlayerState _playerState;  //���� �÷��̾��� ����
        private PlayerController _player;

        public IPlayerState State => _playerState;

        private void Awake() => _player = GetComponent<PlayerController>();
        void Start() {
            if (_playerState == null && _playerState is not PlayerState_Win) {
                ChangeState(new PlayerState_Idle(_player));
            }

        }


        void Update() {
            if(_playerState != null)
                 _playerState.OnUpdate(_player);

            Debug.Log($"{_playerState}���� ������Ʈ");
        }

        public void ChangeState(IPlayerState playerState) { 
            _playerState = playerState;
            Debug.Log($"{_playerState}�� ���� ����");
        }

        /// <summary>
        /// todo
        /// �̸� ���� �ʿ�
        /// ������ �Ⱦ��� ������ ������ Idle���·� �ٲٴ� �̺�Ʈ
        /// �ִϸ��̼ǿ��� �̺�Ʈ�� ȣ��
        /// </summary>
        public void OnPickupEvent() => ChangeState(new PlayerState_Idle(_player));
    }
}

