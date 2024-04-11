using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerTestSh: Obj
{
    Vector2 inputVec;
    float moveSpeed = 10f;
    // ��ǻ� �ϳ��� �̺�Ʈ�� ��� �ɵ�? 
    public static event Action OnNodeChanged; // ��� ���� �ٲٴ� �̺�Ʈ
    public static event Action OnItemCheacked; // �÷��̾ �����϶� ������ üũ
    public static event Action OnNamePos; // �÷��̾� �̸� ��ġ
    public static Action OnPlayerColorChecked; // �̴ϸ� �� �÷��̾� ���� ���� ������ ���� ����

    Transform refTranform;

    float resetTimer = 0; // 1�ʸ��� ������ ��� �ϱ� ���ؼ� ���

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




