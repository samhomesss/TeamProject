using Photon.Pun;
using System;
using UnityEngine;

namespace yb
{
    /// <summary>
    /// �������� ���� �� �ִ� ��� ������Ʈ�� ��ӹ޾ƾ���.
    /// </summary>
    public interface ITakeDamagePhoton
    {
        public PhotonView IphotonView { get; } //0410 18:42 ����� ����� �������̽� �߰�

        void TakeDamagePhoton(int amout, int attackerViewNum);//0410 19:00 ����� ����� �������̽� �߰�
    }
}