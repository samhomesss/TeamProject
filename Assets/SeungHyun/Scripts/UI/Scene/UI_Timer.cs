using Photon.Pun;
using System;
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
    static Action loadScene;
    bool isRoading = false;
    GameObject TimerText;

    UI_PlayerColorPercent _uiPlayerColorPercent;

    private void Start()
    {
        _uiPlayerColorPercent = UI_PlayerColorPercent.UIPlayerColorPercent.GetComponent<UI_PlayerColorPercent>();
        
        TimerText = Util.FindChild(gameObject, "TimerText", true);
        loadScene += SaveData;
        
    }

    // ����Ʈ�� �÷��̾��÷� �ۼ�Ʈ�� �ִ� �÷��̾�� �����ͼ� ���ĵ� �����͸� ������� �ݺ��� ������ ���� �ʳ�? �÷��̾� 1�� �ƴ϶� �÷��̾� ��� NickName�� �������� 
    // SetInt�� �ϳ� ���ؼ� ���� ���� ��ĥ�� ������ �Ѱ��ְ� ������ �°� ��ġ 
    private void SaveData() {
        //todo

        //���⼭ ���� �÷��̾� ���� ����. ����� �׽�Ʈ�� �ϵ��ڵ�
        PlayerPrefs.SetInt("PlayerNumber", 3); // ���� �÷��̾� ���� �־��ָ� ��

        //���⼭ �÷��̾� ������ �´� �÷��̾� �̸� �Է�. ����� �׽�Ʈ�� �ϵ��ڵ�
        PlayerPrefs.SetString("Rank1", "Player1");
        PlayerPrefs.SetString("Rank2", "Player2");
        PlayerPrefs.SetString("Rank3", "Player3");

        PlayerPrefs.SetInt("Rank1Percent", _uiPlayerColorPercent.Players[0].NodeCount); // ���ĵ� �÷��̾��� NodeCount�� �־��ְ� 
        PlayerPrefs.SetInt("Rank2Percent", 30); 
        PlayerPrefs.SetInt("Rank3Percent", 20);

        PhotonNetwork.LoadLevel("GameResultScene");
    }


    private void Update()
    {
        _timer -= Time.deltaTime;

        _second = _timer % 60;
        _minute = _timer / 60;
        
        if (_timer <= 0 && !isRoading)
        {
            _timer = 0;
            isRoading = true;
            loadScene?.Invoke();
        }
        if (_second < 10)
            TimerText.GetComponent<Text>().text = "0" + (int)_minute + ":0" + (int)_second;
        else
            TimerText.GetComponent<Text>().text = "0" + (int)_minute + ":" + (int)_second;
    }
}
