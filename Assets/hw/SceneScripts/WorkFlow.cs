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
                        if (LoginInformation.loggedIn && LoginInformation.profile != null)//�α����� �Ȼ��� + �α����� �ż� ���������� ����ٸ� �κ�� ����
                        {
                            //Todo Create Class LoginInfomation.loggedin, LoginInfomation.profile 
                            if (PhotonManager.instance) //���� �Ŵ��� �ν��Ͻ��� ����� �α���
                            {
                                SceneManager.LoadScene("LobbyScene");
                                sceneType++;
                            }
                        }
                    }
                    break;
                case Define.SceneType.Lobby:
                    //TODO: �κ� ������ ������ ������ Start��ư�� ������ Game������ ��ȯ �ǰԲ� �ۼ�
                    break;
                case Define.SceneType.InGame:
                    break;
                default:
                    break;
            }
        }
    }

