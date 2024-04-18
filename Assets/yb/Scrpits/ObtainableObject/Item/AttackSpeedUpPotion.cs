using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Diagnostics;


namespace yb
{
    public class AttackSpeedUpPotion : ObtainableItem
    {
        private void Start()
        {
            _photonView = GetComponent<PhotonView>(); //0417 23:50 ¿Ã»Òøı√ﬂ∞° 
            type = Define.ItemType.AttackSpeedUpPotion;
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
                while (count < PlayerController.MaxItemSlot)
                {
                    if (player.ItemList.TryGetValue(count, out PlayerController.Item item))
                    {
                        if (item.ItemType == type)
                        {
                            if (item.ItemNumber < PlayerController.MaxItemNumber)
                            {
                                Util.LogRed("item.ItemNumber < PlayerController.MaxItemNumber ¡∂∞« ∏∏¡∑");
                                player.PickupController.SetItem(count, type, () =>
                                {
                                    Util.LogRed("SetItem æ»ø° Action »£√‚µ ");

                                    if (PhotonNetwork.IsMasterClient)
                                    {
                                        Util.LogGreen("SetItem æ»ø° Action »£√‚µ  -> Master Client");
                                        PhotonNetwork.Destroy(gameObject);
                                    }
                                    else
                                    {
                                        Util.LogGreen("SetItem æ»ø° Action »£√‚µ  -> Not Master Client");
                                    }
                                });
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
                        player.PickupController.SetItem(count, type, () =>
                        {
                            if (PhotonNetwork.IsMasterClient)
                            {
                                PhotonNetwork.Destroy(gameObject);
                            }
                        });
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
                            Managers.Resources.Destroy(gameObject);
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
                    Managers.Resources.Destroy(gameObject);
                    break;
                }
            }
        }

    }
}

