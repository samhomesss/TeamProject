using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using yb;

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
    Dictionary<string, int> _playerList = new Dictionary<string, int>();
    Button _goLobbyButton;
    int playerCount;
    private string[] _playersPath = new string[8];
    private void Start()
    {
        for(int i = 0; i< _playersPath.Length; i++) {
            _playersPath[i] = $"hw/PlayerPrefabs/Player{i + 1}";
        }

        playerCount = PlayerPrefs.GetInt("PlayerNumber");
        for (int i = 0; i < playerCount; i++) {
            if (!_playerList.ContainsKey(PlayerPrefs.GetString($"Rank{i + 1}"))) {
                _playerList.Add(PlayerPrefs.GetString($"Rank{i + 1}"), PlayerPrefs.GetInt($"Rank{i + 1}Percent"));
                PlayerController go = Managers.Resources.Instantiate(_playersPath[i], null).GetComponentInChildren<PlayerController>();
                go.SetWinPlayer(i);
            }
        }
        Init();

        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }

    public override void Init()
    {
        base.Init();
        Bind<GameObject>(typeof(GameObjects));
        _goLobbyButton = GameObject.Find("UI_GoLobbyButton").GetComponentInChildren<Button>();
        _goLobbyButton.onClick.AddListener(() =>
        {
            PhotonNetwork.LeaveRoom();
            Map.playerColors.Clear();
            UI_Timer.loadScene = null;
            var roomProperties = PhotonNetwork.CurrentRoom.CustomProperties;
            roomProperties.Clear();

            var playerProperties = PhotonNetwork.LocalPlayer.CustomProperties;
            playerProperties.Clear();

            PhotonNetwork.CurrentRoom.SetCustomProperties(roomProperties);
            PhotonNetwork.LocalPlayer.SetCustomProperties(playerProperties);
        });

        //전체 ResultInfo 읽어오고 일단 보이지 않게 설정 한다
        for (int i = 0; i < 8; i++)
        {
            playerResultInfos.Add(Get<GameObject>((int)((GameObjects)i)));
            playerResultInfos[i].SetActive(false);
        }

        for (int i = 0; i < playerCount; i++)
        {
            PlayerResultInfo resultInfo = playerResultInfos[i].GetComponent<PlayerResultInfo>();
            playerResultInfos[i].SetActive(true);
           
            
            //if (i + 1 == 1)
            //{
            //    resultInfo.PlayerNickName.text = "Player1";
            //    resultInfo.PlayerResultImage.sprite = Managers.Resources.Load<Sprite>(($"Prefabs/sh/UI/Texture/Player1"));
            //}
            //else if (i + 1 == 2)
            //{
            //    resultInfo.PlayerNickName.text = "Player2";
            //    resultInfo.PlayerResultImage.sprite = Managers.Resources.Load<Sprite>(($"Prefabs/sh/UI/Texture/Player2"));
            //}
            //else if (i + 1 == 3)
            //{
            //    resultInfo.PlayerNickName.text = "Player3";
            //    resultInfo.PlayerResultImage.sprite = Managers.Resources.Load<Sprite>(($"Prefabs/sh/UI/Texture/Player3"));
            //}
            //else
            //{
            //    resultInfo.PlayerNickName.text = $"Player{i + 1}";
            //    resultInfo.PlayerResultImage.gameObject.SetActive(false);
            //    resultInfo.PlayerResultNumber.gameObject.SetActive(true);
            //    resultInfo.PlayerResultNumber.text = $"{i + 1}.";
            //}
        }

        for (int i = 0; i < _playerList.Count; i++)
        {
            string rankNickName = PlayerPrefs.GetString($"Rank{i + 1}");
            if (_playerList.TryGetValue(rankNickName, out int percentValue))
            {
                playerResultInfos[i].SetActive(true);
                playerResultInfos[i].GetComponent<PlayerResultInfo>().Initisarize(rankNickName, ((int)(percentValue / 4096f * 100f)).ToString() + "%", percentValue);
            }
        }
    }
}

