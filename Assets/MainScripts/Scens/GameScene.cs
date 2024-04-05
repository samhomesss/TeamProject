using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace hw
{
    public class GameScene : BaseScene
    {
        public override void Clear()
        {
        }

        public override void Init()
        {
            base.Init();

            GameObject go = PhotonNetwork.Instantiate("Prefabs/hw/PlayerPrefabs/Player", Vector3.zero, Quaternion.identity);
            go.name = "Player";
            // UI 
            Sh.Managers.UI.ShowSceneUI<Sh.UI_Weapon>();
            Sh.Managers.UI.ShowSceneUI<Sh.UI_Inven>();
            Sh.Managers.UI.ShowSceneUI<Sh.UI_Hp>();
            Sh.Managers.UI.ShowSceneUI<Sh.UI_MiniMap>();
            Sh.Managers.UI.ShowSceneUI<Sh.ItemCreate_Button>();

            //GameObject
            Sh.Managers.SceneObj.ShowSceneObject<Sh.Map>();
            Sh.Managers.SceneObj.ShowSceneObject<MiniMapCam>();
        }

    }

}
