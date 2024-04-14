using Photon.Pun;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Events;
using yb;

public class GameScene : BaseScene
{
    private PhotonView _photonView;

    [SerializeField] private PlayerController[] _playerControllers;
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
        //todo
        if (IsTestMode.Instance.CurrentUser == Define.User.Hw)
        {
            GameObject go = PhotonNetwork.Instantiate("Prefabs/hw/PlayerPrefabs/Player",Vector3.zero, Quaternion.identity);
            go.GetComponentInChildren<PlayerController>().SetRelicEvent += OnSetRelic; //
            if (PhotonNetwork.IsMasterClient)
            {
                GameObject guardRelic = PhotonNetwork.Instantiate("Prefabs/yb/Relic/GuardRelic", new Vector3(2, 1, 10), Quaternion.identity);
                GameObject shieldRelic = PhotonNetwork.Instantiate("Prefabs/yb/Relic/ShieldRelic", new Vector3(10, 1, 2), Quaternion.identity);
                GameObject bonusAttackSpeedRelic = PhotonNetwork.Instantiate("Prefabs/yb/Relic/BonusAttackSpeedRelic", new Vector3(1, 1, 2), Quaternion.identity);
                GameObject bonusProjectileRelic = PhotonNetwork.Instantiate("Prefabs/yb/Relic/BonusProjectileRelic", new Vector3(1, 1, 4), Quaternion.identity);
                GameObject bonusResurrectionTimeRelic = PhotonNetwork.Instantiate("Prefabs/yb/Relic/BonusResurrectionTimeRelic", new Vector3(1, 1, 6), Quaternion.identity);
                guardRelic.name = "GuardRelic";
                shieldRelic.name = "ShieldRelic";
                bonusAttackSpeedRelic.name = "BonusAttackSpeedRelic";
                bonusProjectileRelic.name = "BonusProjectileRelic";
                bonusResurrectionTimeRelic.name = "BonusResurrectionTimeRelic";
                _playerControllers = new PlayerController[PhotonNetwork.CountOfPlayers];
            }
            _photonView = Util.FindChild(go, "Model").GetComponent<PhotonView>();
            if (_photonView.IsMine)
            {
                Util.FindChild(go, "Camera", true).active = true;
                Util.FindChild(go, "Camera", true).GetComponent<AudioListener>().enabled = true;
                _photonView.RPC("RenamePlayer", RpcTarget.All, _photonView.ViewID);
            }
        }

        Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount);
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

        // 플레이어들에게 보여야 하는 UI
        Managers.UI.ShowSceneUI<UI_PlayerName>();

        //GameObject
        Managers.SceneObj.ShowSceneObject<Map>();
        Managers.SceneObj.ShowSceneObject<MiniMapCam>();

    }


}