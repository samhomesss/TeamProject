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

    //public static Image MainWeaponImage => _mainWeaponImage; // 메인 총 이미지
    //ItemInfoName itemInfoName;
    Map map;
    GameObject mainWeapon;
    // 나중에 이미지 바꿀때 사용
    Image _mainWeaponImage;
    Text _bulletText;
    bool isReload = false; // 장전중인지 아닌지
    // 총알도 설정 해놓은 수치가 따로 있는지 확인하고 설정 해야 할 듯?
    int _maxBullet = 15;
    int _bulletCount = 60;

    string slotItemID = Define.WeaponType.Pistol.ToString(); // 처음은 피스톨로 설정 해주고
    private void Start()
    {
        Debug.Log(slotItemID + "입니다");
        map = Map.MapObject.GetComponent<Map>();
        //itemInfoName = ItemInfoName.ItemNameObject.GetComponent<ItemInfoName>();
        Init();
    }
    public override void Init()
    {
        base.Init();
        Bind<GameObject>(typeof(GameObjects));
        mainWeapon = GetObject((int)GameObjects.MainWeapon);
        GameObject BulletText = GetObject((int)GameObjects.BulletText);
        // 총알 나가는 거 구독 해주고
        // Todo : 윤범이형 총알 나가는거 여기다가 해주면 되고
        SetPlayer(map.Player);
        //Managers.Input.BulletReduce -= BulletCount;
        //Managers.Input.BulletReduce += BulletCount;
        mainWeapon.transform.GetChild(0).GetComponentInChildren<Image>().sprite = Managers.ItemDataBase.GetItemData(slotItemID).itemImage;
        _mainWeaponImage = mainWeapon.GetComponentInChildren<Image>();
        _bulletText = BulletText.GetComponent<Text>();
        _bulletText.text = "15 / 60";
    }
    
    void ChangeWeapon(string itemID)
    {
        string beforeItemID = slotItemID; // 일단 바꿔 주기 전에 아이템 번호 저장하고 
        mainWeapon.transform.GetChild(0).GetComponentInChildren<Image>().sprite = Managers.ItemDataBase.GetItemData(itemID).itemImage;
        _mainWeaponImage = mainWeapon.GetComponentInChildren<Image>(); // 단순 업데이트 
        GameObject go = Managers.Resources.Instantiate($"sh/Weapon/{Managers.ItemDataBase.GetItemData(beforeItemID).itemName}"); // 아이템 생성
        go.transform.position = map.Player.transform.position;
        Debug.Log(go.name);
        slotItemID = itemID;
    }

    // 총알 나가는거
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
    // 장전 모션
    IEnumerator ReloadBullet()
    {
        isReload = true;
        _bulletCount = 50;
        yield return new WaitForSeconds(2f);
        isReload = false;
    }

    void SetPlayer(PlayerController player)
    {
        player.BulletEvent += BulletCount;
        player.WeaponEvent += ChangeWeapon;
    }


}



