using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using yb;

// 아이템 드래그 드롭할 창에 놔두는거
public class DrapDropItem : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    RectTransform _rectTransform;
    CanvasGroup _canvasGroup;
    Map map;

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
            IRelic go = Managers.Resources.Instantiate($"sh/Relic/{Managers.ItemDataBase.GetItemData(eventData.pointerDrag.GetComponent<Image>().sprite.name).itemName}").GetComponent<IRelic>(); // 아이템 생성
            eventData.pointerDrag.GetComponent<Image>().sprite = default;// Managers.Resources.Load<Sprite>("Prefabs/sh/UI/Texture/DefaultItemImage");
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            
            eventData.pointerDrag.GetComponentInParent<UI_RelicInven_Item>().IsEmpty = true;
            eventData.pointerDrag.GetComponentInParent<UI_RelicInven_Item>().SlotItemID = default;
            go.MyTransform.position = map.Player.transform.position;
            go.DeleteRelic(map.Player);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }


    
}
