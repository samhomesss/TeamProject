using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using yb;

// ������ �巡�� ����� â�� ���δ°�
public class DrapDropItem : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    RectTransform _rectTransform;
    CanvasGroup _canvasGroup;
    Map map;
    private PhotonView _photonview;
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
        String name = eventData.pointerDrag.GetComponent<Image>().sprite.name;
        Debug.Log("OnEndDrag");
        _canvasGroup.alpha = 1f;
        _canvasGroup.blocksRaycasts = true;
        // Player�� ���� �ؾߵ� DeleteRelic�� �߰� 
        if (eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition != Vector2.zero) // ���� ���콺 �����Ͱ� ĭ ��ġ�� �ƴ϶��? ���� ���Ѵٸ�
        {

            if (IsTestMode.Instance.CurrentUser == Define.User.Hw)
            {
                _photonview.RPC("DropItem", RpcTarget.All, _photonview.ViewID,name);
                _photonview.RPC("SetDropItemName", RpcTarget.All, _photonview.ViewID);
            }
            else
            {
                IRelic go = Managers.Resources.Instantiate($"sh/Relic/{Managers.ItemDataBase.GetItemData(eventData.pointerDrag.GetComponent<Image>().sprite.name).itemName}").GetComponent<IRelic>(); // ������ ����
                go.MyTransform.position = map.Player.transform.position;
                go.DeleteRelic(map.Player);
            }

            eventData.pointerDrag.GetComponent<Image>().sprite = default;
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            eventData.pointerDrag.GetComponentInParent<UI_RelicInven_Item>().IsEmpty = true;
            eventData.pointerDrag.GetComponentInParent<UI_RelicInven_Item>().SlotItemID = default;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }




    
}
