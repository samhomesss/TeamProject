using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class ItemInfoName : UI_Scene
{
    public static GameObject Item => _item;
    public static int Count
    {
        get {return _count; }
        set { _count = value; }
    }
    public static float DiffFloat
    {
        get { return _diffFloat; }
        set { _diffFloat = value; }
    }

    GameObject go = null; // �ʱ�ȭ ���� ���ְ� ������ �̸� ������Ʈ
    // GameObject panel = null; // ������ �Ծ����� �� �������� ��� panel
    // ���� �� ���¿��� Ŭ�� �ϸ� ������ �ٲٰ� ���� �־��� �������� ��� ��Ŵ
    string itemName; // ��� ������ �̸�
    Text itemInfoTextUI; // UI���� ��� ������ �ؽ�Ʈ �̸�

    static float _diffFloat;
    float diff; // �Ÿ�
    public static event Action<int> OnItemGet; //������ ȹ�� ������
    public static event Action OnItemNotCloesed; // ������ ������ ������
    #region �̰� �г��� ���� �ٽ� ����
   // public static event Action OnChangedItem; // �������� �ٲܶ� 
    #endregion 
    static int _count;
    
    static GameObject _item;

    private void Start()
    {
        PlayerTestSh.OnItemCheacked += CloseByPlayer; // �̰� �κ� �ٲ� ��ߵ�
        Managers.Input.GetItemEvent += IsClosedItem; // �̰� �κ� �ٲ� ��ߵ� 
    }

    // �ش� �������� ������ �־ Ư�� Ű�� �������� �Դ� �۾� 
    void  IsClosedItem()
    {
        int itemcount = 0;

        if (diff <= 3f)
        {
            switch (gameObject.GetComponent<Item>().ItemID / 500)
            {
                case 0: // ��������
                    break;
                case 1: // �Ϲ� ������
                    break;
                case 2: // ���� ������
                    for (int i = 0; i < UI_RelicInven.UI_RelicInven_Items.Count; i++)
                    {
                        if (!UI_RelicInven.UI_RelicInven_Items[i].IsEmpty)
                        {
                            itemcount++;
                            if (itemcount == 2)
                            {
                                _count = itemcount; // �ش� count�� �׳� ����Ǹ鼭 ���� ���� ���� �ϳ� �� ����
                                _item = gameObject;
                                #region �г��� �����ͼ� �ؾ� �ɵ�?
                                Managers.UI.ShowSceneUI<UI_ItemChangePanel>();
                                UI_ItemChangePanel.OnChangedItem?.Invoke();
                                UI_RelicInven_Item.OnChangedItem += DestroyAction;

                                #endregion
                            }
                            // ������ ��á���� �ٲܲ��� �˾�â ����� �����ؼ� ���� 
                            // �Ծ��� �ƴϸ� ���� ���� ������ ������
                            continue;
                        }
                        OnItemGet?.Invoke(gameObject.GetComponent<Item>().ItemID);
                        Destroy(gameObject);
                        Destroy(go);
                        PlayerTestSh.OnItemCheacked -= CloseByPlayer; // �÷��̾� ��ó�� ������ ���°� 
                        Managers.Input.GetItemEvent -= IsClosedItem; // ������ �Դ°� 
                        break;
                    }
                    break;
               
            }

           
        }
    }

    void DestroyAction()
    {
        PlayerTestSh.OnItemCheacked -= CloseByPlayer; // �÷��̾� ��ó�� ������ ���°� 
        Managers.Input.GetItemEvent -= IsClosedItem; // ������ �Դ°�
        Destroy(go);
    }

    // ������ �� ������ �־ �������� �Դ� �۾�
    void CloseByPlayer()
    {
        diff = Vector3.Distance(Map.Player.transform.position, gameObject.transform.position);
        _diffFloat = diff;
        if (diff <= 3f)
        {
            if (go != null )
            {
                return;
            }
            else
            {
                // Todo: �ش� ������Ʈ ���� ���°� ���� �ؾߵ�
                go = Managers.Resources.Instantiate($"sh/UI/Scene/UI_ItemName"); // ������ ���� 
                Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, gameObject.transform.position); // ���⼭ ����� �����°� �ƴϰ� 
                RectTransform mainCanvasRect = go.GetComponent<RectTransform>();
                if (RectTransformUtility.ScreenPointToLocalPointInRectangle(mainCanvasRect, screenPoint, null, out Vector2 localPoint)) // �ش� ĵ�������� � ��ġ�� �ִ��� ã�ƾߵ�
                {
                    for (int ix = 0; ix < go.transform.childCount; ++ix)
                    {
                        var child = go.transform.GetChild(ix);
                        if (child.name.Equals("Item"))
                        {
                            child.GetComponent<RectTransform>().anchoredPosition = localPoint + new Vector2(0f, 80f);
                            break;
                        }
                    }
                }

                itemInfoTextUI = go.transform.GetChild(0).GetChild(1).GetComponent<Text>();
                itemName = Managers.ItemDataBase.GetItemData(gameObject.GetComponent<Item>().ItemID).itemName.ToString();

                itemInfoTextUI.text = itemName; // �������� �̸� ���� 

              
            }
        }
        else
        {
            if (go != null)
            {
                if (UI_ItemChangePanel.ItemChagnePanel != null)
                {
                    Destroy(UI_ItemChangePanel.ItemChagnePanel);
                    Count = 0;
                    UI_RelicInven_Item.IsChanged = false;

                }
                Destroy(go);
            }
        }

    }
}
