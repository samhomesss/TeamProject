using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_RelicInven : UI_Scene
{
    public static List<UI_RelicInven_Item> UI_RelicInven_Items => ui_RelicInven_Items; // 해당 리스트를 가져오는 프로퍼티
    static List<UI_RelicInven_Item> ui_RelicInven_Items = new List<UI_RelicInven_Item>();
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
            relicInvenItem = item.GetOrAddComponent<UI_RelicInven_Item>(); // 해당 컴포넌트 추가해서 
            // 리스트에 추가
            ui_RelicInven_Items.Add(relicInvenItem);
            relicInvenItem.SetInfo($"유물{i}번");
        }
    }

    // 번호를 넘겨주는 아이템 번호로 사용
    public void ChangeImage(int itemID) // 유물 인텝토리에 아이템이 들어 왔을때 아이템 이미지 바꿔주는거
    {
        for (int i = 0; i < ui_RelicInven_Items.Count; i++)
        {
            if (!ui_RelicInven_Items[i].IsEmpty) // 유물창이 비었는지
            {
                continue; // 비어있지 않으면 그냥 다음꺼로 진행
            }
            
            GameObject RelicImage = Util.FindChild(ui_RelicInven_Items[i].gameObject, "ItemIcon", true);
            ui_RelicInven_Items[i].IsEmpty = false;
            RelicImage.GetComponent<Image>().sprite = Managers.ItemDataBase.GetItemData(itemID).itemImage;
            ui_RelicInven_Items[i].SlotItemID = itemID; 
            
            
            break;

        }
    }
}


