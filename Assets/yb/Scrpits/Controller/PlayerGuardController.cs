using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace yb {
    public class PlayerGuardController : MonoBehaviour {
        private float _speed;
        private float _radius;
        private float _defaultAngle = 0f;
        private float _defaultYPos;
        [SerializeField]private PlayerController _player;
        private void Start() {
            //_player = transform.parent.GetComponentInChildren<PlayerController>();
            _speed = Managers.Data.DefaultGuardSpeed;
            _radius = Managers.Data.DefaultGuardSpeed;
            _defaultYPos = transform.position.y;
            //transform.position = _player.transform.position;
        }

        void Update() {
            _defaultAngle += _speed * Time.deltaTime;

            float x = Mathf.Cos(_defaultAngle) * _radius;
            float z = Mathf.Sin(_defaultAngle) * _radius;

            Vector3 pos = new Vector3(x, _defaultYPos, z);
            pos = pos + _player.transform.position/* + _player.PlayerMoveVelocity*/;
            transform.localPosition = pos;
        }
    }
}

