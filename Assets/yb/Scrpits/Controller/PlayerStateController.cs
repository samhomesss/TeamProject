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
        void Start() { }  //�÷��̾��� �⺻ ���¸� idle�� ����

        
        void Update() {
            if(_playerState == null) return;
                 _playerState.OnUpdate(_player);
        }

        public void ChangeState(IPlayerState playerState) => _playerState = playerState;

        /// <summary>
        /// todo
        /// �̸� ���� �ʿ�
        /// ������ �Ⱦ��� ������ ������ Idle���·� �ٲٴ� �̺�Ʈ
        /// �ִϸ��̼ǿ��� �̺�Ʈ�� ȣ��
        /// </summary>
        public void OnPickupEvent() => ChangeState(new PlayerState_Idle(_player));
    }
}

