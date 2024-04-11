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
        void PickupPhoton(int PlayerViewID);  //픽업 함수, 파라미터는 직렬화 할 수 있는 플레이어의 VIewID를 넣어둠
    }
}