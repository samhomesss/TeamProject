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
    private List<Transform> itemBox = new List<Transform>();//�Ķ���ʹ� �ڽ��� ����
    public UnityEvent OnLoaded;
    private WaitForSeconds waitObject = new WaitForSeconds(0.1f);
    private Transform playerRespawnPointTransform;
    private PhotonView _itemBox_transform_Photonview;


    public override void Clear()
    {
    }

    void OnSetRelic(string itemID, UnityAction setRelicAction, UnityAction destroyRelicAction) //����� 0412 18:00 �߰� �������� ������ �̺�Ʈ�� ó��
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

    // �ۼ���: �弼��(2024.04.19).
    // �÷��̾ ������ �ϴ� �Լ�.
    // ������ �غ�� ������ ����ؾ� �ϱ� ������ ��� �÷��̾ ������ ������ ����� �Ŀ� ������ �ϵ��� ����.
    private IEnumerator RespawnPlayers()
    {
        // �÷��̾� GO ����.
        GameObject go = PhotonNetwork.Instantiate($"Prefabs/hw/PlayerPrefabs/Player{PhotonNetwork.LocalPlayer.ActorNumber}", Vector3.zero, Quaternion.identity);

        // ������ ���濡 ��ϵ� ��� �÷��̾ ������ ������ ���.
        yield return StartCoroutine(WaitPlayerLoded());

        // ������ ��ġ ��������.
        Util.FindChild(go, "Model").GetComponent<Transform>().position = playerRespawnPointTransform.GetChild(PhotonNetwork.LocalPlayer.ActorNumber - 1).position;
        go.GetComponentInChildren<PlayerController>().SetRelicEvent += OnSetRelic;

        // ��ġ ����.
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
        // �÷��̾�鿡�� ������ �ϴ� UI
        Managers.UI.ShowSceneUI<UI_PlayerName>();
    }
    IEnumerator WaitPlayerLoded()
    {
        // �÷��̾��� �ε��� ��ٸ��ϴ�.
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


    public void OnLoadedItemBox() //�ε��� �� �ȴ����� ȣ��
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