using Photon.Pun;
using Photon.Pun.Demo.Cockpit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Events;
using yb;

public class GameScene : BaseScene
{
    private PhotonView _photonView;
    private GameObject[] items = new GameObject[6];//0415 18:33 이희웅 테스트용 배열 추가
    private List<Transform> itemBox = new List<Transform>();//파라미터는 박스의 갯수
    public UnityEvent OnLoaded;

    private GameObject _itemBox;
    public override void Clear()
    {
    }

    void OnSetRelic(string itemID, UnityAction setRelicAction, UnityAction destroyRelicAction) //이희웅 0412 18:00 추가 아이템을 줏은뒤 이벤트를 처리
    {
        setRelicAction?.Invoke();
        destroyRelicAction?.Invoke();
    }


    public override void Init()
    {
        _itemBox = new GameObject("ItemBox");
        base.Init();
        //todo
        if (IsTestMode.Instance.CurrentUser == Define.User.Hw)
        {
            GameObject go = PhotonNetwork.Instantiate("Prefabs/hw/PlayerPrefabs/Player", Vector3.zero, Quaternion.identity);
            StartCoroutine(WaitPlayerLoded());
            go.GetComponentInChildren<PlayerController>().SetRelicEvent += OnSetRelic;
            _photonView = Util.FindChild(go, "Model").GetComponent<PhotonView>();

            if (PhotonNetwork.IsMasterClient)
            {
                items[0] = PhotonNetwork.Instantiate("Prefabs/yb/Relic/GuardRelic", new Vector3(2, 1, 10), Quaternion.identity);
                items[1] = PhotonNetwork.Instantiate("Prefabs/yb/Relic/ShieldRelic", new Vector3(10, 1, 2), Quaternion.identity);
                items[2] = PhotonNetwork.Instantiate("Prefabs/yb/Relic/BonusAttackSpeedRelic", new Vector3(1, 1, 2), Quaternion.identity);
                items[3] = PhotonNetwork.Instantiate("Prefabs/yb/Relic/BonusProjectileRelic", new Vector3(1, 1, 4), Quaternion.identity);
                items[4] = PhotonNetwork.Instantiate("Prefabs/yb/Relic/BonusResurrectionTimeRelic", new Vector3(1, 1, 6), Quaternion.identity);
                items[5] = PhotonNetwork.Instantiate("Prefabs/yb/Weapon/Shotgun", new Vector3(0, 1, 0), Quaternion.identity);
                for (int i = 0; i < items.Length; i++)
                {
                    _photonView.RPC("SetDropItemName", RpcTarget.All, items[i].GetComponent<PhotonView>().ViewID);
                }
            }
            if (_photonView.IsMine)
            {
                Util.FindChild(go, "Camera", true).SetActive(true);
                Util.FindChild(go, "Camera", true).GetComponent<AudioListener>().enabled = true;
                _photonView.RPC("RenamePlayer", RpcTarget.All, _photonView.ViewID);
            }

        }
        else if (IsTestMode.Instance.CurrentUser == Define.User.Sh)
        {
            Managers.UI.ShowSceneUI<UI_Timer>();
            Managers.UI.ShowSceneUI<UI_Weapon>();
            Managers.UI.ShowSceneUI<UI_Inven>();
            Managers.UI.ShowSceneUI<UI_Hp>();
            Managers.UI.ShowSceneUI<UI_MiniMap>();
            Managers.UI.ShowSceneUI<UI_RelicInven>();
            Managers.UI.ShowSceneUI<UI_PlayerColorPercent>();
        }

    }
    void ShowUI()
    {
        Managers.UI.ShowSceneUI<UI_Timer>();
        //GameObject
        Map map = Managers.SceneObj.ShowSceneObject<Map>();
        if (map != null)
        {
            map.onLoadMapUI += onLoadedUI;
        }

        Managers.SceneObj.ShowSceneObject<MiniMapCam>();
        Managers.UI.ShowSceneUI<UI_Weapon>();
        Managers.UI.ShowSceneUI<UI_Inven>();
        Managers.UI.ShowSceneUI<UI_Hp>();
        Managers.UI.ShowSceneUI<UI_MiniMap>();
        Managers.UI.ShowSceneUI<UI_RelicInven>();
        Managers.UI.ShowSceneUI<UI_PlayerColorPercent>();
        // UIInfo
        UI_ItemInfo.ItemInfo = Managers.UI.ShowSceneUIInfo<UI_ItemInfo>().gameObject;
        UI_ItemInfo.ItemInfo.SetActive(false);
        // 플레이어들에게 보여야 하는 UI
        Managers.UI.ShowSceneUI<UI_PlayerName>();
    }
    IEnumerator WaitPlayerLoded()
    {
        // 플레이어의 로딩을 기다립니다.
        bool allPlayersLoaded = false;
        while (!allPlayersLoaded)
        {
            int loadedPlayerCount = 0;
            for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
            {
                if (GameObject.Find($"Player{i + 1}")?.GetComponentInChildren<PlayerController>() != null)
                {
                    loadedPlayerCount++;
                }
            }

            // 모든 플레이어가 로드되었는지 확인
            allPlayersLoaded = loadedPlayerCount == PhotonNetwork.CurrentRoom.PlayerCount;

            yield return new WaitForSeconds(0.1f);
        }
        ShowUI();
    }

    public void onLoadedUI() //로딩이 다 된다음에 호출
    {
        if (PhotonNetwork.IsMasterClient)
        {
            for (int i = 1; i < 13; i++)
            {
                itemBox.Add(GameObject.Find($"@Obj_Root/Map/ItemBox/DestructibleObject{i}").GetComponent<Transform>());
            }
            for (int i = 0; i < itemBox.Count; i++)
            {
                if (PhotonNetwork.IsMasterClient)
                {
                    GameObject itembox = PhotonNetwork.Instantiate("Prefabs/yb/Object/DestructibleObject", itemBox[i].transform.position, Quaternion.identity);
                    itembox.transform.SetParent(_itemBox.transform);
                }
            }
        }

    }
}