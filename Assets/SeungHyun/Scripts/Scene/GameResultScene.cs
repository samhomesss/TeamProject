using Photon.Pun;
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

        
        if(PhotonNetwork.IsMasterClient) {
            PhotonNetwork.Instantiate("Prefabs/sh/UI/Scene/UI_GameResult", Vector3.zero, Quaternion.identity);
        }

        //Managers.UI.ShowSceneUI<UI_GameResult>();
        Managers.SceneObj.ShowSceneObject<GameResultMap>();

        _fade.SetFade(false);
    }
}
