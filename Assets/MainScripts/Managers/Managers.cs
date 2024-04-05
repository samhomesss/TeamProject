using Sh;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    private static Managers _instance;
    public static Managers Instance {
        get {
            Init();
            return _instance;
        }
    }


    private Data _data = new Data();

    // �������� 
    private GameManager _gameManager = new GameManager();
    //
    private PoolManager _pool = new PoolManager();
    private InputManager _input = new InputManager();
    private ResourcesManager _resources = new ResourcesManager();
    private SceneManagerEX _scene = new SceneManagerEX();

    #region 04.05 ���� �߰� 
    UIManager _ui = new UIManager();
    SceneObjectManager _sceneObj = new SceneObjectManager();
    ItemDataBase _itemDataBase = new ItemDataBase();
    public static SceneObjectManager SceneObj => _instance._sceneObj;
    public static ItemDataBase ItemDataBase => _instance._itemDataBase;
    public static UIManager UI => _instance._ui;
    #endregion


    public static Data Data => _instance._data;
    // ���� ���� 
    public static GameManager GameManager => _instance._gameManager;
    public static PoolManager Pool => _instance._pool;
    public static SceneManagerEX Scene => _instance._scene;
    public static InputManager Input => Instance._input;
    public static ResourcesManager Resources => Instance._resources;
    
    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        Input.OnUpdate();
        GameManager.Workflow();
    }

    private static void Init()
    {
        if (_instance == null)
        {
            Debug.Log("Init");

            GameObject managers = GameObject.Find("@Managers");
            if (managers == null)
            {
                managers = new GameObject("@Managers");
                managers.AddComponent<Managers>();
            }
            
            DontDestroyOnLoad(managers);
            _instance = managers.GetComponent<Managers>();
            
            Pool.Init();

            #region 04.05 ���� �߰�
            ItemDataBase.Init();
            #endregion
        }
    }
}
