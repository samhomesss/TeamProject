using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace yb {
    public class PlayerStatus : BaseStatus {
        private float _moveSpeed;

        private float _resurrectionTime;

        private float _moveSpeedDecrease = 1f;
        public float MoveSpeed => _moveSpeed;

        public float MoveSpeedDecrease => _moveSpeedDecrease;

        protected override void Init() {
            base.Init();
            _moveSpeed = _data.DefaultPlayerMoveSpeed;
            _resurrectionTime = _data.DefaultResurrectionTime;
        }

        public void SetResurrectionTime(float resurrectionTime) {
            _resurrectionTime = resurrectionTime;
        }

        public void SetMoveSpeedDecrease(float decrease) {
            _moveSpeedDecrease = decrease;
        }
    }

}
