using Photon.Pun;
using UnityEngine;

namespace yb
{
    /// <summary>
    /// ȹ�� ������ ������ ���� ���� �������̽� 0410 19:27�� ����� �߰�
    /// </summary>
    public interface IObtainableObjectPhoton
    {
        public PhotonView IObtainableObjectPhotonView { get; }
        public string NamePhoton { get; }  //�������� �̸�

        void PickupPhoton(int playerViewId);


    }
}