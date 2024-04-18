using Photon.Pun;
using UnityEngine;

namespace yb
{
    /// <summary>
    /// 획득 가능한 아이템 관련 포톤 인터페이스 0410 19:27분 이희웅 추가
    /// </summary>
    public interface IObtainableObjectPhoton
    {
        public PhotonView IObtainableObjectPhotonView { get; }
        public string NamePhoton { get; }  //아이템의 이름

        void PickupPhoton(int playerViewId);


    }
}