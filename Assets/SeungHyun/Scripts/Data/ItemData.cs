using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemData 
{
    public ItemData(int ItemID , string ItemName, Sprite ItemIamge , string ItemInfo ) { itemID = ItemID; itemName = ItemName;  itemImage = ItemIamge;  itemInfo = ItemInfo; }
    public ItemType type; // ������ Ÿ��
    public int itemID; // ������ ���̵�
    public string itemName; // ������ �̸�
    public Sprite itemImage; // ������ �̹���
    public string itemInfo;
}
