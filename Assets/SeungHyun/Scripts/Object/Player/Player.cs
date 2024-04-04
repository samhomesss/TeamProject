using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sh
{
    public class Player : Obj
    {

        Vector2 inputVec;
        float moveSpeed = 10f;

        public static event Action OnNodeChanged; // 노드 색을 바꾸는 이벤트

        private void Update()
        {
            inputVec.x = Input.GetAxis("Horizontal");
            inputVec.y = Input.GetAxis("Vertical");

            if (inputVec.x != 0 || inputVec.y != 0)
            {
                OnNodeChanged?.Invoke();
            }

            Vector3 newPos = new Vector3(transform.position.x + inputVec.x * moveSpeed * Time.deltaTime, 1.5f, transform.position.z + inputVec.y * moveSpeed * Time.deltaTime);

            transform.position = Vector3.MoveTowards(transform.position, newPos, 2f);
        }
    }
}



