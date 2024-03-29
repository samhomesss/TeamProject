using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace yb {
    public class CameraController : MonoBehaviour {
        Vector3 DestPos;
        [SerializeField] GameObject _player;

        private void Start() {
            DestPos = transform.position;
        }
        private void LateUpdate() {
            transform.position = _player.transform.position + DestPos;
        }
    }

}
