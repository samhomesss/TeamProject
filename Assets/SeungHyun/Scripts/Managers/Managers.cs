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
        SoundManager _sound = new SoundManager();
        UIManager _ui = new UIManager();
        
        #region 04.01 승현 싱글톤 추가
        ItemDataBase _itemDataBase = new ItemDataBase();
        Node _node = new Node();
        UIMinimap _uiMinimap = new UIMinimap();

        public static UIMinimap UIMinimap { get { return Instance._uiMinimap; } }
        public static Node Node { get { return Instance._node; } }
        public static ItemDataBase ItemDataBase { get { return Instance._itemDataBase; } }
        #endregion

        public static InputManager Input { get { return Instance._input; } }
        public static PoolManager Pool { get { return Instance._pool; } }
        public static ResourceManager Resource { get { return Instance._resource; } }
        public static SceneManagerEx Scene { get { return Instance._scene; } }
        public static SoundManager Sound { get { return Instance._sound; } }
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
                GameObject go = GameObject.Find("@Managers");
                if (go == null)
                {
                    go = new GameObject { name = "@Managers" };
                    go.AddComponent<Managers>();
                }

                DontDestroyOnLoad(go);
                s_instance = go.GetComponent<Managers>();

                s_instance._pool.Init();
                s_instance._sound.Init();
                // 승현 추가
                s_instance._itemDataBase.Init();
            }
        }

        public static void Clear()
        {
            Input.Clear();
            Sound.Clear();
            Scene.Clear();
            UI.Clear();
            Pool.Clear();
        }

        
    }
}

