using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class ItemInfoName : UI_Scene
{
    public static GameObject Item => _item; // 아이템 패널용 아이템 전달 방식
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

    GameObject itemNameObject = null; // 초기화 까지 해주고 아이템 이름 오브젝트
    // GameObject panel = null; // 아이템 먹었을때 꽉 차있으면 물어볼 panel
    // 이후 저 상태에서 클릭 하면 아이템 바꾸고 원래 있었던 아이템을 드롭 시킴
    string itemName; // 띄울 아이템 이름
    Text itemInfoTextUI; // UI에서 띄울 아이템 텍스트 이름

    static float _diffFloat;
    float diff; // 거리
    public static event Action<int> OnRelicGet; // 유물 아이템 획득 했을때
    public static event Action<int> OnWeaponGet;// 무기 아이템 획득 했을때
    public static event Action<int> OnitemGet; // 일반 아이템 먹었을 때
    public static event Action OnItemNotCloesed; // 아이템 가까이 없을때
    #region 이건 패널쪽 만들어서 다시 생성
   // public static event Action OnChangedItem; // 아이템을 바꿀때 
    #endregion 
    static int _count;
    
    static GameObject _item; // 아이템 패널용 아이템 전달 방식

    private void Start()
    {
        PlayerTestSh.OnItemCheacked += CloseByPlayer; // 이거 부분 바꿔 줘야됨
        Managers.Input.GetItemEvent += IsClosedItem; // 이거 부분 바꿔 줘야됨 
    }

    // 아이템이 가까이 있고 아이템을 판단
    // 해당 아이템이 가까이 있어서 특정 키로 아이템을 먹는 작업 
    void  IsClosedItem()
    {
        int itemcount = 0;

        if (diff <= 3f)
        { 
            // 사용하다 보니 같은 부분들이 있어 이거 빼주면 될듯?
            switch (gameObject.GetComponent<Item>().ItemID / 500)
            {
                case 0: // 장비아이템(총)
                    OnWeaponGet?.Invoke(gameObject.GetComponent<Item>().ItemID);
                    Destroy(gameObject);
                    Destroy(itemNameObject);
                    PlayerTestSh.OnItemCheacked -= CloseByPlayer; // 플레이어 근처에 아이템 띄우는거 
                    Managers.Input.GetItemEvent -= IsClosedItem; // 아이템 먹는거 
                    break;
                case 1: // 일반 아이템
                    OnitemGet?.Invoke(gameObject.GetComponent<Item>().ItemID);
                    Destroy(gameObject);
                    Destroy(itemNameObject);
                    PlayerTestSh.OnItemCheacked -= CloseByPlayer; // 플레이어 근처에 아이템 띄우는거 
                    Managers.Input.GetItemEvent -= IsClosedItem; // 아이템 먹는거 
                    break;
                case 2: // 유물 아이템
                    for (int i = 0; i < UI_RelicInven.UI_RelicInven_Items.Count; i++)
                    {
                        if (!UI_RelicInven.UI_RelicInven_Items[i].IsEmpty)
                        {
                            #region 패널을 이용해서 아이템의 상태를 띄우는 창
                            //itemcount++;
                            // if (itemcount == 2)
                            // {
                            // _count = itemcount; // 해당 count가 그냥 진행되면서 빠져 버려 변수 하나 더 만듬
                            // _item = gameObject;
                            #region 패널을 가져와서 해야 될듯?
                            // Managers.UI.ShowSceneUI<UI_ItemChangePanel>();
                            // UI_ItemChangePanel.OnChangedItem?.Invoke();
                            // UI_RelicInven_Item.OnChangedItem += DestroyAction;

                            #endregion
                            //}
                            // 아이템 꽉찼으면 바꿀껀지 팝업창 만들고 선택해서 띄우기 
                            // 먹었던 아니면 먹지 않은 아이템 떨구기
                            #endregion
                            continue;
                        }
                        OnRelicGet?.Invoke(gameObject.GetComponent<Item>().ItemID);
                        Destroy(gameObject);
                        Destroy(itemNameObject);
                        PlayerTestSh.OnItemCheacked -= CloseByPlayer; // 플레이어 근처에 아이템 띄우는거 
                        Managers.Input.GetItemEvent -= IsClosedItem; // 아이템 먹는거 
                        break;
                    }
                    break;
               
            }

           
        }
    }

    void DestroyAction()
    {
        PlayerTestSh.OnItemCheacked -= CloseByPlayer; // 플레이어 근처에 아이템 띄우는거 
        Managers.Input.GetItemEvent -= IsClosedItem; // 아이템 먹는거
        Destroy(itemNameObject);
    }

    // 아이템 이 가까이 있어서 아이템을 먹는 작업
    void CloseByPlayer()
    {
        diff = Vector3.Distance(Map.Player.transform.position, gameObject.transform.position);
        _diffFloat = diff;
        if (diff <= 3f)
        {
            if (itemNameObject != null )
            {
                return;
            }
            else
            {
                // Todo: 해당 오브젝트 위에 띄우는거 공부 해야됨
                itemNameObject = Managers.Resources.Instantiate($"sh/UI/Scene/UI_ItemName"); // 아이템 생성 
                Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, gameObject.transform.position); // 여기서 계산이 끝나는게 아니고 
                RectTransform mainCanvasRect = itemNameObject.GetComponent<RectTransform>();
                if (RectTransformUtility.ScreenPointToLocalPointInRectangle(mainCanvasRect, screenPoint, null, out Vector2 localPoint)) // 해당 캔버스에서 어떤 위치에 있는지 찾아야됨
                {
                    for (int ix = 0; ix < itemNameObject.transform.childCount; ++ix)
                    {
                        var child = itemNameObject.transform.GetChild(ix);
                        if (child.name.Equals("Item"))
                        {
                            child.GetComponent<RectTransform>().anchoredPosition = localPoint + new Vector2(0f, 80f);
                            break;
                        }
                    }
                }

                itemInfoTextUI = itemNameObject.transform.GetChild(0).GetChild(1).GetComponent<Text>();
                itemName = Managers.ItemDataBase.GetItemData(gameObject.GetComponent<Item>().ItemID).itemName.ToString();

                itemInfoTextUI.text = itemName; // 아이템의 이름 적용 

              
            }
        }
        else
        {
            if (itemNameObject != null)
            {
                if (UI_ItemChangePanel.ItemChagnePanel != null)
                {
                    Destroy(UI_ItemChangePanel.ItemChagnePanel);
                    Count = 0;
                    UI_RelicInven_Item.IsChanged = false;

                }
                Destroy(itemNameObject);
            }
        }

    }
}
