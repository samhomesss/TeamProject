using DG.Tweening;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_RelicInven_Item : UI_Base
{
    enum GameObjects // �ش� ������Ʈ�� ������ �ִ� �͵�
    {
        ItemIcon,
        ItemNameText,
    }
    //public static int BeforeItemID => _beforeChagnedItemID;
    public int SlotItemID
    {
        get { return _slotItemID; }
        set { _slotItemID = value; }
    } // �ش� ������ ������
    public bool IsEmpty
    {
        get { return _isEmpty; }
        set { _isEmpty = value; }
    } // ����ִ°� ����

    public static bool IsChanged
    {
        get { return _isChanged;  }
        set { _isChanged = value; }
    }// _isChanged property

    public GameObject Icon => _icon; // ������ ������

    int _slotItemID; // ���� �� â�� ��� �ִ� ItemID 
    string _name; // �̸� ���� �Ҷ� ���
    bool _isEmpty = true; // ������ ���� Ȯ�� �������� �κ��ȿ� ���°�?
    static bool _isChanged = false; // �������� �ٲٴ� ��Ȳ
    GameObject _icon; // �������� ������
    //GameObject go;// �������� ������� ������
    //static int _beforeChagnedItemID; // ������ �ٲٱ��� ������ ���̵�

    static public event Action<int> OnItemInfoChanged; // �÷����� ������ ������ ����
    //static public event Action OnChangedItem; // �������� �مf���� �ʿ��� �̺�Ʈ

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        Get<GameObject>((int)GameObjects.ItemNameText).GetComponent<Text>().text = _name;

        // �������� ������ ����ϴ°�
        _icon = Get<GameObject>((int)GameObjects.ItemIcon);
        #region �ּ�ó��
        // ���� �������� 2�� �̻� �Ծ������� � �������� �ٲ����� ���ؼ���
        // Todo: ������ Ȯ�� ������ ���ؼ� ���� ����

        //if (ItemInfoName.Count == 2) // �ش� ī��Ʈ�� 2�϶� 
        //{
        //    _icon.BindEvent(ChangeRelic); // ���� �������� �ٲ۴ٰ� ���� click������ �ٲٴ°�
        //}
        #endregion
        // UI_ItemChangePanel.OnChangedItem += ChangeAction;
        _icon.BindEvent(CheckItemInfo, Define.UIEvent.Enter); // ���콺�� ������ �̺�Ʈ
        _icon.BindEvent(DestroyItemInfo, Define.UIEvent.Exit); // ���콺�� �������� �̺�Ʈ

        #region �ּ�ó��
        //DrapDropItem.OnisEmptySlot -= SlotReset;
        //DrapDropItem.OnisEmptySlot += SlotReset;
        // DrapDropItem.OnisEmptySlot -= IsEmptySlot;
        // DrapDropItem.OnisEmptySlot += IsEmptySlot;
        // DrapDropItem.OnisEmptySlot -= DrapItem;
        // DrapDropItem.OnisEmptySlot += DrapItem;
        #endregion
    }

    #region �ּ�ó�� ���� ��� ����
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
        Debug.Log("���Ե����Ͱ� 0�� �Ǿ����ϴ�.");
        //Transform playerTrans = Map.Player.transform;
        //GameObject go = Managers.Resources.Instantiate($"sh/Relic/{Managers.ItemDataBase.GetItemData(_beforeChagnedItemID).itemName}"); // ������ ����
        //go.transform.position = Map.Player.transform.position;
        //_slotItemID = 0;
    }
    #endregion
    // ���� ������ �ٲ۴ٰ� �����Ҷ� ���
    // �Ƹ� ���� UseItem�̾��� ���̴�.

    #region �ּ�ó�� Ŭ���׼��� ���ؼ� ������ �ٲܶ� ����ϴ� �׼� 
    // Action ����� ���� ���
    /*
    public void ChangeAction()
    {
        if (_isChanged == false)
        {
            //_icon.BindEvent(ChangeRelic); // Ŭ�� �׼� 
        }
    }
    */
    #endregion
    #region �ּ�ó�� ������ Ŭ�������� �ٲٴ� ��Ȳ
    /*
    public void ChangeRelic(PointerEventData PointerEventData)
    {
        #region �ּ� ���߿� ����� ��
        // �������� Ŭ���ؼ� �ٲٸ� �ش� �������� �̹����� �гο� �ִ� �̹����� �ٲٰ� 
        // ���� ��� �ִ� �������� �ٲ㼭 ����Ʈ�� �ָ� ��
        #endregion
        if (ItemInfoName.Count == 2 )
        {
            
            _beforeChagnedItemID = _slotItemID;
            // Todo : �����ߵǰ�
            _icon.GetComponent<Image>().sprite = Managers.ItemDataBase.GetItemData(UI_ItemChangePanel.ItemID).itemImage; // �������� �ϴ� ������ �̹����� �ٲ��ְ�
            _slotItemID = UI_ItemChangePanel.ItemID; // �ش� �������� ID�� �޾ƿͼ� 
            Destroy(ItemInfoName.Item);
            GameObject go = Managers.Resources.Instantiate($"sh/Relic/{Managers.ItemDataBase.GetItemData(_beforeChagnedItemID).itemName}"); // ������ ���� 
            //Todo: �������� �÷��̾� ��ġ�� ���� �ؾ� �ϱ� ��
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
    /// �������� �κ��丮�ȿ� ��� ������ ������ â�ȿ� �� �÷����� 
    /// �ش� �������� ���� ����â 
    /// </summary>
    /// <param name="PointerEventData"></param>
    public void CheckItemInfo(PointerEventData PointerEventData) 
    {
        if (_icon.GetComponent<Image>().sprite == null) // �������� ������ Null ��ȯ
            return;
        GameObject Info = Util.FindChild(UI_ItemInfo.ItemInfo, "ItemInfoBackGround", true);
        GameObject InfoImage = Util.FindChild(UI_ItemInfo.ItemInfo, "ItemImage", true);
        GameObject InfoText = Util.FindChild(UI_ItemInfo.ItemInfo, "ItemInfoText", true);
        UI_ItemInfo.ItemInfo.SetActive(true);
        InfoImage.GetComponent<Image>().sprite = Managers.ItemDataBase.GetItemData(_slotItemID).itemImage; // �ѹ� �ʱ�ȭ ���ָ� �Ǵ� ����
        InfoText.GetComponent<Text>().text = Managers.ItemDataBase.GetItemData(_slotItemID).itemName; // �ѹ� �ʱ�ȭ 
        OnItemInfoChanged?.Invoke(_slotItemID); // ���� ������ �������� ������ ���� ����
        Info.transform.position = gameObject.transform.position + new Vector3(-80, 100, 0);

    }
    /// <summary>
    /// �κ��丮 �� �������� �������� �������
    /// </summary>
    /// <param name="PointerEventData"></param>
    public void DestroyItemInfo(PointerEventData PointerEventData)
    {
        UI_ItemInfo.ItemInfo.SetActive(false);
    }

    // �κ��丮�� �����Ҷ� �κ� ���� �̸��� ���� �ϴ°� 
    public void SetInfo(string name)
    {
        _name = name;
    }

}


