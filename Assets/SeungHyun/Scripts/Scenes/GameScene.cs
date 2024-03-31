using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sh
{
    public class GameScene : BaseScene
    {
        protected override void Init()
        {
            base.Init();

            SceneType = Define.Scene.Game;

            Managers.UI.ShowSceneUI<UI_Weapon>();
            Managers.UI.ShowSceneUI<UI_Inven>();
            Managers.UI.ShowSceneUI<UI_Hp>();
            Managers.UI.ShowSceneUI<UI_Minimap>();
        }

        public override void Clear()
        {

        }
    }
}

