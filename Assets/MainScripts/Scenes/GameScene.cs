using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using yb;

public class GameScene : BaseScene
{
    private PhotonView _photonView;
    private List<Transform> itemBox = new List<Transform>();//파라미터는 박스의 갯수
    public UnityEvent OnLoaded;
    private WaitForSeconds waitObject = new WaitForSeconds(0.1f);

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
            GameObject go = PhotonNetwork.Instantiate($"Prefabs/hw/PlayerPrefabs/Player{PhotonNetwork.LocalPlayer.ActorNumber}", Vector3.zero, Quaternion.identity);
            StartCoroutine(WaitPlayerLoded());
            go.GetComponentInChildren<PlayerController>().SetRelicEvent += OnSetRelic;

            _photonView = Util.FindChild(go, "Model").GetComponent<PhotonView>();
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
            map.onLoadMapUI += OnLoadedItemBox;
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
            allPlayersLoaded = FindObjectsByType<PlayerController>(FindObjectsSortMode.None).Length == PhotonNetwork.CurrentRoom.PlayerCount;
            yield return waitObject;
        }

        ShowUI();
    }

    public void OnLoadedItemBox() //로딩이 다 된다음에 호출
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