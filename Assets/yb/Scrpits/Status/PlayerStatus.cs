using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace yb {
    public class PlayerStatus : BaseStatus {
        private float _moveSpeed;

        public float MoveSpeed => _moveSpeed;
        protected override void Init() {
            base.Init();
            _moveSpeed = _data.DefaultPlayerMoveSpeed;
        }
    }

}
