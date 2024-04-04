using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hw
{
    public class LobbyScene : BaseScene
    {
        protected override void Init()
        {
            base.Init();

            SceneType = Define.Scene.Lobby;

            Managers.UI.ShowSceneUI<CreateRoom>("Lobby/CreateRoom");
            Managers.UI.ShowSceneUI<LobbyUI>("Lobby/LobbyUI");
            Managers.UI.ShowSceneUI<RoomListSlot>("Lobby/RoomListSlot");
        }
        public override void Clear()
        {
        }
    }
}

