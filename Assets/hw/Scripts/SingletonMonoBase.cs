using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SingletonMonoBase<T> : MonoBehaviour where T : SingletonMonoBase<T>
{
    private static T _instance;
    public static T instance
    {
        get
        {
            if (_instance == null) //인스턴스가 없다면 새로운 게임 오브젝트를 만들고 생성된 오브젝트의 이름을 제네릭T 타입의 이름으로 저장
               _instance = new GameObject(typeof(T).Name).AddComponent<T>();

            return _instance;

        }
    }

    protected virtual void Awake()
    {
        if(_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = (T)this;
    }
}
