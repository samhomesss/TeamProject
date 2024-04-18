using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace yb
{
    public class DamageUpPotion : ObtainableItem
    {
        private void Start()
        {
            _photonView = GetComponent<PhotonView>(); //0417 23:50 ������߰� 
            type = Define.ItemType.DamageUpPotion;
        }

        [PunRPC]
        public override void PickupPhoton(int playerViewId)
        {
            base.PickupPhoton(playerViewId);
            PhotonView playerPhotonView = PhotonNetwork.GetPhotonView(playerViewId);
            PlayerController player = playerPhotonView.GetComponent<PlayerController>();
            int count = 0;
            if (PhotonNetwork.GetPhotonView(playerViewId).IsMine)
            {
               _photonView.TransferOwnership(playerViewId);
                while (count < PlayerController.MaxItemSlot)
                {
                    if (player.ItemList.ContainsKey(count))
                    {
                        if (player.ItemList[count].ItemType == type)
                        {
                            if (player.ItemList[count].ItemNumber < PlayerController.MaxItemNumber)
                            {
                                player.PickupController.SetItem(count, type);
                                if (PhotonNetwork.IsMasterClient)
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
                        if (PhotonNetwork.IsMasterClient)
                            PhotonNetwork.Destroy(gameObject);
                        break;
                    }
                }
            }
        }
        public override void Pickup(PlayerController player)
        {
            base.Pickup(player);
            int count = 0;
            while (count < PlayerController.MaxItemSlot)
            {
                if (player.ItemList.ContainsKey(count))
                {
                    if (player.ItemList[count].ItemType == type)
                    {
                        if (player.ItemList[count].ItemNumber < PlayerController.MaxItemNumber)
                        {
                            player.PickupController.SetItem(count, type);
                            if (PhotonNetwork.IsMasterClient)
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
                    if (PhotonNetwork.IsMasterClient)
                        PhotonNetwork.Destroy(gameObject);
                    break;
                }
            }
        }

    }
}

