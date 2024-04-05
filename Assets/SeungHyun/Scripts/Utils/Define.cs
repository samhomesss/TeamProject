using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sh
{
    public class Define
    {
        public enum SceneType
        {
            None,
            Login,
            Lobby,
            InGame,
        }

        public enum UIEvent
        {
            Click,
            Drag,
        }

        public enum MouseEventType
        {
            Press,
            Click,
        }

        // 현재는 안쓴다고 판단.
        public enum CameraMode
        {
            QuarterView,
        }
    }


}
