using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace yb {
    public class HpPotion : ObtainableItem {
        private void Start() {
            type = Define.ItemType.HpPotion;
        }
        public override void Pickup(PlayerController player) {
            base.Pickup(player);
            
            //0�� ���Ժ��� Ȯ��
            //���Կ� Ű���� �ִٸ�, �� ���Կ� �ִ� ������ Ÿ�԰� ��
            //�� ������ ������ Ÿ�԰� ���� ��
            //�ִ� ������ �������� ���� ������ ������ ������ �� ���Կ� �߰�
            //������ ������ �� ������ ���� ���� �˻�
            //�� ������ ������ Ÿ�԰� ���� �ʴٸ�, ���� ���� �˻�
            //���Կ� Ű���� ���ٸ�, �ٷ� �߰�

            int count = 0;
            while (count < PlayerController.MaxItemSlot) {
                if (player.ItemList.ContainsKey(count)) {
                    if (player.ItemList[count].ItemType == type) {
                        if (player.ItemList[count].ItemNumber < PlayerController.MaxItemNumber) {
                            player.PickupController.SetItem(count, type);
                            Managers.Resources.Destroy(gameObject);
                            break;

                        } else {
                            count++;
                            continue;
                        }
                    } else {
                        count++;
                        continue;
                    }
                } else {
                    player.PickupController.SetItem(count, type);
                    Managers.Resources.Destroy(gameObject);
                    break;
                }
            }
        }
    }
}

