using Photon.Pun;
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
            Managers.SceneObj.ShowSceneObject<Map>();

        }
        public override void Clear()
        {
        }
    }
}

