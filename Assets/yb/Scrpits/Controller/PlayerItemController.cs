using System.Collections.Generic;
using UnityEngine;

namespace yb {
    public class PlayerItemController : MonoBehaviour {
        private PlayerController _player;
        Dictionary<int, PlayerController.Item> _itemList;
        private void Start() {
            _player = GetComponent<PlayerController>();
            _itemList = _player.ItemList;
        }

        private void Update() {
            
        }


        public void UseItem(Define.ItemType type) {
            

        }
    }
}