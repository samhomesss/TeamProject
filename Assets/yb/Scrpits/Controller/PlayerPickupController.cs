using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using yb;

namespace yb {
    public class PlayerPickupController : MonoBehaviour {
        private PlayerController _player;
        private Data _data;
        private bool[] _haveRelic = new bool[(int)Define.RelicType.Count];

        private void Awake() {
            _player = GetComponent<PlayerController>();
        }
        void Start() {
            _data = Managers.Data;
        }

        public void SetRelic(IRelic relic) {
            _haveRelic[(int)relic.RelicType] = true;
            _player.WeaponController.SetRelic(relic);
            _player.Status.SetResurrectionTime(_data.BonusResurrectionTime);
        }

        public void DeleteRelic(IRelic relic) {
            _haveRelic[(int)relic.RelicType] = true;
            _player.WeaponController.SetRelic(relic);
            _player.Status.SetResurrectionTime(_data.DefaultResurrectionTime);
        }

        public bool[] IsRelic() => _haveRelic;
    }
}

