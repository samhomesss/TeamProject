using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using yb;
using static Define;

// ���� �߰� ���� �θ� Ŭ������ ����
public class ObtainableObject : MonoBehaviourPunCallbacks, IObtainableObject, IObtainableObjectPhoton
{
    public Define.RelicType RelicType { get; } = Define.RelicType.BonusResurrectionTimeRelic;
    protected Map map => Map.MapObject.GetComponent<Map>();

    [SerializeField] GameObject itemNameObject;
    Text itemInfoTextUI;
    
    protected PhotonView _photonView;
    public string Name => gameObject.name;

    public PhotonView IObtainableObjectPhotonView => _photonView;

    public string NamePhoton => gameObject.name;

    Vector2 screenPoint;
    RectTransform mainCanvasRect;
    

    public virtual void Pickup(PlayerController player) { }

    public virtual void ShowName(PlayerController player)
    {
        if (itemNameObject != null) // ���� ������Ʈ�� �ִٸ� 
        {
            return;
        }
        else
        {
            // ������
            itemNameObject = Managers.Resources.Instantiate($"sh/UI/Scene/UI_ItemName"); // ������ ���� 
            // ��ġ ����
            screenPoint = RectTransformUtility.WorldToScreenPoint(player.MyCamera, gameObject.transform.position); // ���⼭ ����� �����°� �ƴϰ� 
            mainCanvasRect = itemNameObject.GetComponent<RectTransform>();
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(mainCanvasRect, screenPoint, null, out Vector2 localPoint)) // �ش� ĵ�������� � ��ġ�� �ִ��� ã�ƾߵ� Anchored ����ؼ� ����Ҳ��� RectTransformUtility �ʿ�
            {
                for (int ix = 0; ix < itemNameObject.transform.childCount; ++ix)
                {
                    var child = itemNameObject.transform.GetChild(ix);
                    if (child.name.Equals("Item"))
                    {
                        child.GetComponent<RectTransform>().anchoredPosition = localPoint + new Vector2(0f, 80f);
                        break;
                    }
                }
            }
             itemInfoTextUI = Util.FindChild(itemNameObject, "ItemName", true).GetComponent<Text>();

            //�ؽ�Ʈ �̸� ���� 
             itemInfoTextUI.text = Name; // �������� �̸� ���� 
        }
    }
    public virtual void HideName()
    {
        if (itemNameObject != null)
        {
            Destroy(itemNameObject);
        }

    }

    public virtual void PickupPhoton(int playerViewId)
    {

    }

    // ������ Ŭ���̾�Ʈ�� ������ ������ ��û�ϴ� �Լ�.
    // ������ �ʿ��� �������� ȹ���� �� ȣ���� ����� ��.
    [PunRPC]
    public void Replacedweapon(string beforeItemID)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            GameObject ChangeWeaponObject = PhotonNetwork.Instantiate($"Prefabs/yb/Weapon/{Managers.ItemDataBase.GetItemData(beforeItemID).itemName}", Vector3.zero, Quaternion.identity);
            ChangeWeaponObject.transform.position = map.Player.transform.position + Vector3.up;
            int index = ChangeWeaponObject.transform.gameObject.name.IndexOf("(Clone)");
            if (index > 0)
                ChangeWeaponObject.transform.gameObject.name = ChangeWeaponObject.transform.gameObject.name.Substring(0, index);
        }
    }

    [PunRPC]
    public void OnRequestPhotonDestroy(int objectID)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            //Util.LogGreen("SetItem �ȿ� Action ȣ��� -> Master Client");
            PhotonNetwork.Destroy(PhotonNetwork.GetPhotonView(objectID).gameObject);
            //PhotonNetwork.Destroy(destroyObject);
        }
        else
        {
            //Util.LogGreen("SetItem �ȿ� Action ȣ��� -> Not Master Client");
        }
    }

    [PunRPC]
    public void SetDropItemName(int dropObjectViewId)//0416 ����� ���� ��������� �̸�������
    {
        _photonView = PhotonNetwork.GetPhotonView(dropObjectViewId);

        int index = _photonView.transform.gameObject.name.IndexOf("(Clone)");
        if (index > 0)
            _photonView.transform.gameObject.name = _photonView.transform.gameObject.name.Substring(0, index);

        Debug.Log("�µ�Ӿ������̸� ȣ���");
    }
    [PunRPC]
    public void DropItem(int PhotonViewID, int PlayerPhotonViewID)
    {
        _photonView = PhotonNetwork.GetPhotonView(PhotonViewID);
        _photonView.TransferOwnership(PhotonNetwork.MasterClient.ActorNumber);
        PhotonView playerPhoton = PhotonNetwork.GetPhotonView(PlayerPhotonViewID);
        _photonView.gameObject.transform.position = playerPhoton.transform.position + Vector3.up;
        GameObject relicObj = _photonView.gameObject;
        IRelic go = relicObj.GetComponent<IRelic>();
        go.DeleteRelic(playerPhoton.GetComponent<PlayerController>());
    }
}
