using Firebase.Firestore;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using yb;

public class AchievementManager : MonoBehaviour
{
    private Canvas _achievementPopup;
    private RectTransform _achievementPopupBG;
    private TMP_Text _achivementText;
    private Image _achivementImage;

    enum Achievement
    {
        ¾à°ñ,
        °­ÀÎÇÔ,
        ¹«Àû,
    }
    private int _deathCount = 0;




    private void Start()
    {
        _achievementPopup = GetComponent<Canvas>();
        _achievementPopupBG = _achievementPopup.transform.GetChild(0).GetComponent<RectTransform>();
        _achivementText = _achievementPopupBG.transform.Find("AchivementText").GetComponent<TMP_Text>();
        _achivementImage = _achievementPopupBG.transform.Find("TitleImage").GetComponent<Image>();
    }

    IEnumerator ShowAchievementPopup()
    {
        float duration = 1.0f;
        float elapsedTime = 0;
        Vector3 startPosition = _achievementPopupBG.transform.position;
        Vector3 endPosition = new Vector3(startPosition.x, 45f, startPosition.z); 

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            _achievementPopupBG.transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / duration);
            yield return null;
        }
        _achievementPopupBG.transform.position = endPosition;
        yield return new WaitForSeconds(2f);
        yield return StartCoroutine(CloseAchievementPopup());
    }

    IEnumerator CloseAchievementPopup()
    {
        float duration = 1.0f;
        float elapsedTime = 0;
        Vector3 startPosition = _achievementPopupBG.transform.position;
        Vector3 endPosition = new Vector3(startPosition.x, -230f, startPosition.z);

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            _achievementPopupBG.transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / duration);
            yield return null;
        }
        _achievementPopupBG.transform.position = endPosition;
    }


    public void countDead()
    {
        _deathCount++;
        Debug.Log($"{_deathCount}¸¸Å­ Á×¾ú½À´Ï´Ù");
        bool ischeckedAchivement = false;
        if (_deathCount >= 2)
        {
            foreach (var item in LoginInformation.achievement)
            {
                if (item == Achievement.¾à°ñ.ToString())
                {
                    ischeckedAchivement = true;
                }

            }
            if (!ischeckedAchivement)
            {
                _achivementText.text = "I am a Weakling";
                _achivementImage.sprite = Resources.Load<Sprite>("Prefabs/hw/Achievements/ClanIcon_Symbol");
                StartCoroutine(ShowAchievementPopup());

                DocumentReference doc = FirebaseFirestore.DefaultInstance.Collection("users").Document(LoginInformation.userkey);
                var update = new Dictionary<string, object> { { "Achievement", FieldValue.ArrayUnion(Achievement.¾à°ñ.ToString()) } };
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
