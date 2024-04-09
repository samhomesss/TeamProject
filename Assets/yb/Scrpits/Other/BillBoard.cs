using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace yb {
    /// <summary>
    /// 빌보드시킬 오브젝트에 할당
    /// </summary>
    public class BillBoard : MonoBehaviour {
        void Update() {
            Vector3 cameraPos = Camera.main.transform.position;
            transform.LookAt(cameraPos);
        }
    }
}
