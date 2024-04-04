using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace yb {
    public class PlayerStatus : BaseStatus {
        private float _moveSpeed;

        private float _resurrectionTime;

        public float MoveSpeed => _moveSpeed;

        protected override void Init() {
            base.Init();
            _moveSpeed = _data.DefaultPlayerMoveSpeed;
            _resurrectionTime = _data.DefaultResurrectionTime;
        }

        public void SetResurrectionTime(float resurrectionTime) {
            _resurrectionTime = resurrectionTime;
        }
    }

}
