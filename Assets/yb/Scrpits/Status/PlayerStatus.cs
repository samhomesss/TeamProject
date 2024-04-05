using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace yb {
    public class PlayerStatus : BaseStatus {
        private float _moveSpeed;


        //부활 시간
        private float _resurrectionTime;

        //총 발사 시 이동속도 감소 수치
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
