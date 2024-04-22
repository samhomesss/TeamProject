using Photon.Pun;
using Photon.Realtime;
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
    #region ���� ��� ����
    //int player1Count;
    //int player2Count;
    //int player3Count;
    //int player4Count;
    //int player5Count;
    //int player6Count;
    //int player7Count;
    //int player8Count;
    #endregion

    static public int[] PlayerCount => playerCount;
    static int[] playerCount = new int[8];
    static public GameObject UIPlayerColorPercent => ui_PlayerColorPercent;
    public List<PlayerController> Players => _players;
    float resetTimer;

    private WaitForSeconds _waitforuser = new WaitForSeconds(0.1f);
    
    GameObject[] _playerSlider = new GameObject[8];
    List<PlayerController> _players = new List<PlayerController>();

    PlayerController[] _playercontorllers = new PlayerController[PhotonNetwork.CurrentRoom.PlayerCount];

    static GameObject ui_PlayerColorPercent;

    //float timer = 15f;
    private void Start()
    {
       StartCoroutine(WaitPlayerLoded());//�÷��̾ ���� ��Ʈ�ѷ��� �����Ŀ� �ؿ� ��ũ��Ʈ�� ���� 0420 ����� �߰� 

    }

    public void Init(PlayerController player)
    {
        _players.Add(player);
    }

    // NodeCount�� ����Ҷ� ���
    public List<PlayerController> SortNodeCount() // ��� ī��Ʈ ����
    {
        _players.Sort((x, y) => y.NodeCount.CompareTo(x.NodeCount));
        return _players;
    }


    private void Update()
    {
        resetTimer += Time.deltaTime;
       // timer -= Time.deltaTime;
        if (resetTimer >= 2f)
        {
            ColorPercent();
            resetTimer = 0;
        }
       // if (timer <= 0)
       // {
       //     ColorPercent();
       //     MapColorData.MapPlayerCountData = playerCount;
       //     MapColorData.MapDataPlayer = _players;
       //     // ���� �Ѵ� ���� ���� ����
       //     //Debug.Log(MapColorData.MapDataPlayer[0].NodeCount + "MapDataPlayer1");
       //     //Debug.Log(MapColorData.MapDataPlayer[1].NodeCount + "MapDataPlayer2");
       // }
    }

    // �÷��̾�
    IEnumerator WaitPlayerLoded()
    {
        // �÷��̾��� �ε��� ��ٸ��ϴ�.
        bool allPlayersLoaded = false;
        while (!allPlayersLoaded)
        {
            for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
            {
                allPlayersLoaded = GameObject.Find($"Player{i + 1}").GetComponentInChildren<PlayerController>();
                if (allPlayersLoaded == false)
                    break;
            }
            yield return _waitforuser;
        }

        ui_PlayerColorPercent = this.gameObject;
        for (int i = 0; i < _playerSlider.Length; i++)
        {
            _playerSlider[i] = Util.FindChild(gameObject, $"Player{i + 1}", true);
            _playerSlider[i].SetActive(false);
        }

        //Debug.Log(timer + "�ð�");
        for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
        {
            _playercontorllers[i] = GameObject.Find($"Player{i + 1}").GetComponentInChildren<PlayerController>();
            Init(_playercontorllers[i]);
        }

    }
    void ColorPercent()
    {

        #region ���� �ڵ�
        //for (int i = 0; i < playerCount.Length; i++)
        //{
        //    playerCount[i] = 0;
        //}

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
        //        playerCount[0]++;
        //        _players[0].NodeCount = playerCount[0];
        //        _playerSlider[0].GetComponent<Slider>().value = playerCount[0];
        //        _playerSlider[0].SetActive(true);

        //    }
        //    else if (item.nodeColor == NodeColor.Yellow)
        //    {
        //        playerCount[1]++;
        //        _players[1].NodeCount = playerCount[1];
        //        _playerSlider[1].GetComponent<Slider>().value = playerCount[1];
        //        _playerSlider[1].SetActive(true);

        //    }
        //    else if (item.nodeColor == NodeColor.Magenta)
        //    {
        //        playerCount[2]++;
        //        _players[2].NodeCount = playerCount[2];
        //        _playerSlider[2].GetComponent<Slider>().value = playerCount[2];

        //    }
        //    else if (item.nodeColor == NodeColor.Cyan)
        //    {
        //        playerCount[3]++;
        //        _players[3].NodeCount = playerCount[3];
        //        _playerSlider[3].GetComponent<Slider>().value = playerCount[3];

        //    }
        //    else if (item.nodeColor == NodeColor.Green)
        //    {
        //        playerCount[4]++;
        //        _players[4].NodeCount = playerCount[4];
        //        _playerSlider[4].GetComponent<Slider>().value = playerCount[4];
        //    }
        //    else if (item.nodeColor == NodeColor.Blue)
        //    {
        //        playerCount[5]++;
        //        _players[5].NodeCount = playerCount[5];
        //        _playerSlider[5].GetComponent<Slider>().value = playerCount[5];

        //    }
        //    else if (item.nodeColor == NodeColor.Gray)
        //    {
        //        playerCount[6]++;
        //        _players[6].NodeCount = playerCount[0];
        //        _playerSlider[6].GetComponent<Slider>().value = playerCount[6];
        //    }
        //    else if (item.nodeColor == NodeColor.Black)
        //    {
        //        playerCount[7]++;
        //        _players[7].NodeCount = playerCount[7];
        //        _playerSlider[7].GetComponent<Slider>().value = playerCount[7];
        //    }
        //}
        #endregion

        #region ������ ���� �ۿ� �ȵǾ �Ⱒ ù��° ���� �޾ƿ��µ�

        //foreach (var item in Map.Node)
        //{
        //    for (int i = 0; i < _players.Count; i++)
        //    {
        //        if (item.nodeColor == (NodeColor)i)
        //        {
        //            playerCount[i]++;
        //            //_players[i].NodeCount = playerCount[i];
        //            _playerSlider[i].GetComponent<Slider>().value = playerCount[i];
        //            _playerSlider[i].SetActive(true);

        //        }
        //    }
        //}
        #endregion

        #region �׳� NodeCount ����

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
                _playerSlider[1].SetActive(true);
            }
            else if (item.nodeColor == NodeColor.Magenta)
            {
                _players[2].NodeCount++;
                _playerSlider[2].GetComponent<Slider>().value = _players[2].NodeCount;
                _playerSlider[2].SetActive(true);

            }
            else if (item.nodeColor == NodeColor.Cyan)
            {
                _players[3].NodeCount++;
                _playerSlider[3].GetComponent<Slider>().value = _players[3].NodeCount;
                _playerSlider[3].SetActive(true);

            }
            else if (item.nodeColor == NodeColor.Green)
            {
                _players[4].NodeCount++;
                _playerSlider[4].GetComponent<Slider>().value = _players[4].NodeCount;
                _playerSlider[4].SetActive(true);
            }
            else if (item.nodeColor == NodeColor.Blue)
            {
                _players[5].NodeCount++;
                _playerSlider[5].GetComponent<Slider>().value = _players[5].NodeCount;
                _playerSlider[5].SetActive(true);

            }
            else if (item.nodeColor == NodeColor.Gray)
            {
                _players[6].NodeCount++;
                _playerSlider[6].GetComponent<Slider>().value = _players[6].NodeCount;
                _playerSlider[6].SetActive(true);
            }
            else if (item.nodeColor == NodeColor.Black)
            {
                _players[7].NodeCount++;
                _playerSlider[7].GetComponent<Slider>().value = _players[7].NodeCount;
                _playerSlider[7].SetActive(true);
            }
            for (int i = 0; i < _players.Count; i++)
            {

                if (item.nodeColor == (NodeColor)i)
                {
                    _playerSlider[i].GetComponent<Slider>().value = ++_players[i].NodeCount;
                    _playerSlider[i].SetActive(true);
                }
            }
            #endregion
        }
    }
}

