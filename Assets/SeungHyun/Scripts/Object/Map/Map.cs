using System;
using System.Collections.Generic;
using UnityEngine;
using yb;
using static UnityEditor.Progress;
using Color = UnityEngine.Color;


public class Map : Obj
{
    #region Property
    public enum PlayerName
    {
        Player1, Player2, Player3, Player4, Player5, Player6, Player7, Player8
    }
    public enum Texture
    {
        White,
    }
    public static Node[,] Node => node;
   // public static int ColorCount => _colorcount; // 안쓰고 있음
    public static GameObject Player => player;
    #endregion

    static GameObject player; // 플레이어 프리팹
    static Node[,] node = new Node[64, 64];
    //static int _colorcount = 0;

    Texture2D texture;
    MeshRenderer meshRenderer;

    const int length = 4;
    Color[] colors;
    Color[] defaultColors;

    static Dictionary<string, Color> playerColors = new Dictionary<string, Color>();

    // public static event Action<GameObject> OnChangePercent; // 미니맵 옆 퍼센테이지 바꾸는거
    // public static event Action OnColorPercent;

    private void Awake()
    {
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

        // 플레이어를 생성하면서 넣어줌
        player = Managers.SceneObj.ShowSceneObject<PlayerTestSh>().gameObject;
        player.name = ("Player1");

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
        
        PlayerTestSh.OnNodeChanged -= UpdateColor;
        PlayerTestSh.OnNodeChanged += UpdateColor;
       // PlayerTestSh.OnPlayerColorChecked -= PlayerColorCount;
       // PlayerTestSh.OnPlayerColorChecked += PlayerColorCount;
    }
    #region 주석처리
    //private void Update()
    //{
    //    // 총 노드의 갯수는 1024개 
    //    // Debug.Log(_colorcount);
    //}
    #endregion

    void UpdateColor()
    {
        int xPos;
        int yPos;
        for (int ix = 0; ix < length; ++ix)
        {
            for (int jx = 0; jx < length; ++jx)
            {
                colors[jx + ix * length] = PlayerColor(player);
            }
        }

        foreach (var item in node)
        {
            if (item.nodePos.x - 0.75f <= player.transform.position.x && item.nodePos.x + 0.75f >= player.transform.position.x
                && item.nodePos.z + 0.75f >= player.transform.position.z && item.nodePos.z - 0.75f <= player.transform.position.z)
            {
                xPos = (int)(item.nodePos.x + 0.5f);
                yPos = (int)(item.nodePos.z + 0.5f);

                texture.SetPixels(texture.width - xPos * 4, texture.height - yPos * 4, length, length, colors);
                //item.color = PlayerColor(player);
                item.SetColor(PlayerColor(player));
            }
        }

        texture.Apply();
        // 반복문 밖에서 새로 정보를 받아서 new를 만든 다음에 SetPixels는 모든 정보에 대한 갱신으로 하는 느낌이고
        // 반복문 안에서 할거면 SetPixel로 하는게 맞다.

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

    // 윤범이형 Action 추가 
    public void SetPlayer(PlayerController player)
    {
        player.MapEvent -= UpdateColor;
        player.MapEvent += UpdateColor;
    }

}

