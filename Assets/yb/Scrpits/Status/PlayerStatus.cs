using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace yb {

    /// <summary>
    /// 플레이어 능력치 
    /// </summary>
    public class PlayerStatus : BaseStatus {
        [SerializeField]private float _moveSpeed;

        //부활 시간
        [SerializeField]private float _resurrectionTime;

        //총 발사 시 이동속도 감소 수치
        private float _moveSpeedDecrease = 1f;
        public float MoveSpeed { get { return _moveSpeed; }set { _moveSpeed = value; } }

        public float MoveSpeedDecrease => _moveSpeedDecrease;

        public float ResurrectionTime => _resurrectionTime;

        protected override void Init() {
            base.Init();
            _maxHp = _data.DefaultPlayerMaxHp;
            _currentHp = _maxHp;
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
