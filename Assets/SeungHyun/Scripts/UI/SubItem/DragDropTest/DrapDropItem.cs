using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// 아이템 드래그 드롭할 창에 놔두는거
public class DrapDropItem : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    RectTransform _rectTransform;
    CanvasGroup _canvasGroup;
    Map map;
    public static event Action OnisEmptySlot;

    private void Start()
    {
        map = Map.MapObject.GetComponent<Map>();
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.pointerDrag.GetComponent<Image>().sprite == default) { return; }
        Debug.Log("OnBeginDrag");
        _canvasGroup.alpha = 0.6f;
        _canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.pointerDrag.GetComponent<Image>().sprite == default) { return; }

        Debug.Log("OnDrag");
        _rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnDrop(PointerEventData eventData)
    {
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        _canvasGroup.alpha = 1f;
        _canvasGroup.blocksRaycasts = true;
        // Player를 연동 해야됨 DeleteRelic에 추가 
        if (eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition != Vector2.zero) // 만약 마우스 포인터가 칸 위치가 아니라면? 밖을 향한다면
        {
            eventData.pointerDrag.GetComponent<Image>().sprite = Managers.ItemDataBase.GetItemData(0).itemImage;
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            //수정사항
            eventData.pointerDrag.GetComponentInParent<UI_RelicInven_Item>().IsEmpty = true;
            //OnisEmptySlot?.Invoke();
            
            GameObject go = Managers.Resources.Instantiate($"sh/Relic/{Managers.ItemDataBase.GetItemData(eventData.pointerDrag.GetComponentInParent<UI_RelicInven_Item>().SlotItemID).itemName}"); // 아이템 생성
            eventData.pointerDrag.GetComponentInParent<UI_RelicInven_Item>().SlotItemID = 0;
            // Todo: 플레이어 위치를 받아와야 하는데 이걸 플레이어 를 따로 받아와야 할듯?
            go.transform.position = map.Player.transform.position;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }
}
