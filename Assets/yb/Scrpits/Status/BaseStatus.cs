using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace yb {
    /// <summary>
    /// 모든 유닛 능력치 관련 클래스
    /// </summary>
    public class BaseStatus : MonoBehaviour {
        protected Data _data;
        [SerializeField]protected int _currentHp;
        protected int _maxHp;

        public int MaxHp => _maxHp;
        public int CurrentHp => _currentHp;
        private void Start() {
            Init();
        }

        protected virtual void Init() {
            _data = Managers.Data;
            //if(IsTestMode.Instance.CurrentUser == Define.User.Hw) // 0410 22:52분 이희웅 테스트 모드 추가
            //{
            //    _maxHp = 1;
            //    _currentHp = _maxHp;
            //}
        }

        public int SetHp(int amout) { //0421 01:47분 이희웅 최대 체력이 넘어가기에 수정
            _currentHp += amout;
            if (_currentHp > MaxHp)
                _currentHp = MaxHp;

            return _currentHp;
        }
    }
}

