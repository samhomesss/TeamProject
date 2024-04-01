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
            if (_instance == null) //�ν��Ͻ��� ���ٸ� ���ο� ���� ������Ʈ�� ����� ������ ������Ʈ�� �̸��� ���׸�T Ÿ���� �̸����� ����
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
