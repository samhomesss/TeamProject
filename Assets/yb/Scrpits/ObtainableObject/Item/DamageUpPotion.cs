using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace yb {
    public class DamageUpPotion : ObtainableItem {
        private void Start() {
            type = Define.ItemType.DamageUpPotion;
        }
        public override void Pickup(PlayerController player) {
            base.Pickup(player);
            if (player.ItemList.Count >= PlayerController.MaxItemSlot) {
                Debug.Log($"∏µÁ ΩΩ∑‘¿Ã ∞°µÊ √°Ω¿¥œ¥Ÿ");
                return;
            }

            int count = 0;
            while(count < PlayerController.MaxItemSlot) {
                if (player.ItemList.ContainsKey(count)) {
                    if (player.ItemList[count].ItemNumber >= PlayerController.MaxItemNumber) {
                        Debug.Log($"{count}ΩΩ∑‘¿Ã ∞°µÊ √°Ω¿¥œ¥Ÿ");
                        count++;
                        continue;
                    } else {
                        player.PickupController.SetItem(count, type);
                        Managers.Resources.Destroy(gameObject);
                        break;
                    }
                }
                else {
                    player.PickupController.SetItem(count, type);
                    Managers.Resources.Destroy(gameObject);
                    break;
                }
            }
        }
    }
}

