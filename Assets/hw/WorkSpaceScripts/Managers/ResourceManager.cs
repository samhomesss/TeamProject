using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hw
{
    public class ResourceManager
    {
        public T Load<T>(string path) where T : Object
        {
            if (typeof(T) == typeof(GameObject))//GameObject인 이유는 스크립트를 가져올 수 있기 때문에 스크립트를 가져오는것을 제한하고자 조건문을 씀
            {
                string name = path;
                int index = name.LastIndexOf('/');//path경로 상에 마지막 '/'인덱스의 int값을 돌려줌 
                if (index >= 0) //인덱스가 1이상이면 경로에 로드할 객체가 있다는뜻.
                    name = name.Substring(index + 1);// 인덱스 이후로 나오는 문자열을 출력하기에 +1을 해줌

                GameObject go = Managers.Pool.GetOriginal(name);
                if (go != null)
                    return go as T;
            }

            return Resources.Load<T>(path);//Resources의 경로
        }

        public GameObject Instantiate(string path, Transform parent = null)
        {
            GameObject original = Load<GameObject>($"Prefabs/hw/{path}");
            if (original == null)
            {
                Debug.Log($"Failed to load prefab : {path}");
                return null;
            }

            if (original.GetComponent<Poolable>() != null)
                return Managers.Pool.Pop(original, parent).gameObject;

            GameObject go = Object.Instantiate(original, parent);
            go.name = original.name;
            return go;
        }

        public void Destroy(GameObject go)
        {
            if (go == null)
                return;

            Poolable poolable = go.GetComponent<Poolable>();
            if (poolable != null)
            {
                Managers.Pool.Push(poolable);
                return;
            }

            Object.Destroy(go);
        }
    }
}

