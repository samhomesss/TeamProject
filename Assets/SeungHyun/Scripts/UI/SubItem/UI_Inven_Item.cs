using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


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

    public int SlotItemID
    { 
        get { return _slotItemID; } 
        set { _slotItemID = value; }
    }  

    string _name;
    bool _isEmpty = true; // 아이템 유무 확인 
    GameObject _icon; // 아이템의 아이콘
    GameObject go = null;
    int _slotItemID; // 현재 이 창이 들고 있는 ItemID 

    static public event Action<int> OnItemInfoChanged; // 올렸을때 아이템 인포를 조정

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        Get<GameObject>((int)GameObjects.ItemNameText).GetComponent<Text>().text = _name;

        // 아이콘을 가지고 사용하는거
        _icon = Get<GameObject>((int)GameObjects.ItemIcon);
        _icon.BindEvent(UseItem);
        _icon.BindEvent(CheckItemInfo , Define.UIEvent.Enter); // 들어올때 사용할 이벤트 Drag 하나 더 생성 해야 함
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
        _icon.GetComponent<Image>().sprite = Managers.ItemDataBase.GetItemData(0).itemImage;
        _slotItemID = 0;
        Destroy(go);
    }

    #region ItemInfo
    // 아이템 위에 손 올리면 
    public void CheckItemInfo(PointerEventData PointerEventData)
    {
        if (_icon.GetComponent<Image>().sprite == null) // 아이템이 없으면 Null 반환
            return;
        Debug.Log("마우스 커서가 아이템 창 위에 있음");
        go = Managers.Resources.Instantiate($"sh/UI/Scene/UI_ItemInfo"); // 창 생성
        
        OnItemInfoChanged?.Invoke(_slotItemID); // 여기 슬롯의 아이템의 정보를 전달 해줌

        go.transform.GetChild(0).transform.position = gameObject.transform.position + new Vector3(-80 , 100 , 0);
    }
    // 아이템 위에서 손을 땠을때 사용
    public void DestroyItemInfo(PointerEventData PointerEventData)
    {
        //GameObject go2 = new GameObject();
        //go2.name = ("Test");
        //go2.AddComponent<UI_Inven_Item>();
        Destroy(go);
    }
    #endregion

    // 아이템 정보
    public void SetInfo(string name)
    {
        _name = name;
    }
    
}


