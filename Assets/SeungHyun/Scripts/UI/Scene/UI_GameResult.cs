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
    int playerCount = MapColorData.MapDataPlayer.Count; // 일단 임시로 플레이어의 수를 가져옴
    int[] playerColorCount = new int[8];
    string Player1;
    string Player2;
    private void Start()
    {
       
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
            PlayerResultInfo secondResultInfo = playerResultInfos[i+1].GetComponent<PlayerResultInfo>();
            if (MapColorData.MapDataPlayer.Count == 1)
            {
                resultInfo.PlayerNickName.text = PhotonNetwork.PlayerList[i].NickName;
                resultInfo.PlayerColorPercent.value = MapColorData.MapPlayerCountData[i] * 10;
                resultInfo.PlayerColorPercent.gameObject.transform.GetChild(2).GetComponent<TMP_Text>().text = ((int)((float)(MapColorData.MapPlayerCountData[i]) / 4096 * 100)).ToString() + "%";
            }
            else if (MapColorData.MapDataPlayer.Count == 2)
            {

                if (MapColorData.MapPlayerCountData[i] < MapColorData.MapPlayerCountData[i+1])
                {
                    Debug.Log(MapColorData.MapPlayerCountData[i] + "mapData 1번");
                    Debug.Log(MapColorData.MapPlayerCountData[i+1] + "mapData 2번");

                    int temp = MapColorData.MapPlayerCountData[i];
                    MapColorData.MapPlayerCountData[i] = MapColorData.MapPlayerCountData[1];
                    MapColorData.MapPlayerCountData[i+1] = temp;

                    Debug.Log(MapColorData.MapPlayerCountData[i] + "mapData 1번");
                    Debug.Log(MapColorData.MapPlayerCountData[i+1] + "mapData 2번");

                    Player1 = PhotonNetwork.PlayerList[i].NickName;
                    Player2 = PhotonNetwork.PlayerList[i+1].NickName;

                    string strtemp = Player1;
                    Player1 = Player2;
                    Player2 = strtemp;


                    playerResultInfos[i].GetComponent<PlayerResultInfo>().PlayerNickName.text = Player1;
                    playerResultInfos[i+1].GetComponent<PlayerResultInfo>().PlayerNickName.text = Player2;
                    playerResultInfos[i].GetComponent<PlayerResultInfo>().PlayerColorPercent.value = MapColorData.MapPlayerCountData[i] * 10;
                    playerResultInfos[i+1].GetComponent<PlayerResultInfo>().PlayerColorPercent.value = MapColorData.MapPlayerCountData[i+1] * 10;
                    playerResultInfos[i].GetComponent<PlayerResultInfo>().PlayerColorPercent.gameObject.transform.GetChild(2).GetComponent<TMP_Text>().text = ((int)((float)(MapColorData.MapPlayerCountData[i]) / 4096 * 100)).ToString() + "%";
                    playerResultInfos[i+1].GetComponent<PlayerResultInfo>().PlayerColorPercent.gameObject.transform.GetChild(2).GetComponent<TMP_Text>().text = ((int)((float)(MapColorData.MapPlayerCountData[i+1]) / 4096 * 100)).ToString() + "%";
                }
                else
                {
                    playerResultInfos[i].GetComponent<PlayerResultInfo>().PlayerNickName.text = Player1;
                    playerResultInfos[i + 1].GetComponent<PlayerResultInfo>().PlayerNickName.text = Player2;
                    playerResultInfos[i].GetComponent<PlayerResultInfo>().PlayerColorPercent.value = MapColorData.MapPlayerCountData[i] * 10;
                    playerResultInfos[i + 1].GetComponent<PlayerResultInfo>().PlayerColorPercent.value = MapColorData.MapPlayerCountData[i + 1] * 10;
                    playerResultInfos[i].GetComponent<PlayerResultInfo>().PlayerColorPercent.gameObject.transform.GetChild(2).GetComponent<TMP_Text>().text = ((int)((float)(MapColorData.MapPlayerCountData[i]) / 4096 * 100)).ToString() + "%";
                    playerResultInfos[i + 1].GetComponent<PlayerResultInfo>().PlayerColorPercent.gameObject.transform.GetChild(2).GetComponent<TMP_Text>().text = ((int)((float)(MapColorData.MapPlayerCountData[i + 1]) / 4096 * 100)).ToString() + "%";
                }
            }

            else // 다시 짜기
            {
                for (int j = i + 1; j < MapColorData.MapDataPlayer.Count; j++)
                {
                    PlayerResultInfo nextresultInfo = playerResultInfos[j].GetComponent<PlayerResultInfo>();
                    nextresultInfo.PlayerNickName.text = PhotonNetwork.PlayerList[j].NickName;
                    if (MapColorData.MapDataPlayer[i].NodeCount < MapColorData.MapDataPlayer[j].NodeCount)
                    {
                        int temp = MapColorData.MapDataPlayer[i].NodeCount;
                        MapColorData.MapDataPlayer[i].NodeCount = MapColorData.MapDataPlayer[j].NodeCount;
                        MapColorData.MapDataPlayer[j].NodeCount = temp;

                        Player1 = PhotonNetwork.PlayerList[i].NickName;
                        Player2 = PhotonNetwork.PlayerList[j].NickName;

                        string strtemp = Player1;
                        Player1 = Player2;
                        Player2 = strtemp;
                    }
                    nextresultInfo.PlayerNickName.text = Player2;
                    nextresultInfo.PlayerColorPercent.value = MapColorData.MapDataPlayer[j].NodeCount * 10;
                    nextresultInfo.PlayerColorPercent.gameObject.transform.GetChild(2).GetComponent<TMP_Text>().text = ((int)((float)(MapColorData.MapDataPlayer[j].NodeCount) / 4096 * 100)).ToString() + "%";
                }
                resultInfo.PlayerNickName.text = Player1;
                resultInfo.PlayerColorPercent.value = MapColorData.MapDataPlayer[i].NodeCount * 10;
                resultInfo.PlayerColorPercent.gameObject.transform.GetChild(2).GetComponent<TMP_Text>().text = ((int)((float)(MapColorData.MapDataPlayer[i].NodeCount) / 4096 * 100)).ToString() + "%";
            }
        }
               
    }

        
}

