using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ItemInfo : MonoBehaviour
{
    Image itemImage; // ������ ������ �̹��� 
                     // �ش� �̹����� UI�� ĭ���� �޾ƿ��°ɷ� 
    Text itemText; // �����ۿ� ���� ����
    // Action �� �ʿ��ϰ� 
    // �ش� Action�� int ���� ���� 
    // ���� �����ۿ� ���� ������ ���� ������ ���⿡ 


    private void Start()
    { 
        // ��� ������ �����ѵ� Ui�� �� ��ġ�� ��Ȳ�� ���� �̰� ���� �ϴ°� �´µ�?
        itemImage = gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>();
        itemText = gameObject.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>();

        UI_Inven_Item.OnItemInfoChanged += ChangeItemInfo;

    }

    void ChangeItemInfo(int itemID)
    {
        Debug.Log($"itemID�� {itemID} ");
        itemImage.sprite = Managers.ItemDataBase.GetItemData(itemID).itemImage;
        itemText.text = Managers.ItemDataBase.GetItemData(itemID).itemName;
    }

}
