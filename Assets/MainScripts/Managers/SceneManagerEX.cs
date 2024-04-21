using DG.Tweening;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEX
{
    public BaseScene CurrentScene => GameObject.FindFirstObjectByType(typeof(BaseScene)).GetComponent<BaseScene>();
    public bool isContinue { get; set; } = false;

    public UI_Fade Fade { get; private set; }

    public void Init() {
        if(Fade == null)
            Fade = GameObject.Find("UI_Fade").GetComponent<UI_Fade>();
    }

    public void LoadScene(Define.SceneType type) {
        var tween = Fade.SetFade(true);
        if (type == Define.SceneType.Quit) {
            tween.OnComplete(() => QuitGame());
        } else {
            tween.OnComplete(() => DoNextScene(type));
        }
    }
    private void DoNextScene(Define.SceneType type) {
        PhotonNetwork.LoadLevel((int)type);
    }

    private void QuitGame() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); 
#endif
    }
}
