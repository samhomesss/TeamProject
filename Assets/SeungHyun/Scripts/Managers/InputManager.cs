using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace Sh
{
    public class InputManager
    {
        public Action KeyAction = null;
        public Action<Define.MouseEvent> MouseAction = null;

        #region 03.31 승현 추가 
        // 체력 감소 액션 일단은 특정 상호 작용이 아닌 키로 감소 시키고 있어서 여기에 만들어 놓음 옮기면 됨
        public Action<float> HpReduce = null;
        float _damaged = 10f;

        // 총알 나가는 거 액션 
        public Action<int> BulletReduce = null;
        int _bulletCount = 1;
        int count = 50;
        #endregion


        bool _pressed = false;

        #region 03.31 승현 추가
        public void BulletShot()
        {
            // 총알 나가는 거 
            if (BulletReduce != null)
            {
                if (Input.GetMouseButton(0))
                {
                    if (count == 50)
                    {
                        BulletReduce.Invoke(_bulletCount);
                        count = 0;
                    }
                    else { count++; }
                   
                }
            }
        }
        #endregion


        public void OnUpdate()
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            if (Input.anyKey && KeyAction != null)
                KeyAction.Invoke();

            #region 03.31 승현 추가 
            // K를 누르고 구독자가 있으면 
            if (Input.GetKeyDown(KeyCode.K) && HpReduce != null)
            {
                HpReduce.Invoke(_damaged);
            }

           

            #endregion

            if (MouseAction != null)
            {
                if (Input.GetMouseButton(0))
                {
                    MouseAction.Invoke(Define.MouseEvent.Press);
                    _pressed = true;
                }
                else
                {
                    if (_pressed)
                        MouseAction.Invoke(Define.MouseEvent.Click);
                    _pressed = false;
                }
            }
        }


        


        public void Clear()
        {
            KeyAction = null;
            MouseAction = null;
        }
    }
}


