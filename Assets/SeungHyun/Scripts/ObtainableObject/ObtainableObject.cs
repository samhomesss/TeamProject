using Photon.Pun;
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

    public virtual void Pickup(PlayerController player) { }

    public virtual void ShowName(PlayerController player)
    {
        if (itemNameObject != null)
        {
            return;
        }
        else
        {
            itemNameObject = Managers.Resources.Instantiate($"sh/UI/Scene/UI_ItemName"); // 아이템 생성 
            Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(player.MyCamera, gameObject.transform.position); // 여기서 계산이 끝나는게 아니고 
            RectTransform mainCanvasRect = itemNameObject.GetComponent<RectTransform>();
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(mainCanvasRect, screenPoint, null, out Vector2 localPoint)) // 해당 캔버스에서 어떤 위치에 있는지 찾아야됨
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
             //itemName = Managers.ItemDataBase.GetItemData(gameObject.GetComponent<Item>().ItemID).itemName.ToString();
             itemInfoTextUI.text = Name; // 아이템의 이름 적용 
        }
    }
    public virtual void HideName()
    {
        if (itemNameObject != null)
            Destroy(itemNameObject);
    }

    public virtual void PickupPhoton(int playerViewId)
    {

    }

    [PunRPC]
    public void SetDropItemName(int dropObjectViewId)//0416 이희웅 포톤 드랍아이템 이름재지정
    {
        _photonView = PhotonNetwork.GetPhotonView(dropObjectViewId);

        int index = _photonView.transform.gameObject.name.IndexOf("(Clone)");
        if (index > 0)
            _photonView.transform.gameObject.name = _photonView.transform.gameObject.name.Substring(0, index);
    }
    [PunRPC]
    public void DropItem(int PhotonViewID, int PlayerPhotonViewID)
    {
        _photonView = PhotonNetwork.GetPhotonView(PhotonViewID);
        PhotonView playerPhoton = PhotonNetwork.GetPhotonView(PlayerPhotonViewID);
        _photonView.gameObject.transform.position = playerPhoton.transform.position + Vector3.up;
        GameObject relicObj = _photonView.gameObject;
        IRelic go = relicObj.GetComponent<IRelic>();
        if(playerPhoton.IsMine)
        go.DeleteRelic(playerPhoton.GetComponent<PlayerController>());
    }
}
