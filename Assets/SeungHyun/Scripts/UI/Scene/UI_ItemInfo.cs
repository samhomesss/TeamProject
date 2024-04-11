using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ItemInfo : UI_Scene
{
    public static GameObject ItemInfo // 전체 캠버스 초반에 생성하대 사용할 ItemInfo
    {
        get { return go; }
        set { go = value; }
    }
    public static GameObject Info => _info;
    public static GameObject InfoImage => _infoImage;
    public static GameObject InfoText => _infoText;

    #region 아랫부분 정리 해서 다시씀
    //Image itemImage; // 아이템 아이콘 이미지 
    // 해당 이미지는 UI의 칸으로 받아오는걸로 
    // Text itemText; // 아이템에 대한 설명
    // Action 이 필요하고 
    // 해당 Action은 int 값만 전달 
    // 현재 아이템에 대한 정보에 대한 설명은 없기에 
    #endregion
    #region 04.09 승현 추가
    static GameObject _info;
    static GameObject _infoImage;
    static GameObject _infoText;
    #endregion

   

    static GameObject go = null; // 해당 아이템을 가져오고

    private void Start()
    {
        _info = Util.FindChild(UI_ItemInfo.ItemInfo, "ItemInfoBackGround", true);
        _infoImage = Util.FindChild(UI_ItemInfo.ItemInfo, "ItemImage", true);
        _infoText = Util.FindChild(UI_ItemInfo.ItemInfo, "ItemInfoText", true);


       // UI_Inven_Item.OnItemInfoChanged -= ChangeItemInfo;
       // UI_Inven_Item.OnItemInfoChanged += ChangeItemInfo;
       // UI_RelicInven_Item.OnItemInfoChanged -= ChangeItemInfo;
       // UI_RelicInven_Item.OnItemInfoChanged += ChangeItemInfo;
    }

    void ChangeItemInfo(string itemID)
    {
        Debug.Log($"itemID는 {itemID} ");
        _infoImage.GetComponent<Image>().sprite = Managers.ItemDataBase.GetItemData(itemID).itemImage;
        _infoText.GetComponent<Text>().text = Managers.ItemDataBase.GetItemData(itemID).itemName;
    }

}
