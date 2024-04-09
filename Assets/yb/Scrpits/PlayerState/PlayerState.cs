using Photon.Pun;
using UnityEngine;

namespace yb {
    /// <summary>
    /// 플레이어 상태 관련 클래스
    /// </summary>
    public class PlayerState {
        protected Data _data;  //기본 정보 변수

        public PlayerState() {
            _data = Managers.Data;  //상태가 바뀔 시 기본 정보 호출
        }
    }
}
