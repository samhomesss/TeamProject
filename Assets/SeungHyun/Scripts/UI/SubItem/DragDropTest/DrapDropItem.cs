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
        if (eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition != Vector2.zero)
        {
            eventData.pointerDrag.GetComponent<Image>().sprite = Managers.ItemDataBase.GetItemData(0).itemImage;
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            
            OnisEmptySlot?.Invoke();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
        Debug.Log("OnPointerDown");
    }
}
