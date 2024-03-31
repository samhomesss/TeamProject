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
    public GameState state//���� ������ �θ��ų� ������ ������Ƽ
    {
        get => _state;
        set
        {
            if (_state == value) return; //���ӻ��°� �״�θ� ����, �ٲ�� ����
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
                    if (LoginInformation.loggedIn && LoginInformation.profile != null)//�α����� �Ȼ��� + �α����� �ż� ���������� ����ٸ� �κ�� ����
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
