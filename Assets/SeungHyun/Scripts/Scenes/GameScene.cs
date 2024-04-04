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

            // UI 
            Managers.UI.ShowSceneUI<UI_Weapon>();
            Managers.UI.ShowSceneUI<UI_Inven>();
            Managers.UI.ShowSceneUI<UI_Hp>();
            Managers.UI.ShowSceneUI<UI_MiniMap>();
            Managers.UI.ShowSceneUI<ItemCreate_Button>();

            //GameObject
            Managers.SceneObj.ShowSceneObject<Map>();
            Managers.SceneObj.ShowSceneObject<MiniMapCam>();

        }

        public override void Clear()
        {

        }
    }
}

