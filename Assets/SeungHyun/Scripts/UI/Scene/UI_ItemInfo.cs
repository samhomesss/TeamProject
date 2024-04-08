using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ItemInfo : UI_Scene
{
    Image itemImage; // 아이템 아이콘 이미지 
                     // 해당 이미지는 UI의 칸으로 받아오는걸로 
    Text itemText; // 아이템에 대한 설명
                   // Action 이 필요하고 
                   // 해당 Action은 int 값만 전달 
                   // 현재 아이템에 대한 정보에 대한 설명은 없기에 

    public static GameObject ItemInfo
    {
        get { return go; }
        set { go = value; }
    }

    static GameObject go = null; // 해당 아이템을 가져오고

    private void Start()
    {
        UI_Inven_Item.OnItemInfoChanged -= ChangeItemInfo;
        UI_Inven_Item.OnItemInfoChanged += ChangeItemInfo;
        itemImage = gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>();
        itemText = gameObject.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>();
        // 상관 없을거 같긴한데 Ui을 또 고치는 상황이 오면 이건 수정 하는게 맞는듯?
    }

    void ChangeItemInfo(int itemID)
    {
        Debug.Log($"itemID는 {itemID} ");
        itemImage.sprite = Managers.ItemDataBase.GetItemData(itemID).itemImage;
        itemText.text = Managers.ItemDataBase.GetItemData(itemID).itemName;
    }

}
