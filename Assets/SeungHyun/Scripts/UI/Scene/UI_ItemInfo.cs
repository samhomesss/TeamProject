using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ItemInfo : UI_Scene
{
    Image itemImage; // ������ ������ �̹��� 
                     // �ش� �̹����� UI�� ĭ���� �޾ƿ��°ɷ� 
    Text itemText; // �����ۿ� ���� ����
                   // Action �� �ʿ��ϰ� 
                   // �ش� Action�� int ���� ���� 
                   // ���� �����ۿ� ���� ������ ���� ������ ���⿡ 

    public static GameObject ItemInfo
    {
        get { return go; }
        set { go = value; }
    }

    static GameObject go = null; // �ش� �������� ��������

    private void Start()
    {
        UI_Inven_Item.OnItemInfoChanged -= ChangeItemInfo;
        UI_Inven_Item.OnItemInfoChanged += ChangeItemInfo;
        itemImage = gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>();
        itemText = gameObject.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>();
        // ��� ������ �����ѵ� Ui�� �� ��ġ�� ��Ȳ�� ���� �̰� ���� �ϴ°� �´µ�?
    }

    void ChangeItemInfo(int itemID)
    {
        Debug.Log($"itemID�� {itemID} ");
        itemImage.sprite = Managers.ItemDataBase.GetItemData(itemID).itemImage;
        itemText.text = Managers.ItemDataBase.GetItemData(itemID).itemName;
    }

}
