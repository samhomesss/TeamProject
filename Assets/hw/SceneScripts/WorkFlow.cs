using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorkFlow :MonoBehaviour
{
        Define.SceneType sceneType = Define.SceneType.None;

    WorkFlow _instance;
    public WorkFlow Instance => _instance;
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
            ProgramWorkFlow();
        }

        public void ProgramWorkFlow()
        {
            switch (sceneType)
            {
                case Define.SceneType.None:
                    {
                        SceneManager.LoadScene("LoginScene");
                        sceneType++;
                    }
                    break;
                case Define.SceneType.Login:
                    {
                        if (LoginInformation.loggedIn && LoginInformation.profile != null)//로그인이 된상태 + 로그인이 돼서 프로파일이 생긴다면 로비로 입장
                        {
                            //Todo Create Class LoginInfomation.loggedin, LoginInfomation.profile 
                            if (PhotonManager.instance) //포톤 매니저 인스턴스가 생기면 로그인
                            {
                                SceneManager.LoadScene("LobbyScene");
                                sceneType++;
                            }
                        }
                    }
                    break;
                case Define.SceneType.Lobby:
                    //TODO: 로비에 있을때 마스터 유저가 Start버튼을 누르면 Game씬으로 전환 되게끔 작성
                    break;
                case Define.SceneType.InGame:
                    break;
                default:
                    break;
            }
        }
    }

