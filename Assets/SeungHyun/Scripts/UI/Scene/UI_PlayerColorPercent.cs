using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.UI;
using yb;
using Color = UnityEngine.Color;
public class UI_PlayerColorPercent : UI_Scene
{
    int player1Count;
    int player2Count;
    int player3Count;
    int player4Count;
    int player5Count;
    int player6Count;
    int player7Count;
    int player8Count;

    float resetTimer;

    GameObject[] _playerSlider = new GameObject[8];
   // Map map; // 플레이어를  가지고 오기 위한 map
    
    private void Start()
    {
      //  map = Map.MapObject.GetComponent<Map>();
        for (int i = 0; i < _playerSlider.Length; i++)
        {
            _playerSlider[i] = Util.FindChild(gameObject, $"Player{i + 1}", true);
        }
    }

    private void Update()
    {
        resetTimer += Time.deltaTime;
        if (resetTimer>= 2f)
        {
            ColorPercent();
            resetTimer = 0;
        }
    }

    void ColorPercent()
    {
        player1Count = 0;
        player2Count = 0;
        player4Count = 0;
        player3Count = 0;
        player5Count = 0;
        player6Count = 0;
        player7Count = 0;
        player8Count = 0;
        
        foreach (var item in Map.Node)
        {
            
            if (item.nodeColor == NodeColor.Red) 
            {
                player1Count++;
                _playerSlider[0].GetComponent<Slider>().value = player1Count;
            }
            else if (item.nodeColor == NodeColor.Yellow)
            {
                player2Count++;
                _playerSlider[1].GetComponent<Slider>().value = player2Count;
            }
            else if (item.nodeColor == NodeColor.Magenta)
            {
                player3Count++;
                _playerSlider[2].GetComponent<Slider>().value = player3Count;

            }
            else if (item.nodeColor == NodeColor.Cyan)
            {
                player4Count++;
                _playerSlider[3].GetComponent<Slider>().value = player4Count;

            }
            else if (item.nodeColor == NodeColor.Green)
            {
                player5Count++;
                _playerSlider[4].GetComponent<Slider>().value = player5Count;
            }
            else if (item.nodeColor == NodeColor.Blue)
            {
                player6Count++;
                _playerSlider[5].GetComponent<Slider>().value = player6Count;

            }
            else if (item.nodeColor == NodeColor.Gray)
            {
                player7Count++;
                _playerSlider[6].GetComponent<Slider>().value = player7Count;
            }
            else if (item.nodeColor == NodeColor.Black)
            {
                player8Count++;
                _playerSlider[7].GetComponent<Slider>().value = player8Count;
            }
        }

    }

    // Todo: 윤범이형 액션 연결 따로 연결

    //void SetPlayer(PlayerController player)
    //{
    //  //  player.ColorPercentEvent += ColorPercent;
    //}
}
