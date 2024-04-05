using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace yb {
    public class BillBoard : MonoBehaviour {
        void Update() {
            Vector3 cameraPos = Camera.main.transform.position;
            transform.LookAt(cameraPos);
        }
    }
}
