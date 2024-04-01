using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace Sh
{
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
        public enum ItemsData
        {
            Default = 0,
            Pistol = 50, // 아이템 번호 50 
            Rifle, // 51
            ShotGun, // 52
        }

        string[] itemDataName = Enum.GetNames(typeof(ItemsData));
        int itemDataLength;

        // 아이템을 관리 하는 Dictionary
        List<ItemData> itemDatas = new List<ItemData>();
        Dictionary<int, ItemData> _itemDictionary = new Dictionary<int, ItemData>();

       

        public void Init()
        {
            itemDataLength = itemDataName.Length;
            for (int i = 0; i < itemDataLength; i++)
            {
                var path = $"Prefabs/sh/Data/{itemDataName[i]}";

                itemDatas.Add(Managers.Resource.Load<ItemData>(path));
            }

            foreach(ItemData itemData in itemDatas)
            {
                _itemDictionary.Add(itemData.itemID, itemData);
            }

            Debug.Log(_itemDictionary[50].name);

        }

        // 밖에서 정보 가져오는거 
        public ItemData GetItemData(int itemID)
        {
            
            return _itemDictionary[itemID];
        }

    }
}

