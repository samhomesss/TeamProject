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


            //TODO 1. 맵불러온다
            //2.Coututine 으로 맵불러온걸 체크한 다음에 플레이어를 불러온다.
            //3.

            PhotonNetwork.Instantiate("Prefabs/hw/PlayerPrefabs/Player",Vector3.zero,Quaternion.identity);
            Managers.SceneObj.ShowSceneObject<Map>();

        }
        public override void Clear()
        {
        }
    }
}

