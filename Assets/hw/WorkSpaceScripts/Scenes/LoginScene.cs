using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hw
{
    public class LoginScene : BaseScene
    {
        protected override void Init()
        {
            base.Init();

            SceneType = Define.Scene.Login;

        }
        public override void Clear()
        {
            Debug.Log("LoginScene Clear!");
        }
    }
}

