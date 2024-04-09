using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ItemCreateButton : UI_Scene
{
    // ������ ���� Ȯ���� ���� ��ũ��Ʈ �Դϴ�
    Button itemCreatebutton;
    int itemID = 50;
    public static event Action<int> OnItemCreateClicked;
    private void Start()
    {
        itemCreatebutton = gameObject.transform.GetChild(0).GetComponent<Button>(); 
        itemCreatebutton.onClick.AddListener(() => OnItemCreateClicked.Invoke(itemID));
    }


}
