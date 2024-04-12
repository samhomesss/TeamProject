using Photon.Pun;
using System;
using UnityEngine;

namespace yb
{
    /// <summary>
    /// 데미지를 입을 수 있고 포톤을 통한 동기화가 필요한 모든 오브젝트가 상속 받아야함
    /// </summary>
    public interface ITakeDamagePhoton
    {
        public PhotonView IphotonView { get; } //0410 18:42 이희웅 포톤뷰 인터페이스 추가

        void TakeDamagePhoton(int amout, int attackerViewNum);//0410 19:00 이희웅 포톤뷰 인터페이스 추가
    }
}