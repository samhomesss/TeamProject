using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Hw
{
    public abstract class BaseScene : MonoBehaviour
    {


        public Define.Scene SceneType { get; protected set; } = Define.Scene.Unknown; //씬의 현재 상태를 불러올 프로퍼티 해당 클래스에서만 씬의 상태를 변경할 수 있음.

        void Awake()
        {
            Init();
        }

        protected virtual void Init()
        {

            Object obj = GameObject.FindObjectOfType(typeof(EventSystem));//객체를 하나만들어 하이어라키창에 이벤트 시스템의 타입을 찾고 찾으면 가져옴
            if (obj == null)
                Managers.Resource.Instantiate("UI/EventSystem").name = "@EventSystem";//Resources에 차지하고 있는 EventSystem프리펩이 메모리가 생성을 할때의 메모리보다 덜차지하기에 
        }

        public abstract void Clear();
    }
}

