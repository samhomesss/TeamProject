using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using yb;

public class GameScene : BaseScene
{
    private PhotonView _photonView;
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

            if (PhotonNetwork.IsMasterClient)
            {
                GameObject guardRelic = PhotonNetwork.Instantiate("Prefabs/yb/Relic/GuardRelic", new Vector3(2, 1, 10), Quaternion.identity);
                GameObject shieldRelic = PhotonNetwork.Instantiate("Prefabs/yb/Relic/ShieldRelic", new Vector3(10, 1, 2), Quaternion.identity);
                GameObject bonusAttackSpeedRelic = PhotonNetwork.Instantiate("Prefabs/yb/Relic/BonusAttackSpeedRelic", new Vector3(1, 1, 2), Quaternion.identity);
                GameObject BonusProjectileRelic = PhotonNetwork.Instantiate("Prefabs/yb/Relic/BonusProjectileRelic", new Vector3(1, 1, 4), Quaternion.identity);
                GameObject BonusResurrectionTimeRelic = PhotonNetwork.Instantiate("Prefabs/yb/Relic/BonusResurrectionTimeRelic", new Vector3(1, 1, 6), Quaternion.identity);
            }
            
            go.name = "Player";
            _photonView = Util.FindChild(go, "Model").GetComponent<PhotonView>();
            if (_photonView.IsMine)
            {
                Util.FindChild(go, "Camera", true).active = true;
                Util.FindChild(go, "Camera", true).GetComponent<AudioListener>().enabled = true;
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

