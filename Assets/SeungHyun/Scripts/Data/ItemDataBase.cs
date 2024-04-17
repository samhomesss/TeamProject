using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;


// 아이템을 저장할 데이터 베이스 
#region 아이템 데이터 베이스 사용 방법
// enum 안에 데이터 베이스 안에 넣을 
// 스크립터블오브젝트들의 이름을 그대로 가져오고 
// itemData의 리스트 안에 
// 경로로 가져온 스크립터블오브젝트들을 가져옴
// 그리고 딕션어리에 추가
#endregion

public class ItemDataBase
{
    #region 주석처리
    //public enum ItemsData
    //{
    //    Default = 0,
    //    Pistol = 50, // 아이템 번호 50 
    //    Rifle, // 51
    //    ShotGun, // 52
    //    Relic = 1000,
    //    Relic2,
    //    Relic3,
    //}

    //string[] itemDataName = Enum.GetNames(typeof(ItemsData));
    // 아이템을 관리 하는 Dictionary
    // ItemData(이름,이미지,타입,아이템 아이디)
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

        #region 주석처리
        //itemDataLength = itemDataName.Length;
        //for (int i = 0; i < itemDatas.Count; i++)
        //{

        //}
        #endregion
        foreach (ItemData itemData in itemDatas)
        {
            if (_itemDictionary.TryGetValue(itemData.itemName, out ItemData testData))
            {
                //Debug.Log($"이미 {itemData.itemName}가 존재함");
            }
            else
            {
                _itemDictionary.Add(itemData.itemName, itemData);
            }
        }
    }

    // 밖에서 정보 가져오는거 
    public ItemData GetItemData(string itemName)
    {

        return _itemDictionary[itemName];
    }

}