﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using yb;

public class UI_RelicInven : UI_Scene
{
    //ItemInfoName itemInfoName;'
    Map map;
    public List<UI_RelicInven_Item> UI_RelicInven_Items => ui_RelicInven_Items; // 해당 리스트를 가져오는 프로퍼티
    //public static GameObject UI_RelicInvens => ui_RelicInven;
    List<UI_RelicInven_Item> ui_RelicInven_Items = new List<UI_RelicInven_Item>();
    //static GameObject ui_RelicInven; // 유아이 렐릭 인벤 
    UI_RelicInven_Item relicInvenItem;
    GameObject[] RelicImage = new GameObject[2]; // 유물 아이템 이미지 저장
    enum GameObjects
    {
        GridPanel
    }

    void Start()
    {
        //ui_RelicInven = gameObject;
        map = Map.MapObject.GetComponent<Map>();
        //itemInfoName = ItemInfoName.ItemNameObject.GetComponent<ItemInfoName>();
        Init();
        SetPlayer(map.Player);
        #region 지금 사용안함
        // 구독해주고
        //UI_ItemCreateButton.OnItemCreateClicked += ChangeImage;
        //itemInfoName.OnRelicGet += ChangeImage;
        //ItemInfoName.OnRelicGet -= ChangeImage;
        //ItemInfoName.OnRelicGet += ChangeImage;
        #endregion
    }
    #region 현재 사용 안함 지워야함
    // 테스트용 지워야함
    //private void Update()
    //{
    //    for (int i = 0; i < ui_RelicInven_Items.Count; i++)
    //    {
    //        Debug.Log(i+"번째 인벤창이 가지고 있는 아이템ID는" + UI_RelicInven_Items[i].SlotItemID);
    //        Debug.Log(i + "번째 인벤창이 비어있는가?" + UI_RelicInven_Items[i].IsEmpty);
    //    }
    //}
    #endregion

    

    void SetPlayer(PlayerController player)
    {
        player.SetRelicEvent += ChangeImage;
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        GameObject gridPanel = Get<GameObject>((int)GameObjects.GridPanel);
        foreach (Transform child in gridPanel.transform)
            Managers.Resources.Destroy(child.gameObject);

        // 실제 인벤토리 정보를 참고해서
        for (int i = 0; i < 2; i++)
        {
            GameObject item = Managers.UI.MakeSubItem<UI_RelicInven_Item>(gridPanel.transform).gameObject;
            relicInvenItem = item.GetOrAddComponent<UI_RelicInven_Item>(); // 해당 컴포넌트 추가해서 
            // 리스트에 추가
            ui_RelicInven_Items.Add(relicInvenItem);
            relicInvenItem.SetInfo($"유물{i}번");
        }
    }

    // 번호를 넘겨주는 아이템 번호로 사용
    /// <summary>
    /// 아이템의 번호 가 들어왓을때 
    /// </summary>
    /// <param name="itemID"></param>
    public void ChangeImage(string itemID) // 유물 인텝토리에 아이템이 들어 왔을때 아이템 이미지 바꿔주는거
    {
        Debug.Log("함수 호출 체크");
        for (int i = 0; i < ui_RelicInven_Items.Count; i++)
        {
            if (!ui_RelicInven_Items[i].IsEmpty) // 유물창이 비었는지 
            {
                continue; // 비어있지 않으면 그냥 다음꺼로 진행
            }
            // 시작점 문제인가?
            
            RelicImage[i] = Util.FindChild(ui_RelicInven_Items[i].gameObject, "ItemIcon", true);
            ui_RelicInven_Items[i].IsEmpty = false;
            RelicImage[i].GetComponent<Image>().sprite = Managers.ItemDataBase.GetItemData(itemID).itemImage;
            ui_RelicInven_Items[i].SlotItemID = Managers.ItemDataBase.GetItemData(itemID).itemName; 
            break;

        }
    }
}


