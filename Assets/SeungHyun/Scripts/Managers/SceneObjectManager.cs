using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Sh
{
    public class SceneObjectManager
    {
        GameObject _sceneObject = null;

        public GameObject ObjectRoot
        {
            get
            {
                GameObject objRoot = GameObject.Find("@Obj_Root");
                if (objRoot == null)
                    objRoot = new GameObject { name = "@Obj_Root" };
                return objRoot;

            }
        }

        public T ShowSceneObject<T>(string name = null) where T : Obj
        {
            if (string.IsNullOrEmpty(name))
                name = typeof(T).Name;

            GameObject go = Managers.Resource.Instantiate($"Obj/Scene/{name}");
            T sceneobj = Util.GetOrAddComponent<T>(go);
            _sceneObject = sceneobj.gameObject;

            go.transform.SetParent(ObjectRoot.transform);

            return sceneobj;
        }

        
    }

}
