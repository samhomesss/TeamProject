using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerTestSh: Obj
{
    Vector2 inputVec;
    float moveSpeed = 10f;
    // 사실상 하나의 이벤트로 묶어도 될듯? 
    public static event Action OnNodeChanged; // 노드 색을 바꾸는 이벤트
    public static event Action OnItemCheacked; // 플레이어가 움직일때 아이템 체크
    public static event Action OnNamePos; // 플레이어 이름 위치
    public static Action OnPlayerColorChecked; // 미니맵 옆 플레이어 색상에 대한 정보를 띄우기 위함

    Transform refTranform;

    float resetTimer = 0; // 1초마다 찍을때 사용 하기 위해서 사용

    private void Awake()
    {
        refTranform = transform;
    }

    private void Update()
    {
        resetTimer += Time.deltaTime;
        inputVec.x = Input.GetAxis("Horizontal");
        inputVec.y = Input.GetAxis("Vertical");
        if (inputVec.x != 0 || inputVec.y != 0)
        {
            OnNodeChanged?.Invoke();
            OnItemCheacked?.Invoke();   
            OnNamePos?.Invoke();
            if (resetTimer >= 2.0f)
            {
                OnPlayerColorChecked?.Invoke();
                resetTimer = 0.0f;
            }
           
        }
         
        Vector3 newPos = new Vector3(refTranform.position.x + inputVec.x * moveSpeed * Time.deltaTime, 1.5f, refTranform.position.z + inputVec.y * moveSpeed * Time.deltaTime);
        refTranform.position = Vector3.MoveTowards(refTranform.position, newPos, 2f);
    }
}




