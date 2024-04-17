using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace yb
{
    public class MoveSpeedUpPotion : ObtainableItem
    {
        private void Start()
        {
            _photonView = GetComponent<PhotonView>(); //0417 23:50 ¿Ã»Òøı√ﬂ∞°  
            type = Define.ItemType.MoveSpeedUpPotion;
        }

        [PunRPC]
        public override void PickupPhoton(int playerViewId)
        {
            base.PickupPhoton(playerViewId);
            PhotonView _photonView = PhotonNetwork.GetPhotonView(playerViewId);
            PlayerController player = _photonView.GetComponent<PlayerController>();
            if (player.ItemList.Count >= PlayerController.MaxItemSlot)
            {
                Debug.Log($"∏µÁ ΩΩ∑‘¿Ã ∞°µÊ √°Ω¿¥œ¥Ÿ");
                return;

            }
            int count = 0;

            if (_photonView.IsMine)
            {
                while (count < PlayerController.MaxItemSlot)
                {
                    if (player.ItemList.ContainsKey(count))
                    {
                        if (player.ItemList[count].ItemNumber >= PlayerController.MaxItemNumber)
                        {
                            Debug.Log($"{count}ΩΩ∑‘¿Ã ∞°µÊ √°Ω¿¥œ¥Ÿ");
                            count++;
                            continue;
                        }
                        else
                        {
                            player.PickupController.SetItem(count, type);
                            if (PhotonNetwork.IsMasterClient)
                                PhotonNetwork.Destroy(gameObject);
                            break;
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
            if (player.ItemList.Count >= PlayerController.MaxItemSlot)
            {
                Debug.Log($"∏µÁ ΩΩ∑‘¿Ã ∞°µÊ √°Ω¿¥œ¥Ÿ");
                return;
            }

            int count = 0;
            while (count < PlayerController.MaxItemSlot)
            {
                if (player.ItemList.ContainsKey(count))
                {
                    if (player.ItemList[count].ItemNumber >= PlayerController.MaxItemNumber)
                    {
                        Debug.Log($"{count}ΩΩ∑‘¿Ã ∞°µÊ √°Ω¿¥œ¥Ÿ");
                        count++;
                        continue;
                    }
                    else
                    {
                        player.PickupController.SetItem(count, type);
                        Managers.Resources.Destroy(gameObject);
                        break;
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

