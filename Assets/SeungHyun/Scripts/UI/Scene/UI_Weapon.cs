using ExitGames.Client.Photon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_Weapon : UI_Scene
{
    enum GameObjects
    {
        MainWeapon,
        BulletText,
    }

    public static Image MainWeaponImage => _mainWeaponImage; // 메인 총 이미지
    GameObject mainWeapon;
    // 나중에 이미지 바꿀때 사용
    static Image _mainWeaponImage;
    Text _bulletText;
    bool isReload = false; // 장전중인지 아닌지
    // 총알도 설정 해놓은 수치가 따로 있는지 확인하고 설정 해야 할 듯?
    int _maxBullet = 50;
    int _bulletCount = 50;

    int slotItemID = 50; // 처음은 피스톨로 설정 해주고
    private void Start()
    {
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
        int beforeItemID = slotItemID; // 일단 바꿔 주기 전에 아이템 번호 저장하고 
        mainWeapon.transform.GetChild(0).GetComponentInChildren<Image>().sprite = Managers.ItemDataBase.GetItemData(itemID).itemImage;
        _mainWeaponImage = mainWeapon.GetComponentInChildren<Image>(); // 단순 업데이트 
        GameObject go = Managers.Resources.Instantiate($"sh/Weapon/{Managers.ItemDataBase.GetItemData(beforeItemID).itemName}"); // 아이템 생성
        go.transform.position = Map.Player.transform.position;
        slotItemID = itemID;
    }


    // 총알 나가는거
    void BulletCount(int bulletnum)
    {
        if (!isReload)
        {
            _bulletCount -= bulletnum;
            if (_bulletCount == 0)
            {
                StartCoroutine("ReloadBullet");
            }
            _bulletText.text = $"{_bulletCount} / {_maxBullet}";
        }
       
        
    }
    // 장전 모션
    IEnumerator ReloadBullet()
    {
        isReload = true;
        _bulletCount = 50;
        yield return new WaitForSeconds(2f);
        isReload = false;
    }
}



