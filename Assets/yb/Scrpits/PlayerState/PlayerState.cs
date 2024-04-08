using Photon.Pun;
using UnityEngine;

namespace yb {
    public class PlayerState {
        protected Data _data;

        public PlayerState() {
            _data = Managers.Data;
        }
    }
}
