using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.UI;
using yb;
using Color = UnityEngine.Color;
public class UI_PlayerColorPercent : UI_Scene
{

    //int player1Count;
    //int player2Count;
    //int player3Count;
    //int player4Count;
    //int player5Count;
    //int player6Count;
    //int player7Count;
    //int player8Count;

    

    float resetTimer;

    GameObject[] _playerSlider = new GameObject[8];
    List<PlayerController> _players = new List<PlayerController>();
<<<<<<< Updated upstream
    Map map;
=======

    PlayerController[] _playercontorllers = new PlayerController[PhotonNetwork.CurrentRoom.PlayerCount];
>>>>>>> Stashed changes
    float timer = 300;
    private void Start()
    {
        for (int i = 0; i < _playerSlider.Length; i++)
        {
            _playerSlider[i] = Util.FindChild(gameObject, $"Player{i + 1}", true);
            _playerSlider[i].SetActive(false);
        }

        Debug.Log(timer + "시간");
        Init(map.Player);
    }

    public void Init(PlayerController player)
    {
        _players.Add(player);
    }

    private void Update()
    {
        resetTimer += Time.deltaTime;
        timer -= Time.deltaTime;
        if (resetTimer>= 2f)
        {
            ColorPercent();
            resetTimer = 0;
        }
        if (timer <= 0)
        {
            ColorPercent();
        }
    }

    void ColorPercent()
    {
        #region 원래 코드
        //player1Count = 0;
        //player2Count = 0;
        //player4Count = 0;
        //player3Count = 0;
        //player5Count = 0;
        //player6Count = 0;
        //player7Count = 0;
        //player8Count = 0;

        //foreach (var item in Map.Node)
        //{

        //    if (item.nodeColor == NodeColor.Red) 
        //    {
        //        player1Count++;
        //        _playerSlider[0].GetComponent<Slider>().value = player1Count;
        //    }
        //    else if (item.nodeColor == NodeColor.Yellow)
        //    {
        //        player2Count++;
        //        _playerSlider[1].GetComponent<Slider>().value = player2Count;
        //    }
        //    else if (item.nodeColor == NodeColor.Magenta)
        //    {
        //        player3Count++;
        //        _playerSlider[2].GetComponent<Slider>().value = player3Count;

        //    }
        //    else if (item.nodeColor == NodeColor.Cyan)
        //    {
        //        player4Count++;
        //        _playerSlider[3].GetComponent<Slider>().value = player4Count;

        //    }
        //    else if (item.nodeColor == NodeColor.Green)
        //    {
        //        player5Count++;
        //        _playerSlider[4].GetComponent<Slider>().value = player5Count;
        //    }
        //    else if (item.nodeColor == NodeColor.Blue)
        //    {
        //        player6Count++;
        //        _playerSlider[5].GetComponent<Slider>().value = player6Count;

        //    }
        //    else if (item.nodeColor == NodeColor.Gray)
        //    {
        //        player7Count++;
        //        _playerSlider[6].GetComponent<Slider>().value = player7Count;
        //    }
        //    else if (item.nodeColor == NodeColor.Black)
        //    {
        //        player8Count++;
        //        _playerSlider[7].GetComponent<Slider>().value = player8Count;
        //    }
        //}
        #endregion

        for (int j = 0; j < _players.Count; j++)
        {
            _players[j].NodeCount = 0;
        }

        foreach (var item in Map.Node)
        {

            if (item.nodeColor == NodeColor.Red)
            {
                _players[0].NodeCount++;
                _playerSlider[0].GetComponent<Slider>().value = _players[0].NodeCount;
            }
            else if (item.nodeColor == NodeColor.Yellow)
            {
                _players[1].NodeCount++;
                _playerSlider[1].GetComponent<Slider>().value = _players[1].NodeCount;
            }
            else if (item.nodeColor == NodeColor.Magenta)
            {
                _players[2].NodeCount++;
                _playerSlider[2].GetComponent<Slider>().value = _players[2].NodeCount;

            }
            else if (item.nodeColor == NodeColor.Cyan)
            {
                _players[3].NodeCount++;
                _playerSlider[3].GetComponent<Slider>().value = _players[3].NodeCount;

            }
            else if (item.nodeColor == NodeColor.Green)
            {
                _players[4].NodeCount++;
                _playerSlider[4].GetComponent<Slider>().value = _players[4].NodeCount;
            }
            else if (item.nodeColor == NodeColor.Blue)
            {
                _players[5].NodeCount++;
                _playerSlider[5].GetComponent<Slider>().value = _players[5].NodeCount;

            }
            else if (item.nodeColor == NodeColor.Gray)
            {
                _players[6].NodeCount++;
                _playerSlider[6].GetComponent<Slider>().value = _players[6].NodeCount;
            }
            else if (item.nodeColor == NodeColor.Black)
            {
                _players[7].NodeCount++;
                _playerSlider[7].GetComponent<Slider>().value = _players[7].NodeCount;
            }
            //for (int i = 0; i < _players.Count; i++)
            //{
                
            //    if (item.nodeColor == (NodeColor)i)
            //    {
            //        _playerSlider[i].GetComponent<Slider>().value = ++_players[i].NodeCount;
            //        _playerSlider[i].SetActive(true);
            //    }
            //}
        }
    }

}
