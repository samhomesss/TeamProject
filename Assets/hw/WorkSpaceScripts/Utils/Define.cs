using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hw
{
    public class Define
    {
        public enum Scene
        {
            Unknown,
            Login,
            Lobby,
            JoinedRoom, //3.31일 20:08 분 이희웅 JoinedRoom추가 완료
            Game,
        }

        public enum Sound
        {
            Bgm,
            Effect,
            MaxCount,
        }

        public enum UIEvent
        {
            Click,
            Drag,
        }

        public enum MouseEvent
        {
            Press,
            Click,
        }

        public enum CameraMode
        {
            QuarterView,
        }
    }


}
