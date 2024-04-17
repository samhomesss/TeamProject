using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


    public class LobbyScene : BaseScene
    {
        public override void Init()
        {
            base.Init();

            SceneType = Define.SceneType.Lobby;

            Managers.UI.ShowSceneUI<CreateRoom>("Lobby/CreateRoom", "hw");
            Managers.UI.ShowSceneUI<LobbyUI>("Lobby/LobbyUI", "hw");
            Managers.UI.ShowSceneUI<RoomListSlot>("Lobby/RoomListSlot", "hw");
        }
        public override void Clear()
        {
        }
    }

