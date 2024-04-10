using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace yb {
    /// <summary>
    /// 모든 유닛 능력치 관련 클래스
    /// </summary>
    public class BaseStatus : MonoBehaviour {
        protected Data _data;
        protected int _currentHp;
        protected int _maxHp;

        public int MaxHp => _maxHp;
        public int CurrentHp => _currentHp;
        private void Start() {
            Init();
        }

        protected virtual void Init() {
            _data = Managers.Data;
        }

        public int SetHp(int amout) {
            _currentHp += amout;
            return _currentHp;
        }
    }
}

