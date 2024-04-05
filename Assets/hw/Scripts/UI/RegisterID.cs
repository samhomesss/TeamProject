using Firebase.Auth;
using Firebase.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

    public class RegisterID : UI_Scene
    {

        enum GameObjects
        {
            Id_inputField,
            Pw_inputField,
            ConfirmButton,
            CancelButton
        }

        // ���� �ϰ� 
        private TMP_InputField _idInputField;
        private TMP_InputField _pwInputField;

        private Button _confirmButton;
        private Button _cancelButton;

        public override void Init()
        {
            Bind<GameObject>(typeof(GameObjects));

            GameObject id_inputfield = GetObject((int)GameObjects.Id_inputField);
            GameObject pw_inputfield = GetObject((int)GameObjects.Pw_inputField);
            GameObject confirmButton = GetObject((int)GameObjects.ConfirmButton);
            GameObject cancelButton = GetObject((int)GameObjects.CancelButton);

            _idInputField = id_inputfield.GetComponent<TMP_InputField>();
            _pwInputField = pw_inputfield.GetComponent<TMP_InputField>();
            _confirmButton = confirmButton.GetComponent<Button>();
            _cancelButton = cancelButton.GetComponent<Button>();

            _confirmButton.interactable = false;
        }



        private void Start()
        {
            Init();


            _confirmButton.onClick.AddListener(() =>
            {
                FirebaseAuth.DefaultInstance.CreateUserWithEmailAndPasswordAsync(_idInputField.text, _pwInputField.text).
                    ContinueWithOnMainThread(task =>
                    {
                        if (task.IsCanceled)
                        {
                            Debug.LogError($"[RegisterWindowUI] : Canceled register{_idInputField.text}");
                        }

                        if (task.IsFaulted)
                        {
                            if (task.Exception.Message.Contains("the email address is already in use by another account"))
                            {
                                Debug.Log($"Failed to register.. {_idInputField.text} is alreayUsed");
                                //Todo: �̹� �����ִ� �̸����� �ִٴ� �˾�â ������
                            }

                            else
                            {
                                Debug.LogError($"[UIRegisterWindow] : Faulted register{_idInputField.text},{task.Exception}");
                            }
                            return;
                        }

                        AuthResult result = task.Result;

                        Debug.Log($"{_idInputField.text}������ �Ϸ� �Ǿ����ϴ� ");
                        //Todo: �����Ϸ� �˾�â ����

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
