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
        itemDatas.Add(new ItemData((int)Define.WeaponType.Pistol, "Pistol", Managers.Resources.Load<Sprite>("Prefabs/sh/UI/Texture/Pistol") , "기본 총"));
        itemDatas.Add(new ItemData((int)Define.WeaponType.Rifle, "Rifle", Managers.Resources.Load<Sprite>("Prefabs/sh/UI/Texture/Rifle"), "빠르게 연사가 가능한 소총"));
        itemDatas.Add(new ItemData((int)Define.WeaponType.Shotgun, "Shotgun", Managers.Resources.Load<Sprite>("Prefabs/sh/UI/Texture/Shotgun"), "연사는 느리지만 한발한발이 강한 샷건"));
        itemDatas.Add(new ItemData((int)Define.RelicType.ShieldRelic, "ShieldRelic", Managers.Resources.Load<Sprite>("Prefabs/sh/UI/Texture/ShieldRelic") , "총알 피해를 막아주는 보호막 유물"));
        itemDatas.Add(new ItemData((int)Define.RelicType.BonusAttackSpeedRelic, "BonusAttackSpeedRelic", Managers.Resources.Load<Sprite>("Prefabs/sh/UI/Texture/BonusAttackSpeedRelic"), "추가 공격속도 증가 유물"));
        itemDatas.Add(new ItemData((int)Define.RelicType.BonusProjectileRelic, "BonusProjectileRelic", Managers.Resources.Load<Sprite>("Prefabs/sh/UI/Texture/BonusProjectileRelic"), "추가 투사체 증가 유물"));
        itemDatas.Add(new ItemData((int)Define.RelicType.BonusResurrectionTimeRelic, "BonusResurrectionTimeRelic", Managers.Resources.Load<Sprite>("Prefabs/sh/UI/Texture/BonusResurrectionTimeRelic"), "부활 시간 감소 유물"));
        itemDatas.Add(new ItemData((int)Define.RelicType.GuardRelic, "GuardRelic", Managers.Resources.Load<Sprite>("Prefabs/sh/UI/Texture/GuardRelic"), "총알을 막아주는 오브젝트 유물"));
        itemDatas.Add(new ItemData((int)Define.ItemType.HpPotion, "HpPotion", Managers.Resources.Load<Sprite>("Prefabs/sh/UI/Texture/HpPotion"), "체력 물약"));
        itemDatas.Add(new ItemData((int)Define.ItemType.MoveSpeedUpPotion, "MoveSpeedUpPotion", Managers.Resources.Load<Sprite>("Prefabs/sh/UI/Texture/MoveSpeedUpPotion"), "이동속도 증가 물약"));
        itemDatas.Add(new ItemData((int)Define.ItemType.AttackSpeedUpPotion, "AttackSpeedUpPotion", Managers.Resources.Load<Sprite>("Prefabs/sh/UI/Texture/AttackSpeedUpPotion"), "공격속도 증가 물약"));
        itemDatas.Add(new ItemData((int)Define.ItemType.DamageUpPotion, "DamageUpPotion", Managers.Resources.Load<Sprite>("Prefabs/sh/UI/Texture/DamageUpPotion"), "데미지 증가 물약"));

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