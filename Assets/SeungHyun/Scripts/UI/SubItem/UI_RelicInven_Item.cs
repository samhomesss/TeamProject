using DG.Tweening;
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
    //public static int BeforeItemID => _beforeChagnedItemID;
    public int SlotItemID
    {
        get { return _slotItemID; }
        set { _slotItemID = value; }
    } // 해당 슬롯의 아이템
    public bool IsEmpty
    {
        get { return _isEmpty; }
        set { _isEmpty = value; }
    } // 비어있는가 판정

    public static bool IsChanged
    {
        get { return _isChanged;  }
        set { _isChanged = value; }
    }// _isChanged property

    public GameObject Icon => _icon; // 아이템 아이콘

    int _slotItemID; // 현재 이 창이 들고 있는 ItemID 
    string _name; // 이름 설정 할때 사용
    bool _isEmpty = true; // 아이템 유무 확인 아이템이 인벤안에 없는가?
    static bool _isChanged = false; // 아이템을 바꾸는 상황
    GameObject _icon; // 아이템의 아이콘
    //GameObject go;// 떨어질때 만들어질 아이템
    //static int _beforeChagnedItemID; // 아이템 바꾸기전 아이템 아이디

    static public event Action<int> OnItemInfoChanged; // 올렸을때 아이템 인포를 조정
    //static public event Action OnChangedItem; // 아이템을 바꿧을때 필요한 이벤트

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
        #region 주석처리
        // 유물 아이템을 2개 이상 먹었을때만 어떤 아이템을 바꿀지에 대해서만
        // Todo: 아이템 확인 판정에 대해서 따로 관리

        //if (ItemInfoName.Count == 2) // 해당 카운트가 2일때 
        //{
        //    _icon.BindEvent(ChangeRelic); // 유물 아이템을 바꾼다고 생각 click했을때 바꾸는것
        //}
        #endregion
        // UI_ItemChangePanel.OnChangedItem += ChangeAction;
        _icon.BindEvent(CheckItemInfo, Define.UIEvent.Enter); // 마우스가 들어갔을때 이벤트
        _icon.BindEvent(DestroyItemInfo, Define.UIEvent.Exit); // 마우스가 나갔을때 이벤트

        #region 주석처리
        //DrapDropItem.OnisEmptySlot -= SlotReset;
        //DrapDropItem.OnisEmptySlot += SlotReset;
        // DrapDropItem.OnisEmptySlot -= IsEmptySlot;
        // DrapDropItem.OnisEmptySlot += IsEmptySlot;
        // DrapDropItem.OnisEmptySlot -= DrapItem;
        // DrapDropItem.OnisEmptySlot += DrapItem;
        #endregion
    }

    #region 주석처리 현재 사용 안함
    void SlotReset()
    {
        _slotItemID = 0;
    }

    void ImageUpdate()
    {
        if (_icon.GetComponent<Image>().sprite != null) 
        {
            //_slotItemID = 
        }
    }

    void IsEmptySlot()
    {
        //if (_slotItemID == 0)
        //    return;
        _isEmpty = true;
    }

    void DrapItem()
    {
        if (_slotItemID == 0)
            return;
        Debug.Log("슬롯데이터가 0이 되었습니다.");
        //Transform playerTrans = Map.Player.transform;
        //GameObject go = Managers.Resources.Instantiate($"sh/Relic/{Managers.ItemDataBase.GetItemData(_beforeChagnedItemID).itemName}"); // 아이템 생성
        //go.transform.position = Map.Player.transform.position;
        //_slotItemID = 0;
    }
    #endregion
    // 유물 아이템 바꾼다고 선택할때 사용
    // 아마 원래 UseItem이었을 것이다.

    #region 주석처리 클릭액션을 통해서 유물을 바꿀때 사용하는 액션 
    // Action 사용을 위한 방식
    /*
    public void ChangeAction()
    {
        if (_isChanged == false)
        {
            //_icon.BindEvent(ChangeRelic); // 클릭 액션 
        }
    }
    */
    #endregion
    #region 주석처리 유물을 클릭했을때 바꾸는 상황
    /*
    public void ChangeRelic(PointerEventData PointerEventData)
    {
        #region 주석 나중에 지우면 됨
        // 아이템을 클릭해서 바꾸면 해당 아이템의 이미지를 패널에 있는 이미지로 바꾸고 
        // 현재 들고 있는 아이템을 바꿔서 떨어트려 주면 됨
        #endregion
        if (ItemInfoName.Count == 2 )
        {
            
            _beforeChagnedItemID = _slotItemID;
            // Todo : 지워야되고
            _icon.GetComponent<Image>().sprite = Managers.ItemDataBase.GetItemData(UI_ItemChangePanel.ItemID).itemImage; // 먹으려고 하는 아이템 이미지로 바꿔주고
            _slotItemID = UI_ItemChangePanel.ItemID; // 해당 아이템의 ID를 받아와서 
            Destroy(ItemInfoName.Item);
            GameObject go = Managers.Resources.Instantiate($"sh/Relic/{Managers.ItemDataBase.GetItemData(_beforeChagnedItemID).itemName}"); // 아이템 생성 
            //Todo: 윤범이형 플레이어 위치로 수정 해야 하긴 함
            go.transform.position = Map.Player.transform.position;
            Destroy(UI_ItemChangePanel.ItemChagnePanel);
            ItemInfoName.Count = 0;
            Debug.Log(ItemInfoName.Count);
            OnChangedItem?.Invoke();

            
        }
        
    }
    */
    #endregion

    
    /// <summary>
    /// 아이템이 인벤토리안에 들어 왔을때 아이템 창안에 손 올렸을때 
    /// 해당 아이템을 띄우는 정보창 
    /// </summary>
    /// <param name="PointerEventData"></param>
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
    /// <summary>
    /// 인벤토리 안 아이템의 범위에서 벗어났을때
    /// </summary>
    /// <param name="PointerEventData"></param>
    public void DestroyItemInfo(PointerEventData PointerEventData)
    {
        UI_ItemInfo.ItemInfo.SetActive(false);
    }

    // 인벤토리를 생성할때 인벤 위에 이름을 설정 하는거 
    public void SetInfo(string name)
    {
        _name = name;
    }

}


