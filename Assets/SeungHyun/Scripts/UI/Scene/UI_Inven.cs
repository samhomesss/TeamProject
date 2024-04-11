using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using yb;

public class UI_Inven : UI_Scene
{

    List<UI_Inven_Item> ui_Inven_Items = new List<UI_Inven_Item>();
    UI_Inven_Item invenItem;

    enum GameObjects
    {
        GridPanel
    }
     
    void Start()
    {
        Init();
        // 구독해주고
        //UI_ItemCreateButton.OnItemCreateClicked += ChangeImage;
        ItemInfoName.OnRelicGet -= ChangeImage;
        ItemInfoName.OnRelicGet += ChangeImage;
     }


    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));
        GameObject gridPanel = Get<GameObject>((int)GameObjects.GridPanel);
        foreach (Transform child in gridPanel.transform)
            Managers.Resources.Destroy(child.gameObject);

        // 실제 인벤토리 정보를 참고해서
        for (int i = 0; i < 4; i++)
        {
            GameObject item = Managers.UI.MakeSubItem<UI_Inven_Item>(gridPanel.transform).gameObject;
            invenItem = item.GetOrAddComponent<UI_Inven_Item>();
            // 리스트에 추가
            ui_Inven_Items.Add(invenItem);
            invenItem.SetInfo($"아이템{i}번");
        }
    }

    // 번호를 넘겨주는 아이템 번호로 사용
    public void ChangeImage(int itemID)
    {
        for (int i = 0; i < ui_Inven_Items.Count; i++)
        {
            if (!ui_Inven_Items[i].IsEmpty)
            {
                continue;
            }
        // Item 적용 부분

        if (itemID / 500 == 0) // 이거 조건 없어도 될듯? 아이템을 먹을때 해당 판정을 먼저함
        {
            
            ui_Inven_Items[i].IsEmpty = false;
            // 아랫부분 추후에 수정하고
            ui_Inven_Items[i].transform.GetChild(0).GetComponentInChildren<Image>().sprite = Managers.ItemDataBase.GetItemData(itemID).itemImage;
            ui_Inven_Items[i].SlotItemID = itemID; // 데이터 넘겨주는 형식
        }
            break;
        }
    }
}


