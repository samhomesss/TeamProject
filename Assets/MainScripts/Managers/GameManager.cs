using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState{
    None,
    Login,
    WaitUntilLoggedIn,
    InLobby,
    InGameReady,
    InGamePlay,
}
public class GameManager
{
    [SerializeField]private GameState _state;
    public GameState state//게임 정보를 부르거나 셋팅할 프로퍼티
    {
        get => _state;
        set
        {
            if (_state == value) return; //게임상태가 그대로면 리턴, 바뀌면 변경
            _state = value;
        }
    }

}
