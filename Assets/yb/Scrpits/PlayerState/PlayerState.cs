using Photon.Pun;
using UnityEngine;

namespace yb {
    /// <summary>
    /// �÷��̾� ���� ���� Ŭ����
    /// </summary>
    public class PlayerState {
        protected Data _data;  //�⺻ ���� ����

        public PlayerState() {
            _data = Managers.Data;  //���°� �ٲ� �� �⺻ ���� ȣ��
        }
    }
}
