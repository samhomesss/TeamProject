using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using yb;
using static Define;

// �̰͵� ��� �Ǵ� ��ũ��Ʈ
public class ItemInfoName : UI_Scene
{
    //#region 04.11 Test �÷��̾� ����
    //PlayerController _player2;
    //#endregion

    //public static GameObject Item => _item; // ������ �гο� ������ ���� ���
    //public static int Count
    //{
    //    get {return _count; }
    //    set { _count = value; }
    //}
    ////public static float DiffFloat
    ////{
    ////    get { return _diffFloat; }
    ////    set { _diffFloat = value; }
    ////}
    //UI_RelicInven uI_RelicInven;
    //public static GameObject ItemNameObject => _itemNameObject;

    //static GameObject _itemNameObject; // �ʱ�ȭ ���� ���ְ� ������ �̸� ������Ʈ
    //#region �ּ�ó��
    //// GameObject panel = null; // ������ �Ծ����� �� �������� ��� panel
    //// ���� �� ���¿��� Ŭ�� �ϸ� ������ �ٲٰ� ���� �־��� �������� ��� ��Ŵ
    //// string itemName; // ��� ������ �̸�
    ////Text itemInfoTextUI; // UI���� ��� ������ �ؽ�Ʈ �̸�

    //// static float _diffFloat;
    ////float diff; // �Ÿ�
    ////public event Action<string> OnRelicGet; // ���� ������ ȹ�� ������
    ////public event Action<string> OnWeaponGet;// ���� ������ ȹ�� ������
    ////public event Action<string> OnitemGet; // �Ϲ� ������ �Ծ��� ��
    ////public event Action OnItemNotCloesed; // ������ ������ ������
    //#endregion
    //#region �̰� �г��� ���� �ٽ� ����
    //// public static event Action OnChangedItem; // �������� �ٲܶ� 
    //#endregion
    //static int _count;
    
    //static GameObject _item; // ������ �гο� ������ ���� ���

    //private void Start()
    //{
    //    uI_RelicInven = UI_RelicInven.UI_RelicInvens.GetComponent<UI_RelicInven>();
    //    _itemNameObject = gameObject;
    //    // �÷��̾ �÷��̾� ��Ʈ�ѷ� �޾Ƽ� ����� ���� �ɵ�?
    //   //_player2 = GameObject.Find("Player1").GetComponent<PlayerController>();
    //   // SetPlayer(_player2);
    //   // PlayerTestSh.OnItemCheacked += CloseByPlayer; // �̰� �κ� �ٲ� ��ߵ�
    //   // Managers.Input.GetItemEvent += IsClosedItem; // �̰� �κ� �ٲ� ��ߵ�  // FŰ �������� 
    //}

    //// �������� ������ �ְ� �������� �Ǵ�
    //// �ش� �������� ������ �־ Ư�� Ű�� �������� �Դ� �۾� 
    ////void  IsClosedItem()
    ////{
    ////    int itemcount = 0;

    ////    if (diff <= 3f)
    ////    { 
    ////        // ����ϴ� ���� ���� �κе��� �־� �̰� ���ָ� �ɵ�?
    ////        switch (gameObject.GetComponent<Item>().ItemID / 500)
    ////        {
    ////            case 0: // ��������(��)
    ////                OnWeaponGet?.Invoke(gameObject.GetComponent<Item>().ItemID);
    ////                Destroy(gameObject);
    ////                Destroy(itemNameObject);
    ////                //PlayerTestSh.OnItemCheacked -= CloseByPlayer; // �÷��̾� ��ó�� ������ ���°� 
    ////                //Managers.Input.GetItemEvent -= IsClosedItem; // ������ �Դ°� 
    ////                break;
    ////            case 1: // �Ϲ� ������
    ////                OnitemGet?.Invoke(gameObject.GetComponent<Item>().ItemID);
    ////                Destroy(gameObject);
    ////                Destroy(itemNameObject);
    ////                //PlayerTestSh.OnItemCheacked -= CloseByPlayer; // �÷��̾� ��ó�� ������ ���°� 
    ////                //Managers.Input.GetItemEvent -= IsClosedItem; // ������ �Դ°� 
    ////                break;
    ////            case 2: // ���� ������
    ////                for (int i = 0; i < UI_RelicInven.UI_RelicInven_Items.Count; i++)
    ////                {
    ////                    if (!UI_RelicInven.UI_RelicInven_Items[i].IsEmpty)
    ////                    {
    ////                        #region �г��� �̿��ؼ� �������� ���¸� ���� â
    ////                        //itemcount++;
    ////                        // if (itemcount == 2)
    ////                        // {
    ////                        // _count = itemcount; // �ش� count�� �׳� ����Ǹ鼭 ���� ���� ���� �ϳ� �� ����
    ////                        // _item = gameObject;
    ////                        #region �г��� �����ͼ� �ؾ� �ɵ�?
    ////                        // Managers.UI.ShowSceneUI<UI_ItemChangePanel>();
    ////                        // UI_ItemChangePanel.OnChangedItem?.Invoke();
    ////                        // UI_RelicInven_Item.OnChangedItem += DestroyAction;

    ////                        #endregion
    ////                        //}
    ////                        // ������ ��á���� �ٲܲ��� �˾�â ����� �����ؼ� ���� 
    ////                        // �Ծ��� �ƴϸ� ���� ���� ������ ������
    ////                        #endregion
    ////                        continue;
    ////                    }
    ////                    OnRelicGet?.Invoke(gameObject.GetComponent<Item>().ItemID);
    ////                    Destroy(gameObject);
    ////                    Destroy(itemNameObject);
    ////                    //PlayerTestSh.OnItemCheacked -= CloseByPlayer; // �÷��̾� ��ó�� ������ ���°� 
    ////                    //Managers.Input.GetItemEvent -= IsClosedItem; // ������ �Դ°� 
    ////                    break;
    ////                }
    ////                break;
               
    ////        }
    ////    }
    ////}

    //void PickUpRelic(string ItemID)
    //{
    //    if (ItemID == "ShieldRelic" || ItemID == "BonusAttackSpeedRelic"|| ItemID == "BonusProjectileRelic" 
    //        || ItemID == "BonusResurrectionTimeRelic" || ItemID == "GuardRelic")
    //    {
    //       //for (int i = 0; i < uI_RelicInven.UI_RelicInven_Items.Count; i++)
    //       //{
    //       //    if (!uI_RelicInven.UI_RelicInven_Items[i].IsEmpty)
    //       //        continue;
    //       //
    //       //    //OnRelicGet?.Invoke(gameObject.name);
    //       //   // Destroy(gameObject);
    //       //    //Destroy(itemNameObject);
    //       //   // PlayerTestSh.OnItemCheacked -= CloseByPlayer; // �÷��̾� ��ó�� ������ ���°� 
    //       //   // Managers.Input.GetItemEvent -= IsClosedItem; // ������ �Դ°� 
    //       //    break;
    //       //}
    //    }
    //}

    //void PickUpItem(string ItemID)
    //{
    //    #region �ּ�ó��
    //    //if (ItemID / 500 == 1)
    //    //{
    //    //    OnitemGet?.Invoke(ItemID);
    //    //    Destroy(gameObject);
    //    //    Destroy(itemNameObject);
    //    //    PlayerTestSh.OnItemCheacked -= CloseByPlayer; // �÷��̾� ��ó�� ������ ���°� 
    //    //    Managers.Input.GetItemEvent -= IsClosedItem; // ������ �Դ°� 
    //    //}
    //    #endregion
    //}

    //void PickUpWeapon(string ItemID)
    //{
    //    if (ItemID == "Pistol" || ItemID == "Rifle" || ItemID == "Shotgun")
    //    {
    //        #region �ּ�ó��
    //        //OnWeaponGet?.Invoke(ItemID);
    //        // Destroy(gameObject);
    //        // Destroy(itemNameObject);
    //        //PlayerTestSh.OnItemCheacked -= CloseByPlayer; // �÷��̾� ��ó�� ������ ���°� 
    //        // Managers.Input.GetItemEvent -= IsClosedItem; // ������ �Դ°� 
    //        #endregion
    //    }
    //}
    /////*
    ////#region �ּ�ó�� �Ⱦ��� �Լ�
    ////// ��ǻ� �Ⱦ��� �Լ�
    //////void DestroyAction()
    //////{
    //////   // PlayerTestSh.OnItemCheacked -= CloseByPlayer; // �÷��̾� ��ó�� ������ ���°� 
    //////    Managers.Input.GetItemEvent -= IsClosedItem; // ������ �Դ°�
    //////    Destroy(itemNameObject);
    //////}
    ////#endregion
    ////// ������ �� ������ �־ �������� �Դ� �۾�
    /////*
    ////void CloseByPlayer()
    ////{
    ////    // �Ÿ� ����� �÷��̾� ���� �ϴ°� ���� ����
    ////    diff = Vector3.Distance(Map.Player.transform.position, gameObject.transform.position);
    ////    _diffFloat = diff;
    ////    if (diff <= 3f)
    ////    {
    ////        if (itemNameObject != null )
    ////        {
    ////            return;
    ////        }
    ////        else
    ////        {
    ////            // Todo: �ش� ������Ʈ ���� ���°� ���� �ؾߵ�
    ////            itemNameObject = Managers.Resources.Instantiate($"sh/UI/Scene/UI_ItemName"); // ������ ���� 
    ////            Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, gameObject.transform.position); // ���⼭ ����� �����°� �ƴϰ� 
    ////            RectTransform mainCanvasRect = itemNameObject.GetComponent<RectTransform>();
    ////            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(mainCanvasRect, screenPoint, null, out Vector2 localPoint)) // �ش� ĵ�������� � ��ġ�� �ִ��� ã�ƾߵ�
    ////            {
    ////                for (int ix = 0; ix < itemNameObject.transform.childCount; ++ix)
    ////                {
    ////                    var child = itemNameObject.transform.GetChild(ix);
    ////                    if (child.name.Equals("Item"))
    ////                    {
    ////                        child.GetComponent<RectTransform>().anchoredPosition = localPoint + new Vector2(0f, 80f);
    ////                        break;
    ////                    }
    ////                }
    ////            }

    ////            itemInfoTextUI = itemNameObject.transform.GetChild(0).GetChild(1).GetComponent<Text>();
    ////            itemName = Managers.ItemDataBase.GetItemData(gameObject.GetComponent<Item>().ItemID).itemName.ToString();

    ////            itemInfoTextUI.text = itemName; // �������� �̸� ���� 

              
    ////        }
    ////    }
    ////    else
    ////    {
    ////        if (itemNameObject != null)
    ////        {
    ////            if (UI_ItemChangePanel.ItemChagnePanel != null)
    ////            {
    ////                Destroy(UI_ItemChangePanel.ItemChagnePanel);
    ////                Count = 0;
    ////                UI_RelicInven_Item.IsChanged = false;

    ////            }
    ////            Destroy(itemNameObject);
    ////        }
    ////    }

    ////}
    ////*/
    ////// �������� Action ����
    ////// ������ �Ǿ� �ִ� �����̰� 
    
    ////void SetPlayer(PlayerController player)
    ////{
    ////    //player.ClosedItemEvent = null;
    ////   // player.ClosedItemEvent += CloseByPlayer;
    ////    //player.WeaponEvent += PickUpWeapon;
    ////    //player.SetRelicEvent += PickUpRelic;
    ////    //player.ItemEvent += PickUpItem; // �Ⱦ� ������ �� ���� �� -> string �����ε� ���� ��������� ����
    ////}
    
}
