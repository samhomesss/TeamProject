using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hw
{
public class WorkFlow
{
        Define.Scene sceneType = Define.Scene.Unknown;
        public void Update()
        {
            ProgramWorkFlow();
        }

        public void ProgramWorkFlow()
        {
            switch (sceneType)
            {
                case Define.Scene.Unknown:
                    {
                        SceneManager.LoadScene("LoginScene");
                        sceneType++;
                    }
                    break;
                case Define.Scene.Login:
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
                case Define.Scene.JoinedRoom:
                    //TODO: �κ� ������ ������ ������ Start��ư�� ������ Game������ ��ȯ �ǰԲ� �ۼ�
                    break;
                case Define.Scene.Game:
                    break;
                default:
                    break;
            }
        }
    }


}