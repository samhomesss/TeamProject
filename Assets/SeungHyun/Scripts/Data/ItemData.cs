using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemData 
{
    public ItemData(int ItemID , string ItemName, Sprite ItemIamge , string ItemInfo ) { itemID = ItemID; itemName = ItemName;  itemImage = ItemIamge;  itemInfo = ItemInfo; }
    public ItemType type; // 아이템 타입
    public int itemID; // 아이템 아이디
    public string itemName; // 아이템 이름
    public Sprite itemImage; // 아이템 이미지
    public string itemInfo;
}
