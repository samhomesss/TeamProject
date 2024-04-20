using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using yb;

public class UI_Timer : UI_Scene
{
    public static UI_Timer instance;

    public float Timer => _timer;

    float _timer = 15f;
    float _minute;
    float _second;
    static Action loadScene;
    bool isRoading = false;
    GameObject TimerText;

    UI_PlayerColorPercent _uiPlayerColorPercent;

    private void Start()
    {
        StartCoroutine(Wait_UI_PlayerColorPercentLoaded());

    }
    IEnumerator Wait_UI_PlayerColorPercentLoaded()//0420 ����� �߰� ���۾��� �ȵŸ� UI_PlayerCOlorPercent�� null �϶� �����ϴ� ������ ����, ���涧 ���� ��ٸ��� ���������� ����
    {
        bool _playerColorPercentLoaded = false;
        while (!_playerColorPercentLoaded)
        {
            _playerColorPercentLoaded = UI_PlayerColorPercent.UIPlayerColorPercent;
            yield return new WaitForSeconds(0.1f);
        }
        _uiPlayerColorPercent = UI_PlayerColorPercent.UIPlayerColorPercent.GetComponent<UI_PlayerColorPercent>();

        TimerText = Util.FindChild(gameObject, "TimerText", true);
        loadScene += SaveData;

        yield return StartCoroutine(Update_timer());//���� �ε尡 �� ���� ������ Ÿ�̸� �ڷ�ƾ�� ���ư����� ����
    }
    // ����Ʈ�� �÷��̾��÷� �ۼ�Ʈ�� �ִ� �÷��̾�� �����ͼ� ���ĵ� �����͸� ������� �ݺ��� ������ ���� �ʳ�? �÷��̾� 1�� �ƴ϶� �÷��̾� ��� NickName�� �������� 
    // SetInt�� �ϳ� ���ؼ� ���� ���� ��ĥ�� ������ �Ѱ��ְ� ������ �°� ��ġ 
    private void SaveData()
    {
        //todo
        List<PlayerController> playerList = _uiPlayerColorPercent.SortNodeCount();

        //���⼭ ���� �÷��̾� ���� ����. ����� �׽�Ʈ�� �ϵ��ڵ�
        PlayerPrefs.SetInt("PlayerNumber", playerList.Count); // ���� �÷��̾� ���� �־��ָ� ��w



        //���⼭ �÷��̾� ������ �´� �÷��̾� �̸� �Է�. ����� �׽�Ʈ�� �ϵ��ڵ�
        for (int i = 0; i < playerList.Count; i++)
        {

            PlayerPrefs.SetString($"Rank{i + 1}", playerList[i].PlayerNickName);//��ȿ� ����� �г��Ӹ� �̾���
            PlayerPrefs.SetInt($"Rank{i + 1}Percent", playerList[i].NodeCount); // ���ĵ� �÷��̾��� NodeCount�� �־��ְ� 
        }

        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("GameResultScene");
        }
    }


    private IEnumerator Update_timer() //0420 ����� ������Ʈ���� �ڷ�ƾ���� ���� ���⸦ ���߱� ���ؼ�
    {
        while (true)
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

            yield return null;
        }
    }
}
