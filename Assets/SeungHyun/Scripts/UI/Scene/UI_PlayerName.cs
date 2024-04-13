using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using yb;

public class UI_PlayerName : UI_Scene
{
    GameObject NameText; // 플레이어의 이름오브젝트
    Map map;
    private void Start()
    {
        map = Map.MapObject.GetComponent<Map>();
        NameText = Util.FindChild(gameObject, "NameText", true);
        NameText.GetComponent<Text>().text = map.Player[PhotonNetwork.LocalPlayer.ActorNumber - 1].transform.parent.name; // 이름 텍스트를 이걸로 설정해주고
        PlayerName();
        SetPlayer(map.Player[PhotonNetwork.LocalPlayer.ActorNumber- 1]);
        //PlayerTestSh.OnNamePos += PlayerName;
    }
    
    // Todo : 윤범이 형 플레이어 연결 하려면 PlayerController 상속 받아서 처리 해야 하는데 
    void PlayerName()
    {
        // 위치는 Map.Player.tranform.position을 상속 받은 PlayerController의 위치에 넣어주면 됨
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, map.Player[PhotonNetwork.LocalPlayer.ActorNumber-1].transform.parent.position); // 여기서 계산이 끝나는게 아니고 
        RectTransform mainCanvasRect = gameObject.GetComponent<RectTransform>();
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(mainCanvasRect, screenPoint, null, out Vector2 localPoint)) // 해당 캔버스에서 어떤 위치에 있는지 찾아야됨
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
    // ToDo: 윤범이형 액션 추가
    void SetPlayer(PlayerController player)
    {
        //player.MapEvent -= PlayerName;
        player.MapEvent += PlayerName;
    }
}
