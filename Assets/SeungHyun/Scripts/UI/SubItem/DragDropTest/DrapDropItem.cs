using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DrapDropItem : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    RectTransform _rectTransform;
    CanvasGroup _canvasGroup;

    public static event Action OnisEmptySlot;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        _canvasGroup.alpha = 0.6f;
        _canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        _rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnDrop(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        _canvasGroup.alpha = 1f;
        _canvasGroup.blocksRaycasts = true;
        if (eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition != Vector2.zero) // 만약 마우스 포인터가 칸 위치가 아니라면? 밖을 향한다면
        {
            eventData.pointerDrag.GetComponent<Image>().sprite = Managers.ItemDataBase.GetItemData(0).itemImage;
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            //수정사항
            eventData.pointerDrag.GetComponentInParent<UI_RelicInven_Item>().IsEmpty = true;
            //OnisEmptySlot?.Invoke();
            GameObject go = Managers.Resources.Instantiate($"sh/Relic/{Managers.ItemDataBase.GetItemData(eventData.pointerDrag.GetComponentInParent<UI_RelicInven_Item>().SlotItemID).itemName}"); // 아이템 생성
            eventData.pointerDrag.GetComponentInParent<UI_RelicInven_Item>().SlotItemID = 0;
            go.transform.position = Map.Player.transform.position;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }
}
