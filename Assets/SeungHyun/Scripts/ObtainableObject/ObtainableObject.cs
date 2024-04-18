using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using yb;
using static Define;

// 새로 추가 승현 부모 클래스로 만듬
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
        if (itemNameObject != null) // 현재 오브젝트가 있다면 
        {
            return;
        }
        else
        {
            // 생성부
            itemNameObject = Managers.Resources.Instantiate($"sh/UI/Scene/UI_ItemName"); // 아이템 생성 
            // 위치 조정
            screenPoint = RectTransformUtility.WorldToScreenPoint(player.MyCamera, gameObject.transform.position); // 여기서 계산이 끝나는게 아니고 
            mainCanvasRect = itemNameObject.GetComponent<RectTransform>();
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(mainCanvasRect, screenPoint, null, out Vector2 localPoint)) // 해당 캔버스에서 어떤 위치에 있는지 찾아야됨 Anchored 사용해서 사용할꺼면 RectTransformUtility 필요
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

            //텍스트 이름 변경 
             itemInfoTextUI.text = Name; // 아이템의 이름 적용 
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

    // 마스터 클라이언트에 아이템 삭제를 요청하는 함수.
    // 삭제가 필요한 아이템을 획득할 때 호출을 해줘야 함.
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
            //Util.LogGreen("SetItem 안에 Action 호출됨 -> Master Client");
            PhotonNetwork.Destroy(PhotonNetwork.GetPhotonView(objectID).gameObject);
            //PhotonNetwork.Destroy(destroyObject);
        }
        else
        {
            //Util.LogGreen("SetItem 안에 Action 호출됨 -> Not Master Client");
        }
    }

    [PunRPC]
    public void SetDropItemName(int dropObjectViewId)//0416 이희웅 포톤 드랍아이템 이름재지정
    {
        _photonView = PhotonNetwork.GetPhotonView(dropObjectViewId);

        int index = _photonView.transform.gameObject.name.IndexOf("(Clone)");
        if (index > 0)
            _photonView.transform.gameObject.name = _photonView.transform.gameObject.name.Substring(0, index);

        Debug.Log("셋드롭아이템이름 호출됨");
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
