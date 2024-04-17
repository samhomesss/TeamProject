using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Timer : UI_Scene
{
    float Timer = 300;
    float minute; 
    float second; 

    GameObject TimerText;

    private void Start()
    {
        TimerText = Util.FindChild(gameObject, "TimerText", true);
    }

    private void Update()
    {
        Timer -= Time.deltaTime;

        second = Timer % 60;
        minute = Timer / 60;

        if (Timer <= 0)
        {
            Timer = 0;
        }
        if (second < 10)
            TimerText.GetComponent<Text>().text = "0" + (int)minute + ":0" + (int)second;
        else
            TimerText.GetComponent<Text>().text = "0" + (int)minute + ":" + (int)second;
    }
}
