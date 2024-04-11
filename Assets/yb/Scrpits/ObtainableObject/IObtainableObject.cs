using Photon.Pun;
using UnityEngine;

namespace yb {
    /// <summary>
    /// 획득 가능한 아이템 관련 인터페이스
    /// </summary>
    public interface IObtainableObject {
        public string Name { get; }  //아이템의 이름
        void Pickup(PlayerController player);  //픽업 함수

    }
}