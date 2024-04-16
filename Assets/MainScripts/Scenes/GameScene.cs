using Photon.Pun;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Events;
using yb;

public class GameScene : BaseScene
{
    private PhotonView _photonView;
    private GameObject[] items = new GameObject[6];//0415 18:33 ����� �׽�Ʈ�� �迭 �߰� 
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

        //GameObject
        Managers.SceneObj.ShowSceneObject<Map>();
        Managers.SceneObj.ShowSceneObject<MiniMapCam>();


        Managers.UI.ShowSceneUI<UI_Timer>();
        Managers.UI.ShowSceneUI<UI_Weapon>();
        Managers.UI.ShowSceneUI<UI_Inven>();
        Managers.UI.ShowSceneUI<UI_Hp>();
        Managers.UI.ShowSceneUI<UI_MiniMap>();
        Managers.UI.ShowSceneUI<UI_RelicInven>();
        Managers.UI.ShowSceneUI<UI_PlayerColorPercent>();
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

            // ��� �÷��̾ �ε�Ǿ����� Ȯ��
            allPlayersLoaded = loadedPlayerCount == PhotonNetwork.CurrentRoom.PlayerCount;

            yield return new WaitForSeconds(0.1f);
        }
        ShowUI();
    }

}