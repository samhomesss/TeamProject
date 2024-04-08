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
        // �������ְ�
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

        // ���� �κ��丮 ������ �����ؼ�
        for (int i = 0; i < 2; i++)
        {
            GameObject item = Managers.UI.MakeSubItem<UI_RelicInven_Item>(gridPanel.transform).gameObject;
            relicInvenItem = item.GetOrAddComponent<UI_RelicInven_Item>();
            // ����Ʈ�� �߰�
            ui_RelicInven_Items.Add(relicInvenItem);
            relicInvenItem.SetInfo($"����{i}��");
        }
    }

    // ��ư �̿��ؼ� ������ �Ծ����� ��� �Ѱ� 
    // ��ȣ�� �Ѱ��ִ� ������ ��ȣ�� ���
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
                ui_RelicInven_Items[i].SlotItemID = itemID; // ������ �Ѱ��ִ� ����
            }
            
            break;

        }
    }
}


