using UnityEngine;


public class LoginScene : BaseScene
{
    public override void Init()
    {
        base.Init();

        SceneType = Define.SceneType.Login;

        Managers.UI.ShowSceneUI<LoginUI>("Login/LoginUI", "hw");
        Managers.UI.ShowSceneUI<RegisterID>("Login/RegisterID", "hw");
        Managers.UI.ShowSceneUI<RegisterNickname>("Login/RegisterNickname", "hw");
        Managers.UI.ShowSceneUI<UI_DescriptionPanel>("Login/UI_DescriptionPanel", "hw");
        Managers.UI.ShowSceneUI<UI_Description>("Login/UI_Description", "hw");
        Managers.UI.ShowSceneUI<ConfirmPopupUI>("Login/ConfirmPopupUI", "hw");
        Managers.UI.ShowSceneUI<AlertPopupUI>("Login/AlertPopupUI", "hw"); 
    }

    public override void Clear()
    {
        Debug.Log("LoginScene Clear!");
    }
}
