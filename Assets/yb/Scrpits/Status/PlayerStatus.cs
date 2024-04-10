using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace yb {

    /// <summary>
    /// �÷��̾� �ɷ�ġ 
    /// </summary>
    public class PlayerStatus : BaseStatus {
        private float _moveSpeed;


        //��Ȱ �ð�
        private float _resurrectionTime;

        //�� �߻� �� �̵��ӵ� ���� ��ġ
        private float _moveSpeedDecrease = 1f;
        public float MoveSpeed => _moveSpeed;

        public float MoveSpeedDecrease => _moveSpeedDecrease;

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
