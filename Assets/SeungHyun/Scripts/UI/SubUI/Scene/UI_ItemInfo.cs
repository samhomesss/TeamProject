using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ItemInfo : UI_Scene
{
    public static GameObject ItemInfo // ��ü ķ���� �ʹݿ� ������ ����� ItemInfo
    {
        get { return go; }
        set { go = value; }
    }
    public GameObject Info => _info;
    public GameObject InfoImage => _infoImage;
    public GameObject InfoText => _infoText;

    GameObject _info;
    GameObject _infoImage;
    GameObject _infoText;
   
    static GameObject go = null; // �ش� �������� ��������

    private void Start()
    {
        _info = Util.FindChild(UI_ItemInfo.ItemInfo, "ItemInfoBackGround", true);
        _infoImage = Util.FindChild(UI_ItemInfo.ItemInfo, "ItemImage", true);
        _infoText = Util.FindChild(UI_ItemInfo.ItemInfo, "ItemInfoText", true);
    }

    //void ChangeItemInfo(string itemID)
    //{
    //    Debug.Log($"itemID�� {itemID} ");
    //    _infoImage.GetComponent<Image>().sprite = Managers.ItemDataBase.GetItemData(itemID).itemImage;
    //    _infoText.GetComponent<Text>().text = Managers.ItemDataBase.GetItemData(itemID).itemName;
    //}

}
