using DG.Tweening;
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
        Fade = GameObject.Find("UI_Fade").GetComponent<UI_Fade>();
    }

    public void LoadScene(Define.SceneType type) {
        var tween = Fade.SetFade(true);
        tween.OnComplete(() => DoNextScene(type));
    }

    private void DoNextScene(Define.SceneType type) {
        //todo
        //���� �� �̵����� �����ؾ���.
        SceneManager.LoadScene(type.ToString());
    }
}
