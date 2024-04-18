using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Timer : UI_Scene
{
    public static UI_Timer instance;

    public float Timer => _timer;

    float _timer = 300f;
    float _minute; 
    float _second; 

    GameObject TimerText;

    private void Start()
    {
        TimerText = Util.FindChild(gameObject, "TimerText", true);
    }

    private void Update()
    {
        _timer -= Time.deltaTime;

        _second = _timer % 60;
        _minute = _timer / 60;

        if (_timer <= 0)
        {
            _timer = 0;

            PhotonNetwork.LoadLevel("GamePlay");
        }
        if (_second < 10)
            TimerText.GetComponent<Text>().text = "0" + (int)_minute + ":0" + (int)_second;
        else
            TimerText.GetComponent<Text>().text = "0" + (int)_minute + ":" + (int)_second;

        

    }
}
