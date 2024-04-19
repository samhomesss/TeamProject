using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using yb;

public class GameScene : BaseScene
{
    const int MAX_PLAYER = 8;
    const int ITEM_BOX_PCS = 13;
    private PhotonView _photonView;
    private List<Transform> itemBox = new List<Transform>();//파라미터는 박스의 갯수
    public UnityEvent OnLoaded;
    private WaitForSeconds waitObject = new WaitForSeconds(0.1f);
    private Transform playerRespawnPointTransform;
    private PhotonView _itemBox_transform_Photonview;


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
        base.Init();
        if (IsTestMode.Instance.CurrentUser == Define.User.Hw)
        {
            StartCoroutine(RespawnPlayers());


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

    // 작성자: 장세윤(2024.04.19).
    // 플레이어를 리스폰 하는 함수.
    // 레벨이 준비될 때까지 대기해야 하기 때문에 모든 플레이어가 생성될 때까지 대기한 후에 리스폰 하도록 구현.
    private IEnumerator RespawnPlayers()
    {
        // 플레이어 GO 생성.
        GameObject go = PhotonNetwork.Instantiate($"Prefabs/hw/PlayerPrefabs/Player{PhotonNetwork.LocalPlayer.ActorNumber}", Vector3.zero, Quaternion.identity);

        // 레벨에 포톤에 등록된 모든 플레이어가 생성될 때까지 대기.
        yield return StartCoroutine(WaitPlayerLoded());

        // 리스폰 위치 가져오기.
        Util.FindChild(go, "Model").GetComponent<Transform>().position = playerRespawnPointTransform.GetChild(PhotonNetwork.LocalPlayer.ActorNumber - 1).position;
        go.GetComponentInChildren<PlayerController>().SetRelicEvent += OnSetRelic;

        // 위치 변경.
        _photonView = Util.FindChild(go, "Model").GetComponent<PhotonView>();
        if (_photonView.IsMine)
        {
            Util.FindChild(go, "Camera", true).SetActive(true);
            Util.FindChild(go, "Camera", true).GetComponent<AudioListener>().enabled = true;
            _photonView.RPC("RenamePlayer", RpcTarget.All, _photonView.ViewID);
        }

    }

    void ShowUI()
    {
        //GameObject
        Map map = Managers.SceneObj.ShowSceneObject<Map>();
        if (map != null)
        {
            map.onLoadMapUI += OnLoadedItemBox;
        }

        Managers.SceneObj.ShowSceneObject<MiniMapCam>();
        Managers.UI.ShowSceneUI<UI_Weapon>();
        Managers.UI.ShowSceneUI<UI_Inven>();
        Managers.UI.ShowSceneUI<UI_Hp>();
        Managers.UI.ShowSceneUI<UI_MiniMap>();
        Managers.UI.ShowSceneUI<UI_RelicInven>();
        Managers.UI.ShowSceneUI<UI_PlayerColorPercent>();
        Managers.UI.ShowSceneUI<UI_Timer>();
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
        bool isSpwanPointLoaded = false;
        while (!allPlayersLoaded || !isSpwanPointLoaded)
        {
            allPlayersLoaded = FindObjectsByType<PlayerController>(FindObjectsSortMode.None).Length == PhotonNetwork.CurrentRoom.PlayerCount;
            isSpwanPointLoaded = RespawnManager.Instance;
            yield return waitObject;
        }
        playerRespawnPointTransform = RespawnManager.Instance.RespawnPoints;
        ShowUI();
    }


    public void OnLoadedItemBox() //로딩이 다 된다음에 호출
    {
        for (int i = 1; i < ITEM_BOX_PCS; i++)
        {
            itemBox.Add(GameObject.Find($"@Obj_Root/Map/ItemBox/DestructibleObject{i}").GetComponent<Transform>());
        }
        if (PhotonNetwork.IsMasterClient)
        {
            _photonView.RPC("SetItemBox", RpcTarget.All, ITEM_BOX_PCS);
            //GameObject itembox = PhotonNetwork.Instantiate("Prefabs/yb/Object/DestructibleObject", itemBox[i].transform.position, Quaternion.identity);
            //itembox.transform.SetParent(_itemBox.transform);
        }
    }
}