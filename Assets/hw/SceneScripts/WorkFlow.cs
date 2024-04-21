using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �ش� Ŭ������ 0407 Managers���� GameManager�� ��ü��, ���� �ش� ��ũ��Ʈ�� �ʿ�ø� ����� ������ X
/// </summary>
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
                        Managers.Scene.LoadScene(Define.SceneType.LoginScene);
                        //SceneManager.LoadScene("LoginScene");
                        sceneType++;
                    }
                    break;
                case Define.SceneType.LoginScene:
                    {
                        if (LoginInformation.loggedIn && LoginInformation.profile != null)//�α����� �Ȼ��� + �α����� �ż� ���������� ����ٸ� �κ�� ����
                        {
                            //Todo Create Class LoginInfomation.loggedin, LoginInfomation.profile 
                            if (PhotonManager.instance) //���� �Ŵ��� �ν��Ͻ��� ����� �α���
                            {
                                Managers.Scene.LoadScene(Define.SceneType.LobbyScene);
                                //SceneManager.LoadScene("LobbyScene");
                                sceneType++;
                            }
                        }
                    }
                    break;
                case Define.SceneType.LobbyScene:
                    //TODO: �κ� ������ ������ ������ Start��ư�� ������ Game������ ��ȯ �ǰԲ� �ۼ�
                    break;
                case Define.SceneType.GamePlay:
                    break;
                default:
                    break;
            }
        }
    }

