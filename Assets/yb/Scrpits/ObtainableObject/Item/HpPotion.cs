using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace yb {
    public class HpPotion : ObtainableItem {
        private void Start() {
            _photonView = GetComponent<PhotonView>(); //0417 23:50 이희웅추가 
            type = Define.ItemType.HpPotion;
        }
        [PunRPC]
        public override void PickupPhoton(int playerViewId)
        {
            base.PickupPhoton(playerViewId);
            PhotonView _photonView = PhotonNetwork.GetPhotonView(playerViewId);
            PlayerController player = _photonView.GetComponent<PlayerController>();
            int count = 0;
            if (_photonView.IsMine)
            {
                while (count < PlayerController.MaxItemSlot)
                {
                    if (player.ItemList.ContainsKey(count))
                    {
                        if (player.ItemList[count].ItemType == type)
                        {
                            if (player.ItemList[count].ItemNumber < PlayerController.MaxItemNumber)
                            {
                                player.PickupController.SetItem(count, type);
                                    PhotonNetwork.Destroy(gameObject);
                                break;

                            }
                            else
                            {
                                count++;
                                continue;
                            }
                        }
                        else
                        {
                            count++;
                            continue;
                        }
                    }
                    else
                    {
                        player.PickupController.SetItem(count, type);
                            PhotonNetwork.Destroy(gameObject);
                        break;
                    }
                }
            }
        }
        public override void Pickup(PlayerController player) {
            base.Pickup(player);
            
            //0번 슬롯부터 확인
            //슬롯에 키값이 있다면, 그 슬롯에 있는 아이템 타입과 비교
            //그 슬롯의 아이템 타입과 같을 때
            //최대 아이템 갯수보다 보유 아이템 갯수가 적으면 그 슬롯에 추가
            //아이템 갯수가 더 많으면 다음 슬롯 검색
            //그 슬롯의 아이템 타입과 같지 않다면, 다음 슬롯 검색
            //슬롯에 키값이 없다면, 바로 추가

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

