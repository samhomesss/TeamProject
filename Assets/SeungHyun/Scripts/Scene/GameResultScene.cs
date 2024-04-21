using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResultScene : BaseScene
{
    public override void Clear()
    {
    }
    public override void Init()
    {
        base.Init();
        Managers.UI.ShowSceneUI<UI_GameResult>();
        Managers.SceneObj.ShowSceneObject<GameResultMap>();

        _fade.SetFade(false);
    }
}
