using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace yb {
    public interface IRangedWeapon {

        public enum weaponType {
            Pistol,
            Rifle,
            Shotgun,
        }

        public weaponType WeaponType { get; set; }

        public Vector3 DefaultScale { get; set; }
        void Shot(Vector3 targetPos,  PlayerController player);

        void Reload(PlayerController player);

        bool CanReload();

        void OnUpdate();

        bool CanShot();
    }
}
