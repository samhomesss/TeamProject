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


    public void Workflow()
    {
        switch (_state)
        {
            case GameState.None:
                break;
            case GameState.Login:
                {
                    SceneManager.LoadScene("LoginScene");
                    _state++;
                }
                break;
            case GameState.WaitUntilLoggedIn:
                {
                    if (LoginInformation.loggedIn && LoginInformation.profile != null)//로그인이 된상태 + 로그인이 돼서 프로파일이 생긴다면 로비로 입장
                    {
                        //Todo Create Class LoginInfomation.loggedin, LoginInfomation.profile 
                        //if (PhotonManager.instance)
                        //{
                        //    SceneManager.LoadScene("LobbyScene");
                        //    _state++;
                        //}
                        //Todo Create PhotenManager
                    }
                }
                break;
            case GameState.InLobby:
                break;
            case GameState.InGameReady:
                break;
            case GameState.InGamePlay:
                break;
            default:
                break;
        }
    }
}
