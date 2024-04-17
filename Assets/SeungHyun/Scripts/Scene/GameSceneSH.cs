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
        if(IsTestMode.Instance.CurrentUser == Define.User.Yb) {
            return;
        }
        base.Init();
        // UI 
        Managers.UI.ShowSceneUI<UI_Timer>();
        Managers.UI.ShowSceneUI<UI_Weapon>();
        Managers.UI.ShowSceneUI<UI_Inven>();
        Managers.UI.ShowSceneUI<UI_Hp>();
        Managers.UI.ShowSceneUI<UI_MiniMap>();
        Managers.UI.ShowSceneUI<UI_RelicInven>();
        Managers.UI.ShowSceneUI<UI_PlayerColorPercent>();

        // UIInfo
        UI_ItemInfo.ItemInfo = Managers.UI.ShowSceneUIInfo<UI_ItemInfo>().gameObject;
        UI_ItemInfo.ItemInfo.SetActive(false);

        // 플레이어들에게 보여야 하는 UI
        Managers.UI.ShowSceneUI<UI_PlayerName>();

        //GameObject
        Managers.SceneObj.ShowSceneObject<Map>();
        Managers.SceneObj.ShowSceneObject<MiniMapCam>();

    }  

}
