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
                         // 해당 아이템을 먹었을때 창 띄우게 설정 하면 될듯?
    float diff; // 거리
    public static event Action<int> OnItemGet; //아이템 획득 했을때

    // 플레이어 이동 액션에 달아 주면 될듯?
    private void Start()
    {
        PlayerTestSh.OnItemCheacked += CloseByPlayer;
    }

    private void Update()
    {
        // Todo : 이부분은 나중에 윤범이 형 꺼 가지고 와서 Update에서 제거하고 Action으로 대체
        if (Input.GetKeyDown(KeyCode.F)) // 키 눌렀을때
        {
            if (diff <= 3f)
            {
                OnItemGet?.Invoke(gameObject.GetComponent<Item>().ItemID); // 해당 아이템의 아이템 정보를 전달 해주고
                Destroy(gameObject); // 파괴
                Destroy(go);
                PlayerTestSh.OnItemCheacked -= CloseByPlayer;

            }

        }
    }

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
                //go.GetComponent<RectTransform>().anchoredPosition = screenPoint;
                

                //Debug.Log($"{gameObject.transform.position} | {screenPoint}");

                itemInfoTextUI = go.transform.GetChild(0).GetChild(1).GetComponent<Text>();
                itemName = Managers.ItemDataBase.GetItemData(gameObject.GetComponent<Item>().ItemID).itemName.ToString();

                itemInfoTextUI.text = itemName; // 아이템의 이름 적용 
                //go.GetComponent<Canvas>().worldCamera = Camera.main;
                //go.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 20);

              
            }
        }
        else
        {
            if (go != null)
            {
                // Debug.Log("들어옴"); // 확인차 
                Destroy(go);
            }
        }

    }
}
