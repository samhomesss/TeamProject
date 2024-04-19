using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_GameResult : UI_Scene
{
    enum GameObjects
    {
        Player1ResultInfo, 
        Player2ResultInfo, 
        Player3ResultInfo, 
        Player4ResultInfo, 
        Player5ResultInfo, 
        Player6ResultInfo, 
        Player7ResultInfo, 
        Player8ResultInfo,
    }

    List<GameObject> playerResultInfos = new List<GameObject>();
    Dictionary<string, int> _playerList;    
    int playerCount; 
    private void Start()
    {
        playerCount = PlayerPrefs.GetInt("PlayerNumber");
        _playerList = new Dictionary<string, int>(playerCount) {
            { PlayerPrefs.GetString("Rank1"),PlayerPrefs.GetInt("Rank1Percent") },
            { PlayerPrefs.GetString("Rank2"),PlayerPrefs.GetInt("Rank2Percent") },
            { PlayerPrefs.GetString("Rank3"),PlayerPrefs.GetInt("Rank3Percent") },
        };
        Init();
    }

    public override void Init()
    {
        base.Init();
        Bind<GameObject>(typeof(GameObjects));
        //전체 ResultInfo 읽어오고 일단 보이지 않게 설정 한다
        for (int i = 0; i < 8; i++)
        {
            playerResultInfos.Add(Get<GameObject>((int)((GameObjects)i)));
            playerResultInfos[i].SetActive(false);
        }
        // 현재 플레이어의 수 만큼만 띄워줌
        for (int i = 0; i < playerCount; i++)
        {
            PlayerResultInfo resultInfo = playerResultInfos[i].GetComponent<PlayerResultInfo>();
            playerResultInfos[i].SetActive(true);
           
            
            if (i + 1 == 1)
            {
                resultInfo.PlayerNickName.text = "Player1";
                resultInfo.PlayerResultImage.sprite = Managers.Resources.Load<Sprite>(($"Prefabs/sh/UI/Texture/Player1"));
            }
            else if (i + 1 == 2)
            {
                resultInfo.PlayerNickName.text = "Player2";
                resultInfo.PlayerResultImage.sprite = Managers.Resources.Load<Sprite>(($"Prefabs/sh/UI/Texture/Player2"));
            }
            else if (i + 1 == 3)
            {
                resultInfo.PlayerNickName.text = "Player3";
                resultInfo.PlayerResultImage.sprite = Managers.Resources.Load<Sprite>(($"Prefabs/sh/UI/Texture/Player3"));
            }
            else
            {
                resultInfo.PlayerNickName.text = $"Player{i + 1}";
                resultInfo.PlayerResultImage.gameObject.SetActive(false);
                resultInfo.PlayerResultNumber.gameObject.SetActive(true);
                resultInfo.PlayerResultNumber.text = $"{i + 1}.";
            }
        }
    
        // 플레이어 Dictionary 이용해서 처리 
    }
}

