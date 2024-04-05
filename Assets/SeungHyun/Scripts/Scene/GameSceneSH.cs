using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneSH : BaseScene
{
    public override void Clear()
    {
    }

    public override void Init()
    {
        base.Init();

        // GameObject go = PhotonNetwork.Instantiate("Prefabs/hw/PlayerPrefabs/Player", Vector3.zero, Quaternion.identity);
        // go.name = "Player";

        // UI 
        Managers.UI.ShowSceneUI<UI_Weapon>();
        Managers.UI.ShowSceneUI<UI_Inven>();
        Managers.UI.ShowSceneUI<UI_Hp>();
        Managers.UI.ShowSceneUI<UI_MiniMap>();
        //Managers.UI.ShowSceneUI<ItemCreate_Button>();
        //GameObject
        Managers.SceneObj.ShowSceneObject<Map>();
        Managers.SceneObj.ShowSceneObject<MiniMapCam>();
    }

}
