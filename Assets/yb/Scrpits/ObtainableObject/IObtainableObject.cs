using Photon.Pun;
using UnityEngine;

namespace yb {
    /// <summary>
    /// ȹ�� ������ ������ ���� �������̽�
    /// </summary>
    public interface IObtainableObject {
        public string Name { get; }  //�������� �̸�
        void Pickup(PlayerController player);  //�Ⱦ� �Լ�

        public PhotonView iObtainableObjectPhotonview { get; } //0411 15:34 ����� ����� ������Ƽ �߰�
    }
}