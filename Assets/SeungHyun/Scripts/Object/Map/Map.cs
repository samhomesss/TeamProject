using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using yb;
using static UnityEditor.Progress;
using Color = UnityEngine.Color;
public class Map : Obj
{
    #region Property
    public static GameObject MapObject => map;


    //0413 04:15 추가 
    // public enum PlayerName
    // {
    //     Player1, Player2, Player3, Player4, Player5, Player6, Player7, Player8
    // }
    //public enum Texture
    //{
    //    White,
    //}
    public static Node[,] Node => node; // 이거는 텍스쳐를 나눈거에요 
    public PlayerController[] Player => _player;
    #endregion

    static Node[,] node = new Node[64, 64]; // 가로 세로 64 * 64 의 노드로 나눈것처럼 
    static GameObject map; // 맵으로 띄워진 오브젝트 가져오는거 
    PlayerController[] _player; // 플레이어들
    Texture2D texture; // 내가 가져오는 Texture
    MeshRenderer meshRenderer; // Mesh

    const int length = 4;
    Color[] colors;
    Color[] defaultColors;

    static Dictionary<string, Color> playerColors = new Dictionary<string, Color>();

    private void Awake()
    {
        map = this.gameObject;
        var path = $"Prefabs/sh/Texture/White";

        playerColors.Add("Player1", Color.red); // 문자열로 비교 안하기 위해서 Dictionary로 만들어서 색 저장
        playerColors.Add("Player2", Color.yellow);
        playerColors.Add("Player3", Color.magenta);
        playerColors.Add("Player4", Color.cyan);
        playerColors.Add("Player5", Color.green);
        playerColors.Add("Player6", Color.blue);
        playerColors.Add("Player7", Color.gray);
        playerColors.Add("Player8", Color.black);

        texture = Managers.Resources.Load<Texture2D>(path);

        #region 04.13  수정 
        // Todo: 04.13 수정

        #region 주석처리
        //PlayerTestSh.OnNodeChanged -= UpdateColor;
        //PlayerTestSh.OnNodeChanged += UpdateColor;
        // PlayerTestSh.OnPlayerColorChecked -= PlayerColorCount;
        // PlayerTestSh.OnPlayerColorChecked += PlayerColorCount;
        #endregion
    }
    #region 주석처리
    //private void Update()
    //{
    //    // 총 노드의 갯수는 1024개 
    //    // Debug.Log(_colorcount);
    //}
    #endregion


    private void Start()
    {
        _player = new PlayerController[PhotonNetwork.CurrentRoom.PlayerCount];
        for (int i = 0; i < PhotonNetwork.CountOfPlayers; i++)
        {
            _player[i] = GameObject.Find($"Player{i + 1}").GetComponentInChildren<PlayerController>();
        }


        #endregion

        for (int i = 0; i < 64; i++)
        {
            for (int j = 0; j < 64; j++)
            {
                node[i, j] = new Node(new Vector3((i + 0.5f), 0, (j + 0.5f)));
            }
        }

        if (meshRenderer == null)
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }

        for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
        {
            _player[i] = GameObject.Find($"Player{i + 1}").GetComponentInChildren<PlayerController>();
        }

        texture = (Texture2D)Instantiate(meshRenderer.material.mainTexture);
        defaultColors = new Color[texture.width * texture.width];
        for (int ix = 0; ix < texture.width; ++ix)
        {
            for (int jx = 0; jx < texture.width; ++jx)
            {
                defaultColors[jx + ix * texture.width] = Color.white;
            }
        }
        texture.SetPixels(defaultColors);
        texture.Apply();

        colors = new Color[length * length];
        for (int ix = 0; ix < length; ++ix)
        {
            for (int jx = 0; jx < length; ++jx)
            {
                colors[jx + ix * length] = Color.yellow;
            }
        }
        meshRenderer.material.mainTexture = texture;
        SetPlayer(_player);
    }

    // 작성자: 장세윤(2024.04.15).
    // Todo: 플레이어가 이동할 때마다 이 함수가 중복되어 여러 번 호출되는 것으로 보임.
    // 호출 횟수를 줄이면서 정확하게 동작하도록 최적화가 필요해 보임.
    void UpdateColor()
    {
        int xPos;
        int yPos;
        for (int ix = 0; ix < length; ++ix)
        {
            for (int jx = 0; jx < length; ++jx)
            {
                for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
                {
                    colors[jx + ix * length] = PlayerColor(_player[i].transform.parent.gameObject);
                }
            }
        }
        
        // 플레이어 별로 루프 처리.
        for (int ix = 0; ix < PhotonNetwork.CurrentRoom.PlayerCount; ++ix)
        {
            // 작성자: 장세윤 (2024.04.15).
            // 검색 알고리즘 업데이트.
            // 플레이어의 위치를 기준으로 타일맵 위치 검색.
            // 플레이어의 위치가 곧 타일맵의 인덱스.
            var xIndex = Mathf.Clamp(Mathf.Max(0, (int)_player[ix].transform.position.x), 0, 63);
            var yIndex = Mathf.Clamp(Mathf.Max(0, (int)_player[ix].transform.position.z), 0, 63);
             
            // Todo: 작성자: 장세윤.
            // 더 정확하게 위치를 찾고 싶은 경우에 시도할 방법. (
            // 위에서 바로 찾은 인덱스를 기준으로 타일맵의 위치를 플레이어의 범위로 다시 확인.
            // 확인한 결과가 맞다면 그대로 타일맵 인덱스를 사용하고,
            // 아니라면, 그 주변의 나머지 8개(9개인데, 앞서 구한 인덱스는 아니기 때문.)에 대해서 x 범위와 z 범위를 다시 검색.
            // 이때 주변 타일맵의 인덱스를 구할 때는 배열 인덱스 범위가 벗어나지 않도록 주의.
                
            var item = node[xIndex, yIndex];
            xPos = (int)(item.nodePos.x + 0.5f);
            yPos = (int)(item.nodePos.z + 0.5f);

            // 앞서 구한 인덱스의 타일이 이미 내 플레이어가 점령한 위치라면, 색칠을 또 할 필요 없음.
            if (item.color == PlayerColor(_player[ix].transform.parent.gameObject))
            {
                Debug.Log("<color=red>여기 걸림</color>");
                continue;
            }

            Debug.Log("<color=green>여긴 밖</color>");

            colors = new Color[length * length];
            for (int jx = 0; jx < length; ++jx)
            {
                for (int kx = 0; kx < length; ++kx)
                {
                    colors[jx + kx * length] = PlayerColor(_player[ix].transform.parent.gameObject);
                }
            }

            texture.SetPixels(texture.width - xPos * 4, texture.height - yPos * 4, length, length, colors);
            texture.Apply();
            item.SetColor(PlayerColor(_player[ix].transform.parent.gameObject));
        }

        #region 기존 코드 백업 (최적화 전 코드)
        //int xPos;
        //int yPos;
        //for (int ix = 0; ix < length; ++ix)
        //{
        //    for (int jx = 0; jx < length; ++jx)
        //    {
        //        for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
        //        {
        //            colors[jx + ix * length] = PlayerColor(_player[i].transform.parent.gameObject);
        //        }
        //    }
        //}

        //foreach (var item in node)//0414 이희웅 수정 
        //{
        //    for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
        //    {
        //        if (item.nodePos.x - 0.75f <= _player[i].transform.position.x && item.nodePos.x + 0.75f >= _player[i].transform.position.x
        //           && item.nodePos.z + 0.75f >= _player[i].transform.position.z && item.nodePos.z - 0.75f <= _player[i].transform.position.z)
        //        {
        //            xPos = (int)(item.nodePos.x + 0.5f);
        //            yPos = (int)(item.nodePos.z + 0.5f);

        //            colors = new Color[length * length];
        //            for (int ix = 0; ix < length; ++ix)
        //            {
        //                for (int jx = 0; jx < length; ++jx)
        //                {
        //                    colors[jx + ix * length] = PlayerColor(_player[i].transform.parent.gameObject);
        //                }
        //            }

        //            texture.SetPixels(texture.width - xPos * 4, texture.height - yPos * 4, length, length, colors);
        //            texture.Apply();
        //            item.SetColor(PlayerColor(_player[i].transform.parent.gameObject));
        //        }
        //    }
        //}
        #endregion
    }
    #region 현재 사용하지 않는 코드
    //void PlayerColorCount(GameObject player)
    //{
    //    OnColorPercent?.Invoke();
    //    //_colorcount = 0;
    //    //Color playerColor = PlayerColor(player);
    //    //foreach (var item in node)
    //    //{   
    //    //    //if (item.color.Equals(PlayerColor(player)))
    //    //    if (item.color.Equals(playerColor))
    //    //    {
    //    //        //_colorcount++;
    //    //        //OnChangePercent?.Invoke(player); // 받아주고
    //    //        OnColorPercent?.Invoke();
    //    //    }
    //    //}
    //    //_colorcount /= 4;
    //}
    #endregion
    static public Color PlayerColor(GameObject player)
    {
        if (playerColors.TryGetValue(player.name, out Color value))
        {
            return value;
        }
        return Color.white;
        #region 주석처리
        //Color color = Color.white;
        //switch (Enum.Parse(typeof(PlayerName), player.name))
        //{
        //    //case "Player1":
        //    //    color = Color.red;
        //    //    break;
        //    //case "Player2":
        //    //    color = Color.yellow;
        //    //    break;
        //    //case "Player3":
        //    //    color = Color.magenta;
        //    //    break;
        //    //case "Player4":
        //    //    color = Color.cyan;
        //    //    break;
        //    //case "Player5":
        //    //    color = Color.green;
        //    //    break;
        //    //case "Player6":
        //    //    color = Color.blue;
        //    //    break;
        //    //case "Player7":
        //    //    color = Color.gray;
        //    //    break;
        //    //case "Player8":
        //    //    color = Color.black;
        //    //    break;
        #endregion
    }

    //윤범이형 Action 추가
    public void SetPlayer(PlayerController[] player)
    {
        for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
        {
            player[i].MapEvent += UpdateColor;
        }
    }


}

