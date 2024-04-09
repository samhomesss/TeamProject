using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager
{
    public Action OnKeyboardEvent;
    public Action<Define.MouseEventType> OnMouseEvent;

    #region �ӽ� �߰� ���� 04.08
    //Todo : ������ �� Action �����ͼ� ���� ���Ѿߵ�
    public Action GetItemEvent;
    #endregion

    public void OnUpdate()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.anyKey)
            OnKeyboardEvent?.Invoke();

        if (Input.GetMouseButtonDown(0))
            OnMouseEvent?.Invoke(Define.MouseEventType.LeftMouseDown);
        
        if (Input.GetMouseButtonDown(1))
            OnMouseEvent?.Invoke(Define.MouseEventType.RightMouseDown);

        if(Input.GetMouseButton(0))
            OnMouseEvent?.Invoke(Define.MouseEventType.LeftMouse);

        if (Input.GetMouseButton(1))
            OnMouseEvent?.Invoke(Define.MouseEventType.RightMouse);

        if (Input.GetMouseButtonUp(0))
            OnMouseEvent?.Invoke(Define.MouseEventType.LeftMouseUp);

        if (Input.GetMouseButtonUp(1))
            OnMouseEvent?.Invoke(Define.MouseEventType.RightMouseUp);

        #region ���� �߰� 04.08
        if (Input.GetKeyDown(KeyCode.F))
        {
            GetItemEvent?.Invoke();
        }
        #endregion
    }
}
