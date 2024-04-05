using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using yb;

namespace yb {
    public class PlayerWeaponController : MonoBehaviour {
        private PlayerController _player;
        private IRangedWeapon _rangeWeapon;
        public IRangedWeapon RangedWeapon => _rangeWeapon;
        private Transform _rangedWeaponsParent;
        public Transform RangedWeaponsParent => _rangedWeaponsParent;

        private void Awake() => _player = GetComponent<PlayerController>();
  
        private void Start() {
            _rangedWeaponsParent = Util.FindChild(gameObject, "RangedWeapons", true).transform;

            _rangeWeapon = new RangedWeapon_Pistol(_rangedWeaponsParent, _player);

            foreach (Transform t in _rangedWeaponsParent)
                t.localScale = Vector3.zero;
        }

        private void Update() => _rangeWeapon.OnUpdate();

        public void OnShotUpdate() => _rangeWeapon.Shot(Vector3.zero, _player);

        public void OnReloadUpdate() => _rangeWeapon.Reload(_player);

        public void SetRelic(IRelic relic) => _rangeWeapon.OnUpdateRelic(_player);

        public void ChangeRangedWeapon(IRangedWeapon weapon) {
            foreach (Transform t in _rangedWeaponsParent) {
                if (t.name == weapon.WeaponType.ToString())
                    t.localScale = weapon.DefaultScale;
                else
                    t.localScale = Vector3.zero;
            }
            _rangeWeapon = weapon;
        }
    }

}
