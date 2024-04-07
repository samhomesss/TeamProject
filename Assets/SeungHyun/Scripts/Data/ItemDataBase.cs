using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;


// �������� ������ ������ ���̽� 
#region ������ ������ ���̽� ��� ���
// enum �ȿ� ������ ���̽� �ȿ� ���� 
// ��ũ���ͺ������Ʈ���� �̸��� �״�� �������� 
// itemData�� ����Ʈ �ȿ� 
// ��η� ������ ��ũ���ͺ������Ʈ���� ������
// �׸��� ��Ǿ�� �߰�
#endregion

public class ItemDataBase
{
    public enum ItemsData
    {
        Default = 0,
        Pistol = 50, // ������ ��ȣ 50 
        Rifle, // 51
        ShotGun, // 52
    }

    string[] itemDataName = Enum.GetNames(typeof(ItemsData));
    int itemDataLength;

    // �������� ���� �ϴ� Dictionary
    List<ItemData> itemDatas = new List<ItemData>();
    Dictionary<int, ItemData> _itemDictionary = new Dictionary<int, ItemData>();

    public void Init()
    {
        itemDataLength = itemDataName.Length;
        for (int i = 0; i < itemDataLength; i++)
        {
            var path = $"Prefabs/sh/Data/{itemDataName[i]}";

            itemDatas.Add(Managers.Resources.Load<ItemData>(path));
        }

        foreach (ItemData itemData in itemDatas)
        {
            if (_itemDictionary.TryGetValue(itemData.itemID, out ItemData testData))
            {
                //Debug.Log($"Ű �ߺ�: {itemData.itemID}");
            }
            else
            {
               // Debug.Log($"Ű ���: {itemData.itemID}");
                _itemDictionary.Add(itemData.itemID, itemData);
            }
        }

       // Debug.Log(_itemDictionary[50].name);

    }

    // �ۿ��� ���� �������°� 
    public ItemData GetItemData(int itemID)
    {

        return _itemDictionary[itemID];
    }

}