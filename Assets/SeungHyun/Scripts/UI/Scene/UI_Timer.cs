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
    IEnumerator Wait_UI_PlayerColorPercentLoaded()//0420 이희웅 추가 이작업이 안돼면 UI_PlayerCOlorPercent가 null 일때 참조하는 문제가 생겨, 생길때 까지 기다리고 가져오도록 수정
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

        yield return StartCoroutine(Update_timer());//위의 로드가 다 끝난 다음에 타이머 코루틴이 돌아가도록 설정
    }
    // 리스트를 플레이어컬러 퍼센트에 있는 플레이어스를 가져와서 정렬된 데이터를 기반으로 반복문 돌리면 되지 않나? 플레이어 1이 아니라 플레이어 대신 NickName을 가져오고 
    // SetInt를 하나 더해서 현재 맵을 색칠한 갯수를 넘겨주고 순위에 맞게 배치 
    private void SaveData()
    {
        //todo
        List<PlayerController> playerList = _uiPlayerColorPercent.SortNodeCount();

        //여기서 포톤 플레이어 수를 대입. 현재는 테스트용 하드코딩
        PlayerPrefs.SetInt("PlayerNumber", playerList.Count); // 포톤 플레이어 수를 넣어주면 됨w



        //여기서 플레이어 순위에 맞는 플레이어 이름 입력. 현재는 테스트용 하드코딩
        for (int i = 0; i < playerList.Count; i++)
        {

            PlayerPrefs.SetString($"Rank{i + 1}", playerList[i].PlayerNickName);//방안에 연결된 닉네임만 뽑아줌
            PlayerPrefs.SetInt($"Rank{i + 1}Percent", playerList[i].NodeCount); // 정렬된 플레이어의 NodeCount를 넣어주고 
        }

        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("GameResultScene");
        }
    }


    private IEnumerator Update_timer() //0420 이희웅 업데이트에서 코루틴으로 수정 동기를 맞추기 위해서
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
