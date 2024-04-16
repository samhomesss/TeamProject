using DG.Tweening;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;


public class UI_Inven_Item : UI_Base 
{
    enum GameObjects
    {
        ItemIcon,
        ItemNameText,
    }
    public GameObject Icon => _icon; // 아이템 아이콘

    public bool IsEmpty
    {
        get { return _isEmpty; }
        set { _isEmpty = value;  }
    }

    public string SlotItemID
    { 
        get { return _slotItemID; } 
        set { _slotItemID = value; }
    }  

    //public static GameObject ItemInfo
    //{
    //    get { return go; }
    //    set { go = value; } 
    //}

    string _name;
    bool _isEmpty = true; // 아이템 유무 확인 
    GameObject _icon; // 아이템의 아이콘
    //static GameObject go = null; // 해당 아이템을 가져오고
    string _slotItemID; // 현재 이 창이 들고 있는 ItemID 

    //GameObject ui_invenItem;
    //public event Action<string> OnItemInfoChanged; // 올렸을때 아이템 인포를 조정

    void Start()
    {
       // ui_invenItem = gameObject;
        Init();
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        Get<GameObject>((int)GameObjects.ItemNameText).GetComponent<Text>().text = _name;

        // 아이콘을 가지고 사용하는거
        _icon = Get<GameObject>((int)GameObjects.ItemIcon);
        _icon.BindEvent(UseItem);
        _icon.BindEvent(CheckItemInfo, Define.UIEvent.Enter); // 들어올때 사용할 이벤트 Drag 하나 더 생성 해야 함
        _icon.BindEvent(DestroyItemInfo, Define.UIEvent.Exit);
    }

    // 아이템 사용한다고 생각할때 사용
    public void UseItem(PointerEventData PointerEventData)
    {
        // 해당 칸의 아이템 창의 번호에 따른 효과 발생 -> ItemID로 적용 하면됨
        // 이미지를 초기화 해주는 쪽
        Debug.Log($"아이템 클릭! {_name}");
        Debug.Log(_icon.name);
        _isEmpty = true;
        _icon.GetComponent<Image>().sprite = default;
        _slotItemID = default;
        UI_ItemInfo.ItemInfo.SetActive(false);
    }

    #region ItemInfo
    // 아이템 위에 손 올리면 
    public void CheckItemInfo(PointerEventData PointerEventData)
    {
        if (_icon.GetComponent<Image>().sprite == null)//Managers.Resources.Load<Sprite>("Prefabs/sh/UI/Texture/DefaultItemImage")) // 아이템이 없으면 Null 반환
            return;

        GameObject Info = Util.FindChild(UI_ItemInfo.ItemInfo, "ItemInfoBackGround", true);
        GameObject InfoImage = Util.FindChild(UI_ItemInfo.ItemInfo, "ItemImage", true);
        GameObject InfoText = Util.FindChild(UI_ItemInfo.ItemInfo, "ItemInfoText", true);

        UI_ItemInfo.ItemInfo.SetActive(true);
        InfoImage.GetComponent<Image>().sprite = Managers.ItemDataBase.GetItemData(_slotItemID).itemImage; // 한번 초기화 해주면 되는 이유
        InfoText.GetComponent<Text>().text = Managers.ItemDataBase.GetItemData(_slotItemID).itemName; // 한번 초기화 
        //OnItemInfoChanged?.Invoke(_slotItemID); // 여기 슬롯의 아이템의 정보를 전달 해줌
        Info.transform.position = gameObject.transform.position + new Vector3(-80, 100, 0);
        #region 위에로 따로 처리
        //UI_ItemInfo.ItemInfo.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = Managers.ItemDataBase.GetItemData(_slotItemID).itemImage; // 한번 초기화 해주면 되는 이유
        //UI_ItemInfo.ItemInfo.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = Managers.ItemDataBase.GetItemData(_slotItemID).itemName; // 한번 초기화 
        //OnItemInfoChanged?.Invoke(_slotItemID); // 여기 슬롯의 아이템의 정보를 전달 해줌
        //UI_ItemInfo.ItemInfo.transform.GetChild(0).transform.position = gameObject.transform.position + new Vector3(-80, 100, 0);
        #endregion
    }
    // 아이템 위에서 손을 땠을때 사용
    public void DestroyItemInfo(PointerEventData PointerEventData)
    {
        UI_ItemInfo.ItemInfo.SetActive(false);
    }
    #endregion

    // 아이템 정보
    public void SetInfo(string name)
    {
        _name = name;
    }
    
}


