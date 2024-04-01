using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Data")]
[Serializable]
public class ItemData : ScriptableObject
{
    public ItemType type; // ������ Ÿ��
    public int itemID; // ������ ���̵�
    public string itemName; // ������ �̸�
    public Sprite itemImage; // ������ �̹���
}
