using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using yb;

// ���� �߰� ���� �θ� Ŭ������ ����
public class ObtainableObject : MonoBehaviourPunCallbacks, IObtainableObject, IObtainableObjectPhoton
{

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
            itemNameObject = Managers.Resources.Instantiate($"sh/UI/Scene/UI_ItemName"); // ������ ���� 
            Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(player.MyCamera, gameObject.transform.position); // ���⼭ ����� �����°� �ƴϰ� 
            RectTransform mainCanvasRect = itemNameObject.GetComponent<RectTransform>();
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(mainCanvasRect, screenPoint, null, out Vector2 localPoint)) // �ش� ĵ�������� � ��ġ�� �ִ��� ã�ƾߵ�
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
             itemInfoTextUI.text = Name; // �������� �̸� ���� 
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
}
