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
        userkey = id.Replace("@", "").Replace(".", "");//�Ű������� ������ �̸����ּҿ� @. ���ڸ��� ���� ����Ű�� ����
        DocumentReference docRef = db.Collection("users").Document(userkey);//Firestore�� "users" �÷��ǿ��� userkey�� �ش��ϴ� ������ ���� ������ �����ɴϴ�.

        await docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            Dictionary<string, object> documentDictionary = task.Result.ToDictionary();

            if(documentDictionary != null)
            {
                profile = new ProfileDataModel
                {
                    nickname = (string)documentDictionary["nickname"], //��ųʸ��� �ִ� �г��� ����

                };
                result = true; //�г����� ���̾�̽��κ��� ������ �� ������ true;
            }
        });
        s_id = id;
        return result;//�г����� ���̾�̽��κ��� ������ �� ������ false;
    }
}
