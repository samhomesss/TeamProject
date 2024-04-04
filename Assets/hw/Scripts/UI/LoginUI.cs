using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using Photon.Pun.Demo.SlotRacer.Utils;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Hw
{
    public class LoginUI : UI_Scene
    {
        enum GameObjects
        {
            LoginButton,
            IdInputField,
            PwInputField,
            RegisterButton,
        }
        private Button _loginButton;
        private TMP_InputField _idInputField;
        private TMP_InputField _pwInputField;

        private Button _registerButton;

        private Canvas _registerID;
        private Canvas _registerNickname;

        [SerializeField] private Toggle testLoginToggle;

        public override void Init()
        {
            base.Init();

            Bind<GameObject>(typeof(GameObjects));//���ε忡 ��� ���ӿ�����Ʈ�� �־��

            GameObject loginButton = GetObject((int)GameObjects.LoginButton);
            GameObject idInputField = GetObject((int)GameObjects.IdInputField);
            GameObject pwInputField = GetObject((int)GameObjects.PwInputField);
            GameObject registerButton = GetObject((int)GameObjects.RegisterButton);

            _loginButton = loginButton.GetComponent<Button>();
            _idInputField = idInputField.GetComponent<TMP_InputField>();
            _pwInputField = pwInputField.GetComponent<TMP_InputField>();
            _registerButton = registerButton.GetComponent<Button>();


            _registerID = Util.FindChild(transform.parent.gameObject, "RegisterID", false).GetComponent<Canvas>();
            _registerNickname = Util.FindChild(transform.parent.gameObject, "RegisterNickname", false).GetComponent<Canvas>();

        }

        private void Awake()
        {
            var lines = File.ReadAllLines("Assets/TestOption.txt");
            if (lines.Length > 0)
            {
                testLoginToggle.gameObject.SetActive(lines[0].Equals("1"));
            }
        }

        private async void Start()
        {

            Init();
            var dependencyState = await FirebaseApp.CheckAndFixDependenciesAsync(); //����Ǿ��ִ� �������� Ȯ��. 

            //Todo: �׽�Ʈ ������ Debug �����
            Debug.Log($"���̾�̽� ���� ����{dependencyState}");

            _registerButton.onClick.AddListener(() =>
            {
                _registerID.enabled = true;
            });


            #region �α��ι�ư �̺�Ʈ
            _loginButton.onClick.AddListener(() =>
            {
                if (testLoginToggle.IsActive() && testLoginToggle.isOn)
                {
                    _idInputField.text = "hiwoong12@naver.com";
                    _pwInputField.text = "123123";
                }

                if (string.IsNullOrEmpty(_idInputField.text) && string.IsNullOrEmpty(_pwInputField.text))
                    return;

                //  TryLogin(_idInputField, _pwInputField);

                FirebaseAuth auth = FirebaseAuth.DefaultInstance;
                auth.SignInWithEmailAndPasswordAsync(_idInputField.text, _pwInputField.text).ContinueWithOnMainThread(async task =>
                {
                    if (task.IsCanceled)
                    {
                        Debug.LogError("�α��� ���� �½�Ʈ���");
                    }
                    else if (task.IsFaulted)
                    {
                        Debug.LogError("�α��� ����,���̵� ��й�ȣ Ȯ��");
                    }
                    else
                    {
                        Debug.Log("ID PW is correct");
                        //Debug.Log($"���� ���� ������Ʈ{GameManager.instance.state}, �α��λ���{LoginInformation.loggedIn} �α�����������{LoginInformation.profile}");
                        bool result = await LoginInformation.RefreshInformationAsync(_idInputField.text);

                        if (result == false)
                        {
                            //todo -> create nickname setting ui
                            _registerNickname.enabled = true;
                            Debug.Log("�г����� �����ؾ� �մϴ�.");
                        }
                    }

                });
            });
            #endregion
        }
    }

}
