using Photon.Pun;
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
        NameText.GetComponent<Text>().text = map.Player[PhotonNetwork.LocalPlayer.ActorNumber - 1].transform.parent.name; // �̸� �ؽ�Ʈ�� �̰ɷ� �������ְ�
        PlayerName();
        SetPlayer(map.Player[PhotonNetwork.LocalPlayer.ActorNumber- 1]);
        //PlayerTestSh.OnNamePos += PlayerName;
    }
    
    // Todo : ������ �� �÷��̾� ���� �Ϸ��� PlayerController ��� �޾Ƽ� ó�� �ؾ� �ϴµ� 
    void PlayerName()
    {
        // ��ġ�� Map.Player.tranform.position�� ��� ���� PlayerController�� ��ġ�� �־��ָ� ��
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, map.Player[PhotonNetwork.LocalPlayer.ActorNumber-1].transform.parent.position); // ���⼭ ����� �����°� �ƴϰ� 
        RectTransform mainCanvasRect = gameObject.GetComponent<RectTransform>();
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(mainCanvasRect, screenPoint, null, out Vector2 localPoint)) // �ش� ĵ�������� � ��ġ�� �ִ��� ã�ƾߵ�
        {
            for (int ix = 0; ix < gameObject.transform.childCount; ++ix)
            {
                var child = gameObject.transform.GetChild(ix);
                if (child.name.Equals("PlayerNameBG"))
                {
                    child.GetComponent<RectTransform>().anchoredPosition = Vector2.zero + new Vector2 (15, -20);//localPoint;// + new Vector2(0,-50);
                    break;
                }
            }
        }
    }
    // ToDo: �������� �׼� �߰�
    void SetPlayer(PlayerController player)
    {
        //player.MapEvent -= PlayerName;
        player.MapEvent += PlayerName;
    }
}
