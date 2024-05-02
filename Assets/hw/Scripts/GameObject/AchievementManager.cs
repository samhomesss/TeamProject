using Firebase.Firestore;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using yb;

public class AchievementManager : MonoBehaviour
{
    private int _deathCount = 0;



    //public void countDead()
    //{
    //    string name = "���";
    //    _deathCount++;
    //    Debug.Log($"{_deathCount}��ŭ �׾����ϴ�");

    //   if(_deathCount >= 2)
    //    {
    //        DocumentReference doc = FirebaseFirestore.DefaultInstance.Collection("users").Document(LoginInformation.userkey);

    //            doc.UpdateAsync(new Dictionary<string, object>
    //            {
    //                  {"Achievement",name},
    //            },SetOptions.MergeAll);
    //    }
    //}



    public void countDead()
    {
        _deathCount++;
        Debug.Log($"{_deathCount}��ŭ �׾����ϴ�");

        if (_deathCount >= 2)
        {
            foreach (var item in LoginInformation.achievement)
            {
                if (item == "���")
                {
                    break;
                }
                else
                {
                    DocumentReference doc = FirebaseFirestore.DefaultInstance.Collection("users").Document(LoginInformation.userkey);
                    var update = new Dictionary<string, object>
        {
            { "Achievement", FieldValue.ArrayUnion("���") }
        };
                    doc.UpdateAsync(update).ContinueWith(task =>
                    {
                        if (task.IsFaulted)
                        {
                            Debug.LogError("Error updating document: " + task.Exception);
                        }
                        else
                        {
                            Debug.Log("Document successfully updated with new achievement.");
                        }
                    });
                }
            }
        }

    }

}
