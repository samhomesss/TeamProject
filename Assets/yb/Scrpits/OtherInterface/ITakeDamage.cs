using Photon.Pun;
using System;
using UnityEngine;

namespace yb {
    /// <summary>
    /// 데미지를 입을 수 있는 모든 오브젝트가 상속받아야함.
    /// </summary>
    public interface ITakeDamage {
        void TakeDamage(int amout, GameObject attacker);
    }
}