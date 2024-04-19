using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using yb;

public class UI_Inven : UI_Scene
{
    Map map;
    List<UI_Inven_Item> ui_Inven_Items = new List<UI_Inven_Item>();
    UI_Inven_Item invenItem;


    enum GameObjects
    {
        GridPanel
    }
     
    void Start()
    {
        map = Map.MapObject.GetComponent<Map>();
        Init();

        SetPlayer(map.Player);
     }

    void SetPlayer(PlayerController player)
    {
        player.SetItemEvent += ChangeImage;
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));
        GameObject gridPanel = Get<GameObject>((int)GameObjects.GridPanel);
        foreach (Transform child in gridPanel.transform)
            Managers.Resources.Destroy(child.gameObject);

        for (int i = 0; i < 4; i++)
        {
            GameObject item = Managers.UI.MakeSubItem<UI_Inven_Item>(gridPanel.transform).gameObject;
            invenItem = item.GetOrAddComponent<UI_Inven_Item>();
            ui_Inven_Items.Add(invenItem);
            invenItem.SetInfo($" ");
        }
    }

    // 번호를 넘겨주는 아이템 번호로 사용
    // int(슬롯) Item(아이템 정보)
    public void ChangeImage(int slotID, PlayerController.Item Item)
    {
        #region 삭제
        //if (ItemID == "ShieldRelic" || ItemID == "BonusAttackSpeedRelic" || ItemID == "BonusProjectileRelic"
        //     || ItemID == "BonusResurrectionTimeRelic" || ItemID == "GuardRelic" || ItemID == "Pistol" || ItemID == "Shotgun" || ItemID == "Rifle")
        //{ return; }

        //for (int i = 0; i < ui_Inven_Items.Count; i++)
        //{
        //if (!ui_Inven_Items[i].IsEmpty)
        //{
        //    continue;
        //}
        // ui_Inven_Items[i].IsEmpty = false;
        // 아랫부분 코드가 좀 더러운거 같아서 추후에 수정하고
        //ui_Inven_Items[i].transform.GetChild(0).GetComponentInChildren<Image>().sprite = Managers.ItemDataBase.GetItemData(ItemID).itemImage;
        //ui_Inven_Items[i].SlotItemID = Managers.ItemDataBase.GetItemData(ItemID).itemName; 
        //break;
        #endregion
        if (Item.ItemNumber == 0)
        {
            ui_Inven_Items[slotID].transform.GetChild(0).GetComponentInChildren<Image>().sprite = default;
            ui_Inven_Items[slotID].SlotItemID = Item.ItemType.ToString();
            ui_Inven_Items[slotID].transform.GetChild(2).GetComponentInChildren<TMP_Text>().text = Item.ItemNumber.ToString();//0418 00:29 이희웅 Text -> TMP_Text로 수정
        }

        ui_Inven_Items[slotID].transform.GetChild(0).GetComponentInChildren<Image>().sprite = Managers.Resources.Load<Sprite>($"Prefabs/sh/UI/Texture/{Item.ItemType.ToString()}");
        ui_Inven_Items[slotID].SlotItemID = Item.ItemType.ToString();
        ui_Inven_Items[slotID].transform.GetChild(2).GetComponentInChildren<TMP_Text>().text = Item.ItemNumber.ToString();//0418 00:29 이희웅 Text -> TMP_Text로 수정
    }
}


