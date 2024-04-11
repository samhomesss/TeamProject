using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace yb {
    public class PlayerGuardController : MonoBehaviour {
        private float _speed;
        private float _radius;
        private float _defaultAngle = 0f;
        private float _defaultYPos;

        private void Start() {
            _speed = Managers.Data.DefaultGuardSpeed;
            _radius = Managers.Data.DefaultGuardSpeed;
            _defaultYPos = transform.position.y;
        }

        void Update() {
            _defaultAngle += _speed * Time.deltaTime;

            float x = Mathf.Cos(_defaultAngle) * _radius;
            float z = Mathf.Sin(_defaultAngle) * _radius;

            transform.localPosition = new Vector3(x, _defaultYPos, z);
        }
    }
}

