using ExitGames.Client.Photon;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Define;


public class GameManager : MonoBehaviour
{

    GameManager _instance;
    private Define.GameState _state;
    
    public Define.GameState state//���� ������ �θ��ų� ������ ������Ƽ
    {
        get => _state;
        set
        {
            if (_state == value) return; //���ӻ��°� �״�θ� ����, �ٲ�� ����
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
        if (IsTestMode.Instance.CurrentUser == User.Hw)
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
                    Managers.Scene.LoadScene(SceneType.LoginScene);
                    //SceneManager.LoadScene("LoginScene");
                    _state++;
                }
                break;
            case Define.GameState.Login:
                {
                    if (LoginInformation.loggedIn && LoginInformation.profile != null)//�α����� �Ȼ��� + �α����� �ż� ���������� ����ٸ� �κ�� ����
                    {
                        //Todo Create Class LoginInfomation.loggedin, LoginInfomation.profile 
                        if (PhotonManager.instance) //���� �Ŵ��� �ν��Ͻ��� ����� �α���
                        {
                            Managers.Scene.LoadScene(SceneType.LobbyScene);
                            //SceneManager.LoadScene("LobbyScene");
                            _state++;
                        }
                    }
                }
                break;
            case Define.GameState.InLobby:
                break;
            case Define.GameState.InGamePlay:
                break;
            default:
                break;
        }
    }
}

