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
                    string nickname = (string)documentDictionary["nickname"]; // ��ųʸ����� �г��� ����
                    profile.achievements = _achievement;
                    result = true; // �г����� ������ true
                }
            }
        });
        s_id = id;
        return result; // ��� ������ ���������� �����Դ��� ���� ��ȯ
    }

    public static void LoadID(string id, out DocumentReference docRef, out bool result)
    {
        result = false;

        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        userkey = id.Replace("@", "").Replace(".", "");//�Ű������� ������ �̸����ּҿ� @. ���ڸ��� ���� ����Ű�� ����
        docRef = db.Collection("users").Document(userkey);//Firestore�� "users" �÷��ǿ��� userkey�� �ش��ϴ� ������ ���� ������ �����ɴϴ�.
    }
}
