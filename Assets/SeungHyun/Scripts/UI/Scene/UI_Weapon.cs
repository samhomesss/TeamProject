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

    Map map;
    GameObject mainWeapon;
    Image _mainWeaponImage;
    Text _bulletText;
    bool isReload = false; // ���������� �ƴ���
    int _maxBullet = 15;
    int _bulletCount = 60;

    string slotItemID = "Obtainable"+Define.WeaponType.Pistol.ToString(); // ó���� �ǽ���� ���� ���ְ�
    private void Start()
    {
        Debug.Log(slotItemID + "�Դϴ�");
        map = Map.MapObject.GetComponent<Map>();
        Init();
    }
    public override void Init()
    {
        base.Init();
        Bind<GameObject>(typeof(GameObjects));
        mainWeapon = GetObject((int)GameObjects.MainWeapon);
        GameObject BulletText = GetObject((int)GameObjects.BulletText);
        Debug.Log(map.Player[0].name);
        SetPlayer(map.Player[0]);
        mainWeapon.transform.GetChild(0).GetComponentInChildren<Image>().sprite = Managers.ItemDataBase.GetItemData(slotItemID).itemImage;
        _mainWeaponImage = mainWeapon.GetComponentInChildren<Image>();
        _bulletText = BulletText.GetComponent<Text>();
        _bulletText.text = "15 / 60";
    }
    
    void ChangeWeapon(string itemID)
    {
        string beforeItemID = slotItemID; // �ϴ� �ٲ� �ֱ� ���� ������ ��ȣ �����ϰ� 
        mainWeapon.transform.GetChild(0).GetComponentInChildren<Image>().sprite = Managers.ItemDataBase.GetItemData("Obtainable"+itemID).itemImage;
        _mainWeaponImage = mainWeapon.GetComponentInChildren<Image>(); // �ܼ� ������Ʈ 
        GameObject go = Managers.Resources.Instantiate($"sh/Weapon/{Managers.ItemDataBase.GetItemData(beforeItemID).itemName}"); // ������ ����
        go.transform.position = map.Player[0].transform.position;
        Debug.Log(go.name);
        slotItemID = "Obtainable"+itemID;
    }

    // �Ѿ� �����°�
    void BulletCount(int bulletnum , int maxBullet)
    {
        //if (!isReload)
        //{
        //    _bulletCount -= bulletnum;
        //    _maxBullet = maxBullet;
        //    if (_bulletCount == 0)
        //    {
        //        StartCoroutine("ReloadBullet");
        //    }
            
        //}
        _bulletText.text = $"{bulletnum} / {maxBullet}";


    }
    //// ���� ���
    //IEnumerator ReloadBullet()
    //{
    //    isReload = true;
    //    _bulletCount = 50;
    //    yield return new WaitForSeconds(2f);
    //    isReload = false;
    //}
    void SetPlayer(PlayerController player)
    {
        player.BulletEvent += BulletCount;
        player.WeaponEvent += ChangeWeapon;
    }
}



