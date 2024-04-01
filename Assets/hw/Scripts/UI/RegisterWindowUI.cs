using Firebase.Auth;
using Firebase.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RegisterWindowUI :MonoBehaviour
{

    [SerializeField]private TMP_InputField _idInputField;
    [SerializeField] private TMP_InputField _pwInputField;

    [SerializeField] private Button _confirmButton;
    [SerializeField] private Button _cancelButton;

    //Todo: 테스트완료하면 SerializeField 없앨것

    private void Awake()
    {
       _idInputField = transform.Find("Panel/Panel - ID/InputField (TMP)").GetComponent<TMP_InputField>();
       _pwInputField = transform.Find("Panel/Panel - PW/InputField (TMP)").GetComponent <TMP_InputField>();

        _confirmButton = transform.Find("Panel/Button - ok").GetComponent<Button>();
        _confirmButton.interactable = false;
        _cancelButton = transform.Find("Panel/Button - Cancel").GetComponent<Button>();


        _confirmButton.onClick.AddListener(() =>
        {
            FirebaseAuth.DefaultInstance.CreateUserWithEmailAndPasswordAsync(_idInputField.text, _pwInputField.text).
                ContinueWithOnMainThread(task =>
                {
                    if(task.IsCanceled)
                    {
                        Debug.LogError($"[RegisterWindowUI] : Canceled register{_idInputField.text}");
                    }

                    if (task.IsFaulted)
                    {
                        if (task.Exception.Message.Contains("the email address is already in use by another account"))
                        {
                            Debug.Log($"Failed to register.. {_idInputField.text} is alreayUsed");
                            //Todo: 이미 쓰고있는 이메일이 있다는 팝업창 보여줌
                        }

                        else
                        {
                            Debug.LogError($"[UIRegisterWindow] : Faulted register{_idInputField.text},{task.Exception}");
                        }
                        return;
                    }

                    AuthResult result = task.Result;

                    Debug.Log($"{_idInputField.text}생성이 완료 되었습니다 ");
                    //Todo: 생성완료 팝업창 제작

                });
        });

        _cancelButton.onClick.AddListener(() =>
        {
            this.GetComponent<Canvas>().enabled = false;
            _idInputField.text = string.Empty;
            _pwInputField.text = string.Empty;
        });

        _idInputField.onValueChanged.AddListener(value => _confirmButton.interactable = IsFormatValid());
        _pwInputField.onValueChanged.AddListener(value => _confirmButton.interactable = IsFormatValid());
    }

    private bool IsFormatValid()
    {
        string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+.[a-zA-Z]{2,}$";
        return System.Text.RegularExpressions.Regex.IsMatch(_idInputField.text, emailPattern) && _pwInputField.text.Length >= 6;
    }

}
