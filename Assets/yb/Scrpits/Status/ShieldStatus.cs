using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using yb;

namespace yb {
    public class ShieldStatus : BaseStatus {
        private float _shieldRecoveryTime;
        public float ShieldRecoveryTime => _shieldRecoveryTime;
        protected override void Init() {
            base.Init();
            _maxHp = _data.DefaultShieldMaxHp;
            _currentHp = _maxHp;
            _shieldRecoveryTime = _data.DefaultShieldRecuveryTime;
        }
    }
}

