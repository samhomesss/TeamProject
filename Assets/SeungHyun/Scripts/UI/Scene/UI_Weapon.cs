using ExitGames.Client.Photon;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using yb;


public class UI_Weapon : UI_Scene
{
    PhotonView _photonView;
    enum GameObjects
    {
        MainWeapon,
        BulletText,
    }

    Map map;
    GameObject mainWeapon;
    Image _mainWeaponImage;
    Text _bulletText;
    bool isReload = false; // 장전중인지 아닌지
    int _maxBullet = 15;
    int _bulletCount = 60;


    string slotItemID = Define.WeaponType.Pistol.ToString(); // 처음은 피스톨로 설정 해주고
    private void Start()
    {
        //Debug.Log(slotItemID + "입니다");
        Init();
    }
    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));
        mainWeapon = GetObject((int)GameObjects.MainWeapon);
        GameObject BulletText = GetObject((int)GameObjects.BulletText);

        map = Map.MapObject.GetComponent<Map>();
        _photonView = GameObject.Find($"Player{PhotonNetwork.LocalPlayer.ActorNumber}").GetComponentInChildren<PhotonView>();

        if (_photonView.IsMine)
            SetPlayer(map.Player);

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

        if (IsTestMode.Instance.CurrentUser == Define.User.Hw)
        {

            map.Player.PhotonView.RPC("Replacedweapon",RpcTarget.All, beforeItemID);


            //GameObject go = PhotonNetwork.Instantiate($"Prefabs/yb/Weapon/{Managers.ItemDataBase.GetItemData(beforeItemID).itemName}", Vector3.zero, Quaternion.identity);

            //if (_photonView.IsMine)
            //{
            //    map.Player.PhotonView.RPC("SetDropItemName", RpcTarget.All, go.GetComponent<PhotonView>().ViewID);//이름 바꾸기
            //    go.transform.position = map.Player.transform.position + Vector3.up;
            //}
        }
        else
        {
            GameObject go = Managers.Resources.Instantiate($"sh/Weapon/{Managers.ItemDataBase.GetItemData(beforeItemID).itemName}"); // 아이템 생성
            if (_photonView.IsMine)
                go.transform.position = map.Player.transform.position;
        }
        slotItemID = itemID;
    }

    // 총알 나가는거
    void BulletCount(int bulletnum, int maxBullet)
    {
        #region 주석
        //if (!isReload)
        //{
        //    _bulletCount -= bulletnum;
        //    _maxBullet = maxBullet;
        //    if (_bulletCount == 0)
        //    {
        //        StartCoroutine("ReloadBullet");
        //    }

        //}
        #endregion
        _bulletText.text = $"{bulletnum} / {maxBullet}";


    }
    //// 장전 모션
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



