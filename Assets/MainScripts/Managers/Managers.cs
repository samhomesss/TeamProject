
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    private static Managers _instance;
    public static Managers Instance { get { Init();return _instance; } }


    private Data _data = new Data();

    // 수정사항 
    private GameManager _gameManager = new GameManager();
    private PoolManager _pool = new PoolManager();
    private InputManager _input = new InputManager();
    private ResourcesManager _resources = new ResourcesManager();
    private SceneManagerEX _scene = new SceneManagerEX();

    #region 04.05 승현 추가 
    UIManager _ui = new UIManager();
    SceneObjectManager _sceneObj = new SceneObjectManager();
    ItemDataBase _itemDataBase = new ItemDataBase();
    public static SceneObjectManager SceneObj => _instance._sceneObj;
    public static ItemDataBase ItemDataBase => _instance._itemDataBase;
    public static UIManager UI => _instance._ui;
    #endregion

    #region 04.05 이희웅 추가
    //[SerializeField]WorkFlow _workFlow = new WorkFlow(); //3.31일 20:08 분 워크플로우 싱글톤 추가
    //public static WorkFlow Work { get { return Instance._workFlow; } } // 0405 17:57분 워크플로우가 필요없어,삭제함
    #endregion

    public static Data Data => _instance._data;
    // 수정 사항 
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
        //GameManager.Workflow(); 0405 17:57분 희웅 삭제, 워크플로우가 필요없어,삭제함
        GameManager.Update(); //0407 17:00 희웅 추가, 워크플로우를 없애고, 게임씬을 관리해줄 GameManager 싱글톤 업데이트 추가
    }

    private static void Init()
    {
        if (_instance == null)
        {
           // Debug.Log("Init");

            GameObject managers = GameObject.Find("@Managers");
            if (managers == null)
            {
                managers = new GameObject("@Managers");
                managers.AddComponent<Managers>();
            }
            
            DontDestroyOnLoad(managers);
            _instance = managers.GetComponent<Managers>();
            
            Pool.Init();

            #region 04.05 승현 추가
            ItemDataBase.Init();
            #endregion
        }
    }
}
