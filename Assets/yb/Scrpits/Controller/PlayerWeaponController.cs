using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using yb;

namespace yb
{
    public class PlayerWeaponController : MonoBehaviour
    {
        private PlayerController _player;
        private IRangedWeapon _rangeWeapon;
        public IRangedWeapon RangedWeapon => _rangeWeapon;
        private Transform _rangedWeaponsParent;
        public Transform RangedWeaponsParent => _rangedWeaponsParent;
        private PhotonView _photonview;

        private void Awake() => _player = GetComponent<PlayerController>();

        private void Start()
        {
            _rangedWeaponsParent = Util.FindChild(gameObject, "RangedWeapons", true).transform;
            _photonview = GetComponent<PhotonView>();
            _rangeWeapon = new RangedWeapon_Pistol(_rangedWeaponsParent, _player);

            foreach (Transform t in _rangedWeaponsParent)
                t.localScale = Vector3.zero;
        }

        private void Update() => _rangeWeapon.OnUpdate();

        public void OnShotUpdate()
        {
            if (IsTestMode.Instance.CurrentUser == Define.User.Hw)
            {
                if (_photonview.IsMine) //15:11 ����� �׽�Ʈ ����ȭ
                    _rangeWeapon.Shot(Vector3.zero, _player);
            }
            else
                _rangeWeapon.Shot(Vector3.zero, _player);
        }
        public void OnReloadUpdate() => _rangeWeapon.Reload(_player);

        public void SetRelic(IRelic relic) => _rangeWeapon.OnUpdateRelic(_player);

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
