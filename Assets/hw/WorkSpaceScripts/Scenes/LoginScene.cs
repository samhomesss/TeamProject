using UnityEngine;

namespace Hw
{
    public class LoginScene : BaseScene
    {
        protected override void Init()
        {
            base.Init();

            SceneType = Define.Scene.Login;

            Managers.UI.ShowSceneUI<LoginUI>("Login/LoginUI");
            Managers.UI.ShowSceneUI<RegisterID>("Login/RegisterID");
            Managers.UI.ShowSceneUI<RegisterNickname>("Login/RegisterNickname");
        }

        public override void Clear()
        {
            Debug.Log("LoginScene Clear!");
        }
    }
}

