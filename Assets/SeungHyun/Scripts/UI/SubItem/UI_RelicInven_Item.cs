using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_RelicInven_Item : UI_Base
{
    enum GameObjects // 해당 오브젝트가 가지고 있는 것들
    {
        ItemIcon,
        ItemNameText,
    }
    public GameObject Icon => _icon; // 아이템 아이콘

    public bool IsEmpty
    {
        get { return _isEmpty; }
        set { _isEmpty = value; }
    } // 비어있는가 판정

    public int SlotItemID
    {
        get { return _slotItemID; }
        set { _slotItemID = value; }
    } // 해당 슬롯의 아이템

    string _name;
    bool _isEmpty = true; // 아이템 유무 확인 
    bool _isChanged = false; // 아이템을 바꾸는 상황
    GameObject _icon; // 아이템의 아이콘
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
        // 유물 아이템을 2개 이상 먹었을때만 어떤 아이템을 바꿀지에 대해서만
        if (_isChanged)
        {
            _icon.BindEvent(ChangeRelic);
        }
        _icon.BindEvent(CheckItemInfo, Define.UIEvent.Enter); 
        _icon.BindEvent(DestroyItemInfo, Define.UIEvent.Exit);
    }

    // 유물 아이템 바꾼다고 선택할때 사용
    public void ChangeRelic(PointerEventData PointerEventData)
    {
        _icon.GetComponent<Image>().sprite = Managers.ItemDataBase.GetItemData(0).itemImage; // 이미지는 받아와야할듯?
        _slotItemID = 0; // 해당 아이템의 ID를 받아와야함
        UI_ItemInfo.ItemInfo.SetActive(false); // 정보창을 업데이트 해줘야 할듯?
    }

    #region ItemInfo
    // 아이템 위에 손 올리면 
    public void CheckItemInfo(PointerEventData PointerEventData)
    {
        if (_icon.GetComponent<Image>().sprite == null) // 아이템이 없으면 Null 반환
            return;
        GameObject Info = Util.FindChild(UI_ItemInfo.ItemInfo, "ItemInfoBackGround", true);
        GameObject InfoImage = Util.FindChild(UI_ItemInfo.ItemInfo, "ItemImage", true);
        GameObject InfoText = Util.FindChild(UI_ItemInfo.ItemInfo, "ItemInfoText", true);

        UI_ItemInfo.ItemInfo.SetActive(true);
        InfoImage.GetComponent<Image>().sprite = Managers.ItemDataBase.GetItemData(_slotItemID).itemImage; // 한번 초기화 해주면 되는 이유
        InfoText.GetComponent<Text>().text = Managers.ItemDataBase.GetItemData(_slotItemID).itemName; // 한번 초기화 
        OnItemInfoChanged?.Invoke(_slotItemID); // 여기 슬롯의 아이템의 정보를 전달 해줌
        Info.transform.position = gameObject.transform.position + new Vector3(-80, 100, 0);

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


