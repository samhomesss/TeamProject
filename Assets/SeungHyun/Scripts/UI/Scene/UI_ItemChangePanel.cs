using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#region 현재 사용하지 않음
// 아이템 먹었을때 바뀌는 거 띄워줄 패널
// 현재 방식이 바뀌어서 사용하지 않을듯?
public class UI_ItemChangePanel : UI_Scene
{ }
//{
//    //public static Action OnChangedItem; // 아이템을 바꿀때 
//    //public static int ItemID => _itemID;
//    //public static GameObject ItemChagnePanel => _panel;

//    //static int _itemID;
//    //static GameObject _panel;
//    GameObject _itemImage; // 패널이 가지고 있는 아이템 이미지
//    GameObject _itemText; // 패널이 가지고 있는 아이템 텍스트
//    GameObject _dontChangeButton; // 패널이 가지고 있는 버튼 

//    private void Start()
//    {
//        //_panel = gameObject;
//        _itemImage = Util.FindChild(gameObject, "ItemImage", true);
//        _itemText = Util.FindChild(gameObject, "ItemText", true);
//        _dontChangeButton = Util.FindChild(gameObject, "DontChangedItem", true);

//       // _itemID = ItemInfoName.Item.GetComponent<Item>().ItemID;
//        _itemImage.GetComponent<Image>().sprite = Managers.ItemDataBase.GetItemData(ItemInfoName.Item.name).itemImage;
//        _itemText.GetComponent<Text>().text = Managers.ItemDataBase.GetItemData(ItemInfoName.Item.name).itemName;

//        UI_RelicInven_Item.IsChanged = true;
//        _dontChangeButton.GetComponent<Button>().onClick.AddListener(() => Destroy(gameObject));
//        //ItemInfoName.OnItemNotCloesed += DestroyPanel;
//        Debug.Log(ItemInfoName.Item.name);

//    }

//    void DestroyPanel()
//    {
//        //Destroy(gameObject);
//       // ItemInfoName.OnItemNotCloesed -= DestroyPanel;
//       // ItemInfoName.Count = 0;
//    }
//}
#endregion