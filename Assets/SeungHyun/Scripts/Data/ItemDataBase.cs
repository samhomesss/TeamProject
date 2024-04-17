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
    #region �ּ�ó��
    //public enum ItemsData
    //{
    //    Default = 0,
    //    Pistol = 50, // ������ ��ȣ 50 
    //    Rifle, // 51
    //    ShotGun, // 52
    //    Relic = 1000,
    //    Relic2,
    //    Relic3,
    //}

    //string[] itemDataName = Enum.GetNames(typeof(ItemsData));
    // �������� ���� �ϴ� Dictionary
    // ItemData(�̸�,�̹���,Ÿ��,������ ���̵�)
    #endregion
    List<ItemData> itemDatas = new List<ItemData>();
    Dictionary<string, ItemData> _itemDictionary = new Dictionary<string, ItemData>();

    public void Init()
    {
        itemDatas.Add(new ItemData((int)Define.WeaponType.Pistol, "Pistol", Managers.Resources.Load<Sprite>("Prefabs/sh/UI/Texture/Pistol")));
        itemDatas.Add(new ItemData((int)Define.WeaponType.Rifle, "Rifle", Managers.Resources.Load<Sprite>("Prefabs/sh/UI/Texture/Rifle")));
        itemDatas.Add(new ItemData((int)Define.WeaponType.Shotgun, "Shotgun", Managers.Resources.Load<Sprite>("Prefabs/sh/UI/Texture/Shotgun")));
        itemDatas.Add(new ItemData((int)Define.RelicType.ShieldRelic, "ShieldRelic", Managers.Resources.Load<Sprite>("Prefabs/sh/UI/Texture/ShieldRelic")));
        itemDatas.Add(new ItemData((int)Define.RelicType.BonusAttackSpeedRelic, "BonusAttackSpeedRelic", Managers.Resources.Load<Sprite>("Prefabs/sh/UI/Texture/BonusAttackSpeedRelic")));
        itemDatas.Add(new ItemData((int)Define.RelicType.BonusProjectileRelic, "BonusProjectileRelic", Managers.Resources.Load<Sprite>("Prefabs/sh/UI/Texture/BonusProjectileRelic")));
        itemDatas.Add(new ItemData((int)Define.RelicType.BonusResurrectionTimeRelic, "BonusResurrectionTimeRelic", Managers.Resources.Load<Sprite>("Prefabs/sh/UI/Texture/BonusResurrectionTimeRelic")));
        itemDatas.Add(new ItemData((int)Define.RelicType.GuardRelic, "GuardRelic", Managers.Resources.Load<Sprite>("Prefabs/sh/UI/Texture/GuardRelic")));
        itemDatas.Add(new ItemData((int)Define.ItemType.HpPotion, "HpPotion", Managers.Resources.Load<Sprite>("Prefabs/sh/UI/Texture/HpPotion")));
        itemDatas.Add(new ItemData((int)Define.ItemType.MoveSpeedUpPotion, "MoveSpeedUpPotion", Managers.Resources.Load<Sprite>("Prefabs/sh/UI/Texture/MoveSpeedUpPotion")));
        itemDatas.Add(new ItemData((int)Define.ItemType.AttackSpeedUpPotion, "AttackSpeedUpPotion", Managers.Resources.Load<Sprite>("Prefabs/sh/UI/Texture/AttackSpeedUpPotion")));
        itemDatas.Add(new ItemData((int)Define.ItemType.DamageUpPotion, "DamageUpPotion", Managers.Resources.Load<Sprite>("Prefabs/sh/UI/Texture/DamageUpPotion")));

        #region �ּ�ó��
        //itemDataLength = itemDataName.Length;
        //for (int i = 0; i < itemDatas.Count; i++)
        //{

        //}
        #endregion
        foreach (ItemData itemData in itemDatas)
        {
            if (_itemDictionary.TryGetValue(itemData.itemName, out ItemData testData))
            {
                //Debug.Log($"�̹� {itemData.itemName}�� ������");
            }
            else
            {
                _itemDictionary.Add(itemData.itemName, itemData);
            }
        }
    }

    // �ۿ��� ���� �������°� 
    public ItemData GetItemData(string itemName)
    {

        return _itemDictionary[itemName];
    }

}