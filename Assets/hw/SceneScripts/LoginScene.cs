using UnityEngine;


    public class LoginScene : BaseScene
    {
        public override void Init()
        {
            base.Init();

            SceneType = Define.SceneType.Login;

            Managers.UI.ShowSceneUI<LoginUI>("Login/LoginUI","hw");
            Managers.UI.ShowSceneUI<RegisterID>("Login/RegisterID","hw");
            Managers.UI.ShowSceneUI<RegisterNickname>("Login/RegisterNickname","hw");
        }

        public override void Clear()
        {
            Debug.Log("LoginScene Clear!");
        }
    }
