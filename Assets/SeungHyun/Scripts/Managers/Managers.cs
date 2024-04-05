using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sh
{
    public class Managers : MonoBehaviour
    {
        
        static Managers s_instance;
        static Managers Instance { get { Init(); return s_instance; } }

        InputManager _input = new InputManager();
        PoolManager _pool = new PoolManager();
        ResourceManager _resource = new ResourceManager();
        SceneManagerEx _scene = new SceneManagerEx();
        UIManager _ui = new UIManager();
        
        #region 04.01 승현 싱글톤 추가
        SceneObjectManager _sceneObj = new SceneObjectManager(); // Scene에서 사용할 Object 저장소
        ItemDataBase _itemDataBase = new ItemDataBase();

        public static SceneObjectManager SceneObj { get { return Instance._sceneObj; } }
        public static ItemDataBase ItemDataBase { get { return Instance._itemDataBase; } }
        #endregion

        public static InputManager Input { get { return Instance._input; } }
        public static PoolManager Pool { get { return Instance._pool; } }
        public static ResourceManager Resource { get { return Instance._resource; } }
        public static SceneManagerEx Scene { get { return Instance._scene; } }
        public static UIManager UI { get { return Instance._ui; } }

        void Start()
        {
            Init();
        }

        void Update()
        {
            _input.OnUpdate();
            _input.BulletShot();
        }

        static void Init()
        {
            if (s_instance == null)
            {
                GameObject go = GameObject.Find("@2Managers");
                if (go == null)
                {
                    go = new GameObject { name = "@2Managers" };
                    go.AddComponent<Managers>();
                }

                DontDestroyOnLoad(go);
                s_instance = go.GetComponent<Managers>();

                s_instance._pool.Init();
                // 승현 추가
                s_instance._itemDataBase.Init();
            }
        }

        public static void Clear()
        {
            Input.Clear();
            Scene.Clear();
            UI.Clear();
            Pool.Clear();
        }

        
    }
}

