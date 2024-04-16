using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using yb;
using static UnityEditor.Progress;

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
        player.ItemEvent += ChangeImage;
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
    public void ChangeImage(string ItemID)
    {
        if (ItemID == "ShieldRelic" || ItemID == "BonusAttackSpeedRelic" || ItemID == "BonusProjectileRelic"
             || ItemID == "BonusResurrectionTimeRelic" || ItemID == "GuardRelic" || ItemID == "ObtainablePistol" || ItemID == "ObtainableShotgun" || ItemID == "ObtainableRifle")
        { return; }

            for (int i = 0; i < ui_Inven_Items.Count; i++)
            {
            if (!ui_Inven_Items[i].IsEmpty)
            {
                continue;
            }

            ui_Inven_Items[i].IsEmpty = false;
            // 아랫부분 코드가 좀 더러운거 같아서 추후에 수정하고
            ui_Inven_Items[i].transform.GetChild(0).GetComponentInChildren<Image>().sprite = Managers.ItemDataBase.GetItemData(ItemID).itemImage;
            ui_Inven_Items[i].SlotItemID = Managers.ItemDataBase.GetItemData(ItemID).itemName; 
            break;
        }
    }
}


