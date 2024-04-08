using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class ItemInfoName : UI_Scene
{
    GameObject go = null; // 초기화 까지 해주고
    string itemName; // 띄울 아이템 이름
    Text itemInfoTextUI; // UI에서 띄울 아이템 텍스트 이름

    float diff; // 거리
    public static event Action<int> OnItemGet; //아이템 획득 했을때

    private void Start()
    {
        PlayerTestSh.OnItemCheacked += CloseByPlayer; // 이거 부분 바꿔 줘야됨
        Managers.Input.GetItemEvent += IsClosedItem; // 이거 부분 바꿔 줘야됨 
    }

    // 해당 아이템이 가까이 있어서 특정 키로 아이템을 먹는 작업 
    void IsClosedItem()
    {
        if (diff <= 3f)
        {
            OnItemGet?.Invoke(gameObject.GetComponent<Item>().ItemID); // 해당 아이템의 아이템 정보를 전달 해주고
            Destroy(gameObject); // 파괴
            Destroy(go);
            PlayerTestSh.OnItemCheacked -= CloseByPlayer;
            Managers.Input.GetItemEvent -= IsClosedItem;
        }
    }

    // 아이템 이 가까이 있어서 아이템을 먹는 작업
    void CloseByPlayer()
    {
        diff = Vector3.Distance(Map.Player.transform.position, gameObject.transform.position);

        if (diff <= 3f)
        {
            if (go != null )
            {
                return;
            }
            else
            {
                // Todo: 해당 오브젝트 위에 띄우는거 공부 해야됨
                go = Managers.Resources.Instantiate($"sh/UI/Scene/UI_ItemName"); // 아이템 생성 
                Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, gameObject.transform.position); // 여기서 계산이 끝나는게 아니고 
                RectTransform mainCanvasRect = go.GetComponent<RectTransform>();
                if (RectTransformUtility.ScreenPointToLocalPointInRectangle(mainCanvasRect, screenPoint, null, out Vector2 localPoint)) // 해당 캔버스에서 어떤 위치에 있는지 찾아야됨
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

                itemInfoTextUI.text = itemName; // 아이템의 이름 적용 

              
            }
        }
        else
        {
            if (go != null)
            {
                Destroy(go);
            }
        }

    }
}
