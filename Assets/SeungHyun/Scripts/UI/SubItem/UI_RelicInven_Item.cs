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
    public GameObject Icon => _icon; // ������ ������

    public bool IsEmpty
    {
        get { return _isEmpty; }
        set { _isEmpty = value; }
    } // ����ִ°� ����

    public int SlotItemID
    {
        get { return _slotItemID; }
        set { _slotItemID = value; }
    } // �ش� ������ ������

    string _name;
    bool _isEmpty = true; // ������ ���� Ȯ�� 
    bool _isChanged = false; // �������� �ٲٴ� ��Ȳ
    GameObject _icon; // �������� ������
    int _slotItemID; // ���� �� â�� ��� �ִ� ItemID 

    static public event Action<int> OnItemInfoChanged; // �÷����� ������ ������ ����

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
        // ���� �������� 2�� �̻� �Ծ������� � �������� �ٲ����� ���ؼ���
        if (_isChanged)
        {
            _icon.BindEvent(ChangeRelic);
        }
        _icon.BindEvent(CheckItemInfo, Define.UIEvent.Enter); 
        _icon.BindEvent(DestroyItemInfo, Define.UIEvent.Exit);
    }

    // ���� ������ �ٲ۴ٰ� �����Ҷ� ���
    public void ChangeRelic(PointerEventData PointerEventData)
    {
        _icon.GetComponent<Image>().sprite = Managers.ItemDataBase.GetItemData(0).itemImage; // �̹����� �޾ƿ;��ҵ�?
        _slotItemID = 0; // �ش� �������� ID�� �޾ƿ;���
        UI_ItemInfo.ItemInfo.SetActive(false); // ����â�� ������Ʈ ����� �ҵ�?
    }

    #region ItemInfo
    // ������ ���� �� �ø��� 
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
    // ������ ������ ���� ������ ���
    public void DestroyItemInfo(PointerEventData PointerEventData)
    {
        UI_ItemInfo.ItemInfo.SetActive(false);
    }
    #endregion

    // ������ ����
    public void SetInfo(string name)
    {
        _name = name;
    }

}


