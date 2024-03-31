using Firebase.Firestore;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Extensions;

namespace DiceGame.UI
{
    public class UINicknameSetting : MonoBehaviour
    {
        private TMP_InputField _nickname;
        private Button _confirm;

        protected void Awake()
        {

            _nickname = transform.Find("NicknamePanel/Panel - nickname/InputField (TMP)").GetComponent<TMP_InputField>();
            _confirm = transform.Find("NicknamePanel/Button - Confirm").GetComponent <Button>();

            _confirm.onClick.AddListener(() =>
            {
                CollectionReference collectionReference = FirebaseFirestore.DefaultInstance.Collection("users");//users의 레퍼런스를 받아옴
                collectionReference.GetSnapshotAsync().ContinueWithOnMainThread(task =>
                {
                    foreach(DocumentSnapshot documentSnapshot in task.Result.Documents)
                    {
                        if(documentSnapshot.GetValue<string>("nickname") == _nickname.text)
                        {
                            //UIManager.instance.Get<UIWarningWindow>().Show($"{_nickname.text} is already in use.");

                            Debug.Log("이미 닉네임을 사용중 입니다");
                            return;
                        }

                    }
                    FirebaseFirestore.DefaultInstance.Collection("users").Document(LoginInformation.userkey).SetAsync(new Dictionary<string, object>
                    {
                        {"nickname",_nickname.text}
                    }).ContinueWithOnMainThread(task =>
                    {
                        LoginInformation.profile = new ProfileDataModel
                        {
                            nickname = _nickname.text,
                        };
                    });

                });

            });


            _confirm.interactable = false;
            _nickname.onValueChanged.AddListener(value => _confirm.interactable = IsFormatValid());
        }


        private bool IsFormatValid()
        {
            return (_nickname.text.Length >= 2) && (_nickname.text.Length <= 10);//bool 타입으로 반환되기에. 버튼의 interactable의 조건이 맞으면 활성화 되도록
        }

    }
}


