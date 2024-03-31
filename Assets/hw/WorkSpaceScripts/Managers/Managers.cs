using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hw
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

        #region 3.31일 20:08 분 이희웅 _workFlow추가 완료 씬 전환을 하는 메인 클래스
        WorkFlow _workFlow = new WorkFlow();
        #endregion

        public static InputManager Input { get { return Instance._input; } }
        public static PoolManager Pool { get { return Instance._pool; } }
        public static ResourceManager Resource { get { return Instance._resource; } }
        public static SceneManagerEx Scene { get { return Instance._scene; } }
        public static SoundManager Sound { get { return Instance._sound; } }
        public static UIManager UI { get { return Instance._ui; } }

        #region 3.31일 20:08 분 이희웅 Work추가 _workFlow의 프로퍼티
        public static WorkFlow Work { get { return Instance._workFlow; } }
        #endregion

        void Start()
        {
            Init();
        }

        void Update()
        {
            _input.OnUpdate();
            _workFlow.Update();
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

