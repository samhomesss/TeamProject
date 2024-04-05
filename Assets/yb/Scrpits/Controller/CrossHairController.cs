using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace yb {
    public class CrossHairController : MonoBehaviour {
        void Update() {
            OnMoveUpdate();
        }

        private void OnMoveUpdate() {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool hit = Physics.Raycast(ray, out var target, float.MaxValue);

            if (!hit)
                return;

            //transform.position = new Vector3(target.point.x ,1.5f, target.point.z);
            transform.position = target.point;
        }
    }
}

