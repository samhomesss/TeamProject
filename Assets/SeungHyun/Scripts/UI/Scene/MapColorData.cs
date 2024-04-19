using ExitGames.Client.Photon;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using yb;

public class MapColorData : MonoBehaviour
{
    public static MapColorData Instance { get { Init(); return _instance; } }
    static public MapColorData _instance;
    
    public static List<PlayerController> MapDataPlayer { get { return _mapDataplayers; } set { _mapDataplayers = value; } }

    public static int[] MapPlayerCountData { get { return _mapPlayerCountData; } set { _mapPlayerCountData = value; } }
    static int[] _mapPlayerCountData = new int[8]; 

    static List<PlayerController> _mapDataplayers = new List<PlayerController>();
    static List<string> _playersNickName = new List<string>();  
    static List<int> _playersNodeCheck = new List<int>();    
    
    public void Start()
    {
        Init();
        
    }

    private static void Init()
    {
        if (_instance == null)
        {
            GameObject mapData = GameObject.Find("@MapColorData");
            if (mapData == null)
            {
                mapData = new GameObject("@MapColorData");
                mapData.AddComponent<MapColorData>();
            }

            DontDestroyOnLoad(mapData);
            _instance = mapData.GetComponent<MapColorData>();
        }
    }
}
