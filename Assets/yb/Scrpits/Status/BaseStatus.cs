using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace yb {
    /// <summary>
    /// ��� ���� �ɷ�ġ ���� Ŭ����
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
            //if(IsTestMode.Instance.CurrentUser == Define.User.Hw) // 0410 22:52�� ����� �׽�Ʈ ��� �߰�
            //{
            //    _maxHp = 1;
            //    _currentHp = _maxHp;
            //}
        }

        public int SetHp(int amout) { //0421 01:47�� ����� �ִ� ü���� �Ѿ�⿡ ����
            _currentHp += amout;
            if (_currentHp > MaxHp)
                _currentHp = MaxHp;

            return _currentHp;
        }
    }
}

