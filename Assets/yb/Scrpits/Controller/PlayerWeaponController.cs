using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using yb;

/// <summary>
/// 플레이어 무기 관리 클래스
/// </summary>
namespace yb
{
    public class PlayerWeaponController : MonoBehaviour
    {
        private PlayerController _player;
        private IRangedWeapon _rangeWeapon;  //현재 플레이어가 장착중인 무기
        public IRangedWeapon RangedWeapon => _rangeWeapon;
        private Transform _rangedWeaponsParent;  //하이어라키 상에서 플레이어의 무기 오브젝트들의 부모 오브젝트
        public Transform RangedWeaponsParent => _rangedWeaponsParent;

        private PhotonView _photonview;//0409 08:06 이희웅 코드 수정 총알 동기화를 위한 포톤뷰 생성

        private void Awake() => _player = GetComponent<PlayerController>();

        
        private void Start()
        {
            _photonview = GetComponent<PhotonView>();//0409 08:06 이희웅 코드 수정 총알 동기화를 위한 코드
            _rangedWeaponsParent = Util.FindChild(gameObject, "RangedWeapons", true).transform;
            
            foreach (Transform t in _rangedWeaponsParent)
                t.localScale = Vector3.zero;  //모든 무기의 크기를 zero로 초기화

            _rangeWeapon = new RangedWeapon_Pistol(_rangedWeaponsParent, _player);  //기본 무기를 Pistol로 할당
        }

        private void Update() => _rangeWeapon.OnUpdate();  //장착중인 무기에 맞는 Update함수 호출

        public void OnShotUpdate()
        {
            if (IsTestMode.Instance.CurrentUser == Define.User.Hw)
            {
                if (_photonview.IsMine) //0409 08:06 이희웅 코드 수정 총알 동기화를 위한 코드
                    _rangeWeapon.Shot(Vector3.zero, _player);//장착중인 무기에 맞는 Shot함수 호출
            }
        }
        public void OnReloadUpdate() => _rangeWeapon.Reload(_player);//장착중인 무기에 맞는 Reload함수 호출

        public void SetRelic(IRelic relic) => _rangeWeapon.OnUpdateRelic(_player);  //렐릭 습득시 무기에 렐릭효과 부여.

        /// <summary>
        /// 무기 교체 함수
        /// </summary>
        /// <param name="weapon"></param>
        public void ChangeRangedWeapon(IRangedWeapon weapon)
        {
            foreach (Transform t in _rangedWeaponsParent)
            {
                if (t.name == weapon.WeaponType.ToString())
                    t.localScale = weapon.DefaultScale;
                else
                    t.localScale = Vector3.zero;
            }
            _rangeWeapon = weapon;
        }
    }

}
