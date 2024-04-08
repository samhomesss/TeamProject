using UnityEngine;
using Color = UnityEngine.Color;


public class Map : Obj
{
    public enum Texture
    {
        White,
    }
    public static GameObject Player => player;
    Texture2D texture;
    static GameObject player; // 플레이어 프리팹
    MeshRenderer meshRenderer;
    const int length = 4;
    Node[,] node = new Node[64, 64];
    Color[] colors;
    Color[] defaultColors;

    private void Awake()
    {
        var path = $"Prefabs/sh/Texture/White";
        
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
    }
    private void Update()
    {
        //Debug.Log(PlayerColorCount(player) / 4);
    }
   
    void UpdateColor()
    {
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
                var xPos = (int)(item.nodePos.x + 0.5f);
                var yPos = (int)(item.nodePos.z + 0.5f);
               
                texture.SetPixels(texture.width - xPos * 4, texture.height - yPos * 4, length, length, colors);
                item.color = PlayerColor(player);
                texture.Apply();
            }
        }
    }
    public int PlayerColorCount(GameObject player)
    {
        int count = 0;
        foreach (var item in node)
        {
            if (item.color == PlayerColor(player))
            {
                count++;
            }
        }
        return count;
    }
    public Color PlayerColor(GameObject player)
    {
        Color color = Color.white;
        switch (player.name)
        {
            case "Player1":
                color = Color.yellow;
                break;
            case "Player2":
                color = Color.red;
                break;
        }
        return color;
    }
    
}

