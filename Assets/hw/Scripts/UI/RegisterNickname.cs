using Firebase.Firestore;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Extensions;
using UnityEngine.Profiling;


public class RegisterNickname : UI_Scene
{
    enum GameObjects
    {
        Nickname_InputField,
        Confirm_Button,
    }
    private TMP_InputField _nickname_InputField;
    private Button _confirm_Button;

    public override void Init()
    {
        base.Init();
        Bind<GameObject>(typeof(GameObjects));
        GameObject nickname_InputField = Get<GameObject>((int)GameObjects.Nickname_InputField);
        GameObject Confirm_Button = GetObject((int)GameObjects.Confirm_Button);


        _nickname_InputField = nickname_InputField.GetComponent<TMP_InputField>();
        _confirm_Button = Confirm_Button.GetComponent<Button>();

    }

    protected void Start()
    {
        Init();
        _confirm_Button.onClick.AddListener(() =>
        {
            CollectionReference collectionReference = FirebaseFirestore.DefaultInstance.Collection("users");//users의 레퍼런스를 받아옴
            collectionReference.GetSnapshotAsync().ContinueWithOnMainThread(task =>
            {
                foreach (DocumentSnapshot documentSnapshot in task.Result.Documents)
                {
                    if (documentSnapshot.GetValue<string>("nickname") == _nickname_InputField.text)
                    {
                        //UIManager.instance.Get<UIWarningWindow>().Show($"{_nickname.text} is already in use.");

                        Debug.Log("이미 닉네임을 사용중 입니다");
                        return;
                    }

                }
                DocumentReference doc = FirebaseFirestore.DefaultInstance.Collection("users").Document(LoginInformation.userkey);

                doc.SetAsync(new Dictionary<string, object>
                {
                       {"nickname",_nickname_InputField.text},
                }).ContinueWithOnMainThread(task =>
                {
                    LoginInformation.profile = new ProfileDataModel
                    {
                        nickname = _nickname_InputField.text,
                    };
                doc.SetAsync(new Dictionary<string, object>
                {
                   {"Achievement",LoginInformation.profile.achievements},
                }, SetOptions.MergeAll);
                });
            });

        });
        _confirm_Button.interactable = false;
        _nickname_InputField.onValueChanged.AddListener(value => _confirm_Button.interactable = IsFormatValid());
    }


    private bool IsFormatValid()
    {
        return (_nickname_InputField.text.Length >= 2) && (_nickname_InputField.text.Length <= 10);//bool 타입으로 반환되기에. 버튼의 interactable의 조건이 맞으면 활성화 되도록
    }

}
