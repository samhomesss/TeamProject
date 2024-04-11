using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ItemInfo : UI_Scene
{
    public static GameObject ItemInfo // ��ü ķ���� �ʹݿ� �����ϴ� ����� ItemInfo
    {
        get { return go; }
        set { go = value; }
    }
    public static GameObject Info => _info;
    public static GameObject InfoImage => _infoImage;
    public static GameObject InfoText => _infoText;

    #region �Ʒ��κ� ���� �ؼ� �ٽþ�
    //Image itemImage; // ������ ������ �̹��� 
    // �ش� �̹����� UI�� ĭ���� �޾ƿ��°ɷ� 
    // Text itemText; // �����ۿ� ���� ����
    // Action �� �ʿ��ϰ� 
    // �ش� Action�� int ���� ���� 
    // ���� �����ۿ� ���� ������ ���� ������ ���⿡ 
    #endregion
    #region 04.09 ���� �߰�
    static GameObject _info;
    static GameObject _infoImage;
    static GameObject _infoText;
    #endregion

   

    static GameObject go = null; // �ش� �������� ��������

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
        Debug.Log($"itemID�� {itemID} ");
        _infoImage.GetComponent<Image>().sprite = Managers.ItemDataBase.GetItemData(itemID).itemImage;
        _infoText.GetComponent<Text>().text = Managers.ItemDataBase.GetItemData(itemID).itemName;
    }

}
