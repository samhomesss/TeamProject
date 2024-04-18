using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
    int playerCount = MapColorData.MapDataPlayer.Count; // 일단 임시로 플레이어의 수를 가져옴
    private void Start()
    {
        foreach (var item in MapColorData.MapDataPlayer)
        {
            Debug.Log(item.name);
        }
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

        if (MapColorData.MapDataPlayer == null)
            return;

        for (int i = 0; i < MapColorData.MapDataPlayer.Count; i++)
        {
            PlayerResultInfo resultInfo = playerResultInfos[i].GetComponent<PlayerResultInfo>();
            if (MapColorData.MapDataPlayer.Count == 1)
            {
                resultInfo.PlayerNickName.text = MapColorData.MapDataPlayer[i].name;
                resultInfo.PlayerResultImage.sprite = Managers.Resources.Load<Sprite>(($"Prefabs/sh/UI/Texture/Player1"));
                resultInfo.PlayerColorPercent.value = MapColorData.MapDataPlayer[i].NodeCount * 10;
                resultInfo.PlayerColorPercent.gameObject.transform.GetChild(2).GetComponent<TMP_Text>().text = ((float)(MapColorData.MapDataPlayer[i].NodeCount) / 4096 * 100).ToString() + "%";
            }
            
            for (int j = i + 1; j < MapColorData.MapDataPlayer.Count; j++)
            {
                if (MapColorData.MapDataPlayer[i].NodeCount < MapColorData.MapDataPlayer[j].NodeCount)
                {
                    int temp = MapColorData.MapDataPlayer[i].NodeCount;
                    MapColorData.MapDataPlayer[i].NodeCount = MapColorData.MapDataPlayer[j].NodeCount;
                    MapColorData.MapDataPlayer[j].NodeCount = temp;
                }
            }
            
        }

        
    }
}
