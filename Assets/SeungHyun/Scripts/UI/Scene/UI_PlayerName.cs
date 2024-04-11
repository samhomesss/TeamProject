using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerName : UI_Scene
{
    GameObject NameText; // �÷��̾��� �̸�������Ʈ

    private void Start()
    {
        NameText = Util.FindChild(gameObject, "NameText", true);
        NameText.GetComponent<Text>().text = Map.Player.name; // �̸� �ؽ�Ʈ�� �̰ɷ� �������ְ�
        PlayerName();
        PlayerTestSh.OnNamePos += PlayerName;
    }
    
    // Todo : ������ �� �÷��̾� ���� �Ϸ��� PlayerControoler ��� �޾Ƽ� ó�� �ؾ� �ϴµ� 
    void PlayerName()
    {
        // ��ġ�� Map.Player.tranform.position�� ��� ���� PlayerController�� ��ġ�� �־��ָ� ��
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, Map.Player.transform.position); // ���⼭ ����� �����°� �ƴϰ� 
        RectTransform mainCanvasRect = gameObject.GetComponent<RectTransform>();
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(mainCanvasRect, screenPoint, null, out Vector2 localPoint)) // �ش� ĵ�������� � ��ġ�� �ִ��� ã�ƾߵ�
        {
            for (int ix = 0; ix < gameObject.transform.childCount; ++ix)
            {
                var child = gameObject.transform.GetChild(ix);
                if (child.name.Equals("PlayerNameBG"))
                {
                    child.GetComponent<RectTransform>().anchoredPosition = localPoint + new Vector2(0,-50);
                    break;
                }
            }
        }
    }
}
