using Photon.Pun;
using UnityEngine;

namespace yb {
    public class PlayerState {
        protected Data _data;
        protected PhotonView _photonView;

        public PlayerState(PlayerController player) {
            _data = Managers.Data;
            _photonView = player.PhotonView;
        }
    }
}
