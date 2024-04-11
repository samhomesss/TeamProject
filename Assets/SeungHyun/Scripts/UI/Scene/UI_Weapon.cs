using ExitGames.Client.Photon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using yb;


public class UI_Weapon : UI_Scene
{
    enum GameObjects
    {
        MainWeapon,
        BulletText,
    }

    public static Image MainWeaponImage => _mainWeaponImage; // ���� �� �̹���
    Map map;
    GameObject mainWeapon;
    // ���߿� �̹��� �ٲܶ� ���
    static Image _mainWeaponImage;
    Text _bulletText;
    bool isReload = false; // ���������� �ƴ���
    // �Ѿ˵� ���� �س��� ��ġ�� ���� �ִ��� Ȯ���ϰ� ���� �ؾ� �� ��?
    int _maxBullet = 50;
    int _bulletCount = 50;

    int slotItemID = 50; // ó���� �ǽ���� ���� ���ְ�
    private void Start()
    {
        Init();
        map = Map.MapObject.GetComponent<Map>();
    }
    public override void Init()
    {
        base.Init();
        Bind<GameObject>(typeof(GameObjects));
        mainWeapon = GetObject((int)GameObjects.MainWeapon);
        GameObject BulletText = GetObject((int)GameObjects.BulletText);
        // �Ѿ� ������ �� ���� ���ְ�
        // Todo : �������� �Ѿ� �����°� ����ٰ� ���ָ� �ǰ�
        //Managers.Input.BulletReduce -= BulletCount;
        //Managers.Input.BulletReduce += BulletCount;
        mainWeapon.transform.GetChild(0).GetComponentInChildren<Image>().sprite = Managers.ItemDataBase.GetItemData(slotItemID).itemImage;
        _mainWeaponImage = mainWeapon.GetComponentInChildren<Image>();
        _bulletText = BulletText.GetComponent<Text>();
        _bulletText.text = $"{_bulletCount} / {_maxBullet}";

        ItemInfoName.OnWeaponGet -= ChangeWeapon;
        ItemInfoName.OnWeaponGet += ChangeWeapon;
    }
    
    void ChangeWeapon(int itemID)
    {
        int beforeItemID = slotItemID; // �ϴ� �ٲ� �ֱ� ���� ������ ��ȣ �����ϰ� 
        mainWeapon.transform.GetChild(0).GetComponentInChildren<Image>().sprite = Managers.ItemDataBase.GetItemData(itemID).itemImage;
        _mainWeaponImage = mainWeapon.GetComponentInChildren<Image>(); // �ܼ� ������Ʈ 
        GameObject go = Managers.Resources.Instantiate($"sh/Weapon/{Managers.ItemDataBase.GetItemData(beforeItemID).itemName}"); // ������ ����
        go.transform.position = map.Player.transform.position;
        slotItemID = itemID;
    }


    // �Ѿ� �����°�
    void BulletCount(int bulletnum , int maxBullet)
    {
        if (!isReload)
        {
            _bulletCount -= bulletnum;
            _maxBullet = maxBullet;
            if (_bulletCount == 0)
            {
                StartCoroutine("ReloadBullet");
            }
            _bulletText.text = $"{_bulletCount} / {_maxBullet}";
        }
       
        
    }
    // ���� ���
    IEnumerator ReloadBullet()
    {
        isReload = true;
        _bulletCount = 50;
        yield return new WaitForSeconds(2f);
        isReload = false;
    }

    void SetPlayer(PlayerController player)
    {
        player.BulletEvent -= BulletCount;
        player.BulletEvent += BulletCount;
    }


}



