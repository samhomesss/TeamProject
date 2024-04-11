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
   // public static int ColorCount => _colorcount; // �Ⱦ��� ����
    public static GameObject Player => player;
    #endregion

    static GameObject player; // �÷��̾� ������
    static Node[,] node = new Node[64, 64];
    //static int _colorcount = 0;

    Texture2D texture;
    MeshRenderer meshRenderer;

    const int length = 4;
    Color[] colors;
    Color[] defaultColors;

    static Dictionary<string, Color> playerColors = new Dictionary<string, Color>();

    // public static event Action<GameObject> OnChangePercent; // �̴ϸ� �� �ۼ������� �ٲٴ°�
    // public static event Action OnColorPercent;

    private void Awake()
    {
        var path = $"Prefabs/sh/Texture/White";

        playerColors.Add("Player1", Color.red); // ���ڿ��� �� ���ϱ� ���ؼ� Dictionary�� ���� �� ����
        playerColors.Add("Player2", Color.yellow);
        playerColors.Add("Player3", Color.magenta);
        playerColors.Add("Player4", Color.cyan);
        playerColors.Add("Player5", Color.green);
        playerColors.Add("Player6", Color.blue);
        playerColors.Add("Player7", Color.gray);
        playerColors.Add("Player8", Color.black);
        
        texture = Managers.Resources.Load<Texture2D>(path);

        // �÷��̾ �����ϸ鼭 �־���
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
    #region �ּ�ó��
    //private void Update()
    //{
    //    // �� ����� ������ 1024�� 
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
        // �ݺ��� �ۿ��� ���� ������ �޾Ƽ� new�� ���� ������ SetPixels�� ��� ������ ���� �������� �ϴ� �����̰�
        // �ݺ��� �ȿ��� �ҰŸ� SetPixel�� �ϴ°� �´�.

    }
    #region ���� ������� �ʴ� �ڵ�
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
    //    //        //OnChangePercent?.Invoke(player); // �޾��ְ�
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
        #region �ּ�ó��
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

    // �������� Action �߰� 
    public void SetPlayer(PlayerController player)
    {
        player.MapEvent -= UpdateColor;
        player.MapEvent += UpdateColor;
    }

}

