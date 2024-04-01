using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginUI : MonoBehaviour
{
    [SerializeField]private Button _loginButton;
    [SerializeField] private TMP_InputField _idInputField;
    [SerializeField] private TMP_InputField _pwInputField;


    [SerializeField] private Button _registerButton;
    [SerializeField] private Canvas _registerCanvas;


    [SerializeField] private Canvas _registerNickname;


    //Todo: �׽�Ʈ ������ [SerializeField] ����

    private async void Start()
    {
        var dependencyState = await FirebaseApp.CheckAndFixDependenciesAsync(); //����Ǿ��ִ� �������� Ȯ��.

        _loginButton = transform.Find("LoginPanel/Button - TryLogin").GetComponent<Button>();
        _idInputField = transform.Find("LoginPanel/ID Panel/InputField (TMP)").GetComponent<TMP_InputField>();
        _pwInputField = transform.Find("LoginPanel/PW Panel/InputField (TMP)").GetComponent <TMP_InputField>();
        _registerButton = transform.Find("LoginPanel/Button(Register)").GetComponent<Button>();
        _registerCanvas = GameObject.Find("Canvas - Register").GetComponent<Canvas>();
        _registerNickname = GameObject.Find("Canvas - NicknameSettingWindow").GetComponent<Canvas>();

        //Todo: �׽�Ʈ ������ Debug �����
        Debug.Log($"���̾�̽� ���� ����{dependencyState}");

        _registerButton.onClick.AddListener(() =>
        {
            _registerCanvas.enabled = true;
        });


        #region �α��ι�ư �̺�Ʈ
        _loginButton.onClick.AddListener(() =>
        {
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
                else if(task.IsFaulted)
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
