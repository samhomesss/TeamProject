using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class ItemInfoName : UI_Scene
{
    GameObject go = null; // �ʱ�ȭ ���� ���ְ�
    string itemName; // ��� ������ �̸�
    Text itemInfoTextUI; // UI���� ��� ������ �ؽ�Ʈ �̸�
                         // �ش� �������� �Ծ����� â ���� ���� �ϸ� �ɵ�?
    float diff; // �Ÿ�
    public static event Action<int> OnItemGet; //������ ȹ�� ������

    // �÷��̾� �̵� �׼ǿ� �޾� �ָ� �ɵ�?
    private void Start()
    {
        PlayerTestSh.OnItemCheacked += CloseByPlayer;
    }

    private void Update()
    {
        // Todo : �̺κ��� ���߿� ������ �� �� ������ �ͼ� Update���� �����ϰ� Action���� ��ü
        if (Input.GetKeyDown(KeyCode.F)) // Ű ��������
        {
            if (diff <= 3f)
            {
                OnItemGet?.Invoke(gameObject.GetComponent<Item>().ItemID); // �ش� �������� ������ ������ ���� ���ְ�
                Destroy(gameObject); // �ı�
                Destroy(go);
                PlayerTestSh.OnItemCheacked -= CloseByPlayer;

            }

        }
    }

    void CloseByPlayer()
    {
        diff = Vector3.Distance(Map.Player.transform.position, gameObject.transform.position);

        if (diff <= 3f)
        {
            if (go != null )
            {
                return;
            }
            else
            {
                // Todo: �ش� ������Ʈ ���� ���°� ���� �ؾߵ�
                go = Managers.Resources.Instantiate($"sh/UI/Scene/UI_ItemName"); // ������ ���� 
                Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, gameObject.transform.position); // ���⼭ ����� �����°� �ƴϰ� 
                RectTransform mainCanvasRect = go.GetComponent<RectTransform>();
                if (RectTransformUtility.ScreenPointToLocalPointInRectangle(mainCanvasRect, screenPoint, null, out Vector2 localPoint)) // �ش� ĵ�������� � ��ġ�� �ִ��� ã�ƾߵ�
                {
                    for (int ix = 0; ix < go.transform.childCount; ++ix)
                    {
                        var child = go.transform.GetChild(ix);
                        if (child.name.Equals("Item"))
                        {
                            child.GetComponent<RectTransform>().anchoredPosition = localPoint + new Vector2(0f, 80f);
                            break;
                        }
                    }
                }
                //go.GetComponent<RectTransform>().anchoredPosition = screenPoint;
                

                //Debug.Log($"{gameObject.transform.position} | {screenPoint}");

                itemInfoTextUI = go.transform.GetChild(0).GetChild(1).GetComponent<Text>();
                itemName = Managers.ItemDataBase.GetItemData(gameObject.GetComponent<Item>().ItemID).itemName.ToString();

                itemInfoTextUI.text = itemName; // �������� �̸� ���� 
                //go.GetComponent<Canvas>().worldCamera = Camera.main;
                //go.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 20);

              
            }
        }
        else
        {
            if (go != null)
            {
                // Debug.Log("����"); // Ȯ���� 
                Destroy(go);
            }
        }

    }
}
