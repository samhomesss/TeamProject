using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#region ���� ������� ����
// ������ �Ծ����� �ٲ�� �� ����� �г�
// ���� ����� �ٲ� ������� ������?
public class UI_ItemChangePanel : UI_Scene
{ }
//{
//    //public static Action OnChangedItem; // �������� �ٲܶ� 
//    //public static int ItemID => _itemID;
//    //public static GameObject ItemChagnePanel => _panel;

//    //static int _itemID;
//    //static GameObject _panel;
//    GameObject _itemImage; // �г��� ������ �ִ� ������ �̹���
//    GameObject _itemText; // �г��� ������ �ִ� ������ �ؽ�Ʈ
//    GameObject _dontChangeButton; // �г��� ������ �ִ� ��ư 

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