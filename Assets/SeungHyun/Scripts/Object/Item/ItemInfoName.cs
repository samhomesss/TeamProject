using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class ItemInfoName : UI_Scene
{
    GameObject go = null; // �ʱ�ȭ ���� ���ְ�
    string itemName; // ��� ������ �̸�
    Text itemInfoTextUI; // UI���� ��� ������ �ؽ�Ʈ �̸�

    float diff; // �Ÿ�
    public static event Action<int> OnItemGet; //������ ȹ�� ������

    private void Start()
    {
        PlayerTestSh.OnItemCheacked += CloseByPlayer; // �̰� �κ� �ٲ� ��ߵ�
        Managers.Input.GetItemEvent += IsClosedItem; // �̰� �κ� �ٲ� ��ߵ� 
    }

    // �ش� �������� ������ �־ Ư�� Ű�� �������� �Դ� �۾� 
    void IsClosedItem()
    {
        int count = 0;

        if (diff <= 3f)
        {

            switch (gameObject.GetComponent<Item>().ItemID / 500)
            {
                case 0: // ��������
                    break;
                case 1: // �Ϲ� ������
                    break;
                case 2: // ���� ������
                    for (int i = 0; i < UI_RelicInven.UI_RelicInven_Items.Count; i++)
                    {
                        if (!UI_RelicInven.UI_RelicInven_Items[i].IsEmpty)
                        {
                            count++;
                            if (count == 2)
                            // ������ ��á���� �ٲܲ��� �˾�â ����� �����ؼ� ���� 
                            // �Ծ��� �ƴϸ� ���� ���� ������ ������
                            continue;
                        }
                        OnItemGet?.Invoke(gameObject.GetComponent<Item>().ItemID);
                        Destroy(gameObject);
                        Destroy(go);
                        PlayerTestSh.OnItemCheacked -= CloseByPlayer; // �÷��̾� ��ó�� ������ ���°� 
                        Managers.Input.GetItemEvent -= IsClosedItem; // ������ �Դ°� 
                        break;
                    }
                    break;
               
            }

           
        }
    }

    // ������ �� ������ �־ �������� �Դ� �۾�
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
                


                itemInfoTextUI = go.transform.GetChild(0).GetChild(1).GetComponent<Text>();
                itemName = Managers.ItemDataBase.GetItemData(gameObject.GetComponent<Item>().ItemID).itemName.ToString();

                itemInfoTextUI.text = itemName; // �������� �̸� ���� 

              
            }
        }
        else
        {
            if (go != null)
            {
                Destroy(go);
            }
        }

    }
}
