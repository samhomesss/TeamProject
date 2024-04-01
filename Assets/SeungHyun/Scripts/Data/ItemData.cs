using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Data")]
[Serializable]
public class ItemData : ScriptableObject
{
    public ItemType type; // 아이템 타입
    public int itemID; // 아이템 아이디
    public string itemName; // 아이템 이름
    public Sprite itemImage; // 아이템 이미지
}
