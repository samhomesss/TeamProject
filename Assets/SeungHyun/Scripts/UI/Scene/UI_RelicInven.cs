using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_RelicInven : UI_Scene
{

    List<UI_RelicInven_Item> ui_RelicInven_Items = new List<UI_RelicInven_Item>();
    UI_RelicInven_Item relicInvenItem;

    enum GameObjects
    {
        GridPanel
    }

    void Start()
    {
        Init();
        // 구독해주고
        //UI_ItemCreateButton.OnItemCreateClicked += ChangeImage;
        ItemInfoName.OnItemGet -= ChangeImage;
        ItemInfoName.OnItemGet += ChangeImage;
    }


    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        GameObject gridPanel = Get<GameObject>((int)GameObjects.GridPanel);
        foreach (Transform child in gridPanel.transform)
            Managers.Resources.Destroy(child.gameObject);

        // 실제 인벤토리 정보를 참고해서
        for (int i = 0; i < 2; i++)
        {
            GameObject item = Managers.UI.MakeSubItem<UI_RelicInven_Item>(gridPanel.transform).gameObject;
            relicInvenItem = item.GetOrAddComponent<UI_RelicInven_Item>();
            // 리스트에 추가
            ui_RelicInven_Items.Add(relicInvenItem);
            relicInvenItem.SetInfo($"유물{i}번");
        }
    }

    // 버튼 이용해서 아이템 먹었을때 사용 한거 
    // 번호를 넘겨주는 아이템 번호로 사용
    public void ChangeImage(int itemID)
    {
        for (int i = 0; i < ui_RelicInven_Items.Count; i++)
        {
            if (!ui_RelicInven_Items[i].IsEmpty)
            {
                continue;
            }
            if (itemID / 500 == 2)
            {
                ui_RelicInven_Items[i].IsEmpty = false;
                ui_RelicInven_Items[i].transform.GetChild(0).GetComponentInChildren<Image>().sprite = Managers.ItemDataBase.GetItemData(itemID).itemImage;
                ui_RelicInven_Items[i].SlotItemID = itemID; // 데이터 넘겨주는 형식
            }
            
            break;

        }
    }
}


