using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace Sh
{
    public class InputManager
    {
        public Action<Define.MouseEventType> MouseAction = null;

        #region 03.31 승현 추가 
        // 체력 감소 액션 일단은 특정 상호 작용이 아닌 키로 감소 시키고 있어서 여기에 만들어 놓음 옮기면 됨
        public Action<float> HpReduce = null;
        float _damaged = 10f;

        // 총알 나가는 거 액션 
        public Action<int> BulletReduce = null;
        int _bulletCount = 1;
        float count = 1f;
        float shotTime = 1f;
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
                    if (count < shotTime)
                    {
                        return;
                    }
                    else
                    {
                        BulletReduce.Invoke(_bulletCount);
                        count = 0;
                    }
                   
                }
            }
        }
        #endregion


        public void OnUpdate()
        {
            count += Time.deltaTime;

            if (EventSystem.current.IsPointerOverGameObject())
                return;

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
                    MouseAction.Invoke(Define.MouseEventType.Press);
                    _pressed = true;
                }
                else
                {
                    if (_pressed)
                        MouseAction.Invoke(Define.MouseEventType.Click);
                    _pressed = false;
                }
            }
        }


        


        public void Clear()
        {
            MouseAction = null;
        }
    }
}


