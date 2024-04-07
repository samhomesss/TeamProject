using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class GameScene : BaseScene
 {
     public override void Clear()
     {
     }

     public override void Init()
     {
         base.Init();

        //주의 메인에 머지할때는 희웅 작업영역 주석처리할것
        if (IsTestMode.Instance.CurrentUser == Define.User.Hw)
        {
            GameObject go = PhotonNetwork.Instantiate("Prefabs/hw/PlayerPrefabs/Player", Vector3.zero, Quaternion.identity);
            go.name = "Player";
            // UI 
        }

        Managers.UI.ShowSceneUI<UI_Weapon>();
         Managers.UI.ShowSceneUI<UI_Inven>();
         Managers.UI.ShowSceneUI<UI_Hp>();
         Managers.UI.ShowSceneUI<UI_MiniMap>();
         Managers.UI.ShowSceneUI<ItemCreate_Button>();

         //GameObject
         Managers.SceneObj.ShowSceneObject<Map>();
         Managers.SceneObj.ShowSceneObject<MiniMapCam>();
     }

 }


