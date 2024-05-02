using Firebase.Extensions;
using Firebase.Firestore;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public static class LoginInformation
{
    private static string s_id;
    private static List<string> _achievement = new List<string>();
    public static bool loggedIn => string.IsNullOrEmpty(s_id) == false;
    public static string userkey { get; private set; }
    public static ProfileDataModel profile { get; set; }
    public static string S_id { get => s_id; }

    public static List<string> achievement { get => _achievement; }

    public static async Task<bool> RefreshInformationAsync(string id)
    {
        LoadID(id, out DocumentReference docRef, out bool result);
        await docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            Dictionary<string, object> documentDictionary = task.Result.ToDictionary();
            if (documentDictionary != null)
            {
                profile = new ProfileDataModel();

                if(documentDictionary.ContainsKey("nickname") && documentDictionary.ContainsKey("Achievement"))
                {
                    List<object> list = (List<object>)documentDictionary["Achievement"];
                    foreach(var item in list)
                    {
                        _achievement.Add(item.ToString());
                    }
                    string nickname = (string)documentDictionary["nickname"]; // 딕셔너리에서 닉네임 설정
                    profile.achievements = _achievement;
                    result = true; // 닉네임이 있으면 true
                }
            }
        });
        s_id = id;
        return result; // 모든 정보를 성공적으로 가져왔는지 여부 반환
    }

    public static void LoadID(string id, out DocumentReference docRef, out bool result)
    {
        result = false;

        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        userkey = id.Replace("@", "").Replace(".", "");//매개변수로 가져온 이메일주소에 @. 문자를뺀 값을 유저키에 저장
        docRef = db.Collection("users").Document(userkey);//Firestore의 "users" 컬렉션에서 userkey에 해당하는 문서에 대한 참조를 가져옵니다.
    }
}
