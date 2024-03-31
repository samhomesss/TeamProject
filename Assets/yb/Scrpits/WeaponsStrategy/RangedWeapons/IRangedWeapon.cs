using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace yb {
    public interface IRangedWeapon {
        void Shot(Vector3 targetPos,  PlayerController player);
        void OnUpdate();
    }
}
