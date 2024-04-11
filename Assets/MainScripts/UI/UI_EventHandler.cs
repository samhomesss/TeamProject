using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EventHandler : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler , IPointerExitHandler
{
    public Action<PointerEventData> OnEnterHandler;
    public Action<PointerEventData> OnClickHandler;
    
    #region 04.05 ½ÂÇö Ãß°¡
    public Action<PointerEventData> OnDragHandler = null;
    public Action<PointerEventData> OnExitHandler = null;

    #endregion
    public void OnPointerClick(PointerEventData eventData)
    {
        OnClickHandler?.Invoke(eventData);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnEnterHandler?.Invoke(eventData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnExitHandler?.Invoke(eventData);
    }
}
