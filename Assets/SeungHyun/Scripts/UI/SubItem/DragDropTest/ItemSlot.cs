using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public static RectTransform IconPos => _iconPos;
    static RectTransform _iconPos;
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        if (eventData.pointerDrag != null)
        {
            
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition
                = Vector2.zero;
            
        } 
    }
}
