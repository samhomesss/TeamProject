using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace yb {
    public class CameraController : MonoBehaviour {
        Vector3 DestPos;
        private Transform _player;

        private void Start() {
            _player = transform.root;
           DestPos = transform.position;
        }
        private void LateUpdate() {
           transform.position = _player.transform.position + DestPos;
        }
    }

}
