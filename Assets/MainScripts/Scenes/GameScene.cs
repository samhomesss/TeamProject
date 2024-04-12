using Photon.Pun;
using UnityEngine;
using UnityEngine.Events;
using yb;

public class GameScene : BaseScene
{
    private PhotonView _photonView;
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
            
            go.GetComponentInChildren<PlayerController>().SetRelicEvent += OnSetRelic; //

            if (PhotonNetwork.IsMasterClient)
            {
                GameObject guardRelic = PhotonNetwork.Instantiate("Prefabs/yb/Relic/GuardRelic", new Vector3(2, 1, 10), Quaternion.identity);
                GameObject shieldRelic = PhotonNetwork.Instantiate("Prefabs/yb/Relic/ShieldRelic", new Vector3(10, 1, 2), Quaternion.identity);
                GameObject bonusAttackSpeedRelic = PhotonNetwork.Instantiate("Prefabs/yb/Relic/BonusAttackSpeedRelic", new Vector3(1, 1, 2), Quaternion.identity);
                GameObject BonusProjectileRelic = PhotonNetwork.Instantiate("Prefabs/yb/Relic/BonusProjectileRelic", new Vector3(1, 1, 4), Quaternion.identity);
                GameObject BonusResurrectionTimeRelic = PhotonNetwork.Instantiate("Prefabs/yb/Relic/BonusResurrectionTimeRelic", new Vector3(1, 1, 6), Quaternion.identity);
            }
            _photonView = Util.FindChild(go, "Model").GetComponent<PhotonView>();

            if (_photonView.IsMine)
            {
                Util.FindChild(go, "Camera", true).active = true;
                Util.FindChild(go, "Camera", true).GetComponent<AudioListener>().enabled = true;

                _photonView.RPC("RenamePlayer", RpcTarget.All, _photonView.ViewID);
            }
            
        }

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

        //GameObject
        Managers.SceneObj.ShowSceneObject<Map>();
        Managers.SceneObj.ShowSceneObject<MiniMapCam>();
    }


}