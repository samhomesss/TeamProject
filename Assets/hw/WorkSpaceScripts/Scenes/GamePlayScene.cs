using Photon.Pun;
using Sh;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hw
{
    public class GamePlayScene : BaseScene
    {
        protected override void Init()
        {
            base.Init();


            //TODO 1. �ʺҷ��´�
            //2.Coututine ���� �ʺҷ��°� üũ�� ������ �÷��̾ �ҷ��´�.
            //3.

            PhotonNetwork.Instantiate("Prefabs/hw/PlayerPrefabs/Player",Vector3.zero,Quaternion.identity);

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
        public override void Clear()
        {
        }
    }
}
