using Photon.Pun;
using System.Collections;
using UnityEngine;

public class Util : MonoBehaviourPunCallbacks
{

    //public struct MyRect
    //{
    //    private float m_XMin;

    //    private float m_ZMin;

    //    private float m_Width;

    //    private float m_Height;

    public static void Log(object message)
    {
#if UNITY_EDITOR
        Debug.Log(message);
#endif
    }

    public static void LogRed(object message)
    {
#if UNITY_EDITOR
        Debug.Log($"<color=red>{message}</color>");
#endif
    }

    public static void LogGreen(object message)
    {
#if UNITY_EDITOR
        Debug.Log($"<color=green>{message}</color>");
#endif
    }

    public static void LogBlue(object message)
    {
#if UNITY_EDITOR
        Debug.Log($"<color=blue>{message}</color>");
#endif
    }

    public static IEnumerator CoActive(bool trigger, GameObject go, float time)
    {
        yield return new WaitForSeconds(time);
        go.SetActive(trigger);
    }//������Ʈ ��Ȱ��ȭ �ڷ�ƾ �߰�;

    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if (go == null)
            return null;

        if (recursive == false)
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                Transform transform = go.transform.GetChild(i);
                if (string.IsNullOrEmpty(name) || transform.name == name)
                {
                    T component = transform.GetComponent<T>();
                    if (component != null)
                        return component;
                }
            }
        }
        else
        {
            foreach (T component in go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) || component.name == name)
                    return component;
            }
        }

        return null;
    }


    public static GameObject FindChild(GameObject go, string name, bool recursion = false)
    {
        if (go == null || string.IsNullOrEmpty(name))
            return null;

        if (recursion)
        {
            foreach (Transform child in go.transform)
            {
                if (child.name == name)
                    return child.gameObject;

                GameObject found = FindChild(child.gameObject, name, recursion);
                if (found != null) return found;
            }
        }
        else
        {
            foreach (Transform child in go.transform)
            {
                if (child.name == name)
                    return child.gameObject;
            }
        }

        return null;
    }


    public static T GetOrAddComponent<T>(GameObject go) where T : Component
    {
        var component = go.GetComponent<T>();
        if (component == null)
            component = go.AddComponent<T>();

        return component;
    }
}
