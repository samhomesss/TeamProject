using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    [SerializeField] private PhotonView _photonView;
    public override void Clear()
    {
    }

    public override void Init()
    {
        base.Init();
        //todo
        if (IsTestMode.Instance.CurrentUser == Define.User.Hw)
        {
            GameObject go = PhotonNetwork.Instantiate("Prefabs/hw/PlayerPrefabs/Player", Vector3.zero, Quaternion.identity);
            go.name = "Player";
            _photonView = Util.FindChild(go, "Model").GetComponent<PhotonView>();
            if (_photonView.IsMine)
            {
                Util.FindChild(go, "Camera", true).active = true;
            }
            //    Managers.UI.ShowSceneUI<UI_Weapon>();
            //    Managers.UI.ShowSceneUI<UI_Inven>();
            //    Managers.UI.ShowSceneUI<UI_Hp>();
            //    Managers.UI.ShowSceneUI<UI_MiniMap>();
            //    // UI 

            //    //GameObject
            //    Managers.SceneObj.ShowSceneObject<Map>();
            //    Managers.SceneObj.ShowSceneObject<MiniMapCam>();
            //}
            //Managers.UI.ShowSceneUI<ItemCreate_Button>();
        }


    }
}

