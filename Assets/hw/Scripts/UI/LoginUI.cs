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


    //Todo: 테스트 끝나면 [SerializeField] 삭제

    private async void Start()
    {
        var dependencyState = await FirebaseApp.CheckAndFixDependenciesAsync(); //연결되어있는 상태인지 확인.

        _loginButton = transform.Find("LoginPanel/Button - TryLogin").GetComponent<Button>();
        _idInputField = transform.Find("LoginPanel/ID Panel/InputField (TMP)").GetComponent<TMP_InputField>();
        _pwInputField = transform.Find("LoginPanel/PW Panel/InputField (TMP)").GetComponent <TMP_InputField>();
        _registerButton = transform.Find("LoginPanel/Button(Register)").GetComponent<Button>();
        _registerCanvas = GameObject.Find("Canvas - Register").GetComponent<Canvas>();
        _registerNickname = GameObject.Find("Canvas - NicknameSettingWindow").GetComponent<Canvas>();

        //Todo: 테스트 끝나면 Debug 지울것
        Debug.Log($"파이어베이스 현재 상태{dependencyState}");

        _registerButton.onClick.AddListener(() =>
        {
            _registerCanvas.enabled = true;
        });


        #region 로그인버튼 이벤트
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
                    Debug.LogError("로그인 실패 태스트취소");
                }
                else if(task.IsFaulted)
                {
                    Debug.LogError("로그인 실패,아이디 비밀번호 확인");
                }
                else
                {
                    Debug.Log("ID PW is correct");
                    //Debug.Log($"현재 게임 스테이트{GameManager.instance.state}, 로그인상태{LoginInformation.loggedIn} 로그인프로파일{LoginInformation.profile}");
                    bool result = await LoginInformation.RefreshInformationAsync(_idInputField.text);

                    if (result == false)
                    {
                        //todo -> create nickname setting ui
                        _registerNickname.enabled = true;
                        Debug.Log("닉네임을 설정해야 합니다.");
                    }
                }

            });
        });
        #endregion
    }
}
