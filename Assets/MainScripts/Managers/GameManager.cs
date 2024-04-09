using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Define;


public class GameManager : MonoBehaviour
{

    GameManager _instance;
    private Define.GameState _state;

    public Define.GameState state//게임 정보를 부르거나 셋팅할 프로퍼티
    {
        get => _state;
        set
        {
            if (_state == value) return; //게임상태가 그대로면 리턴, 바뀌면 변경
            _state = value;
        }
    }

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(_instance);

        DontDestroyOnLoad(this);
    }
    public void Update()
    {
        //todo
        if(IsTestMode.Instance.CurrentUser == User.Hw)
        {
            ProgramWorkFlow();
        }
    }

    public void ProgramWorkFlow()
    {
        switch (_state)
        {
            case Define.GameState.None:
                {
                    SceneManager.LoadScene("LoginScene");
                    _state++;
                }
                break;
            case Define.GameState.Login:
                {
                    if (LoginInformation.loggedIn && LoginInformation.profile != null)//로그인이 된상태 + 로그인이 돼서 프로파일이 생긴다면 로비로 입장
                    {
                        //Todo Create Class LoginInfomation.loggedin, LoginInfomation.profile 
                        if (PhotonManager.instance) //포톤 매니저 인스턴스가 생기면 로그인
                        {
                            SceneManager.LoadScene("LobbyScene");
                            _state++;
                        }
                    }
                }
                break;
            case Define.GameState.InLobby:
                //TODO: 로비에 있을때 마스터 유저가 Start버튼을 누르면 Game씬으로 전환 되게끔 작성
                break;
            case Define.GameState.InGamePlay:
                break;
            default:
                break;
        }
    }
}

