using Firebase.Extensions;
using Firebase.Firestore;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public static class LoginInformation
{
    private static string s_id;
    public static bool loggedIn => string.IsNullOrEmpty(s_id) == false;
    public static string userkey {  get; private set; }
    public static ProfileDataModel profile { get; set; }



    public static async Task<bool> RefreshInformationAsync(string id)
    {
        bool result = false;

        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        userkey = id.Replace("@", "").Replace(".", "");//매개변수로 가져온 이메일주소에 @. 문자를뺀 값을 유저키에 저장
        DocumentReference docRef = db.Collection("users").Document(userkey);//Firestore의 "users" 컬렉션에서 userkey에 해당하는 문서에 대한 참조를 가져옵니다.

        await docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            Dictionary<string, object> documentDictionary = task.Result.ToDictionary();

            if(documentDictionary != null)
            {
                profile = new ProfileDataModel
                {
                    nickname = (string)documentDictionary["nickname"], //딕셔너리에 있는 닉네임 설정

                };
                result = true; //닉네임을 파이어베이스로부터 가져올 수 있으면 true;
            }
        });
        s_id = id;
        return result;//닉네임을 파이어베이스로부터 가져올 수 없으면 false;
    }
}
