using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using yb;

public class UI_PlayerName : UI_Scene
{
    GameObject NameText; // �÷��̾��� �̸�������Ʈ
    Map map;
    private void Start()
    {
        map = Map.MapObject.GetComponent<Map>();
        NameText = Util.FindChild(gameObject, "NameText", true);
        NameText.GetComponent<Text>().text = map.Player.name; // �̸� �ؽ�Ʈ�� �̰ɷ� �������ְ�
        PlayerName();
        PlayerTestSh.OnNamePos += PlayerName;
    }
    
    // Todo : ������ �� �÷��̾� ���� �Ϸ��� PlayerControoler ��� �޾Ƽ� ó�� �ؾ� �ϴµ� 
    void PlayerName()
    {
        // ��ġ�� Map.Player.tranform.position�� ��� ���� PlayerController�� ��ġ�� �־��ָ� ��
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, map.Player.transform.position); // ���⼭ ����� �����°� �ƴϰ� 
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
    // ToDo: �������� �׼� �߰�
    void SetPlayer(PlayerController player)
    {
        player.MapEvent -= PlayerName;
        player.MapEvent += PlayerName;
    }
}
