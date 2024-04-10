using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestShoter : MonoBehaviour
{
    public float AttackTimer;
    public float currentAttack;

    private void Update() {
        currentAttack += Time.deltaTime;

        if(currentAttack > AttackTimer ) {
            currentAttack = 0;
            var vfx = Managers.Resources.Instantiate("yb/Projectile/Default", null).GetComponent<ProjectileMoveScript>();
            vfx.Init(Quaternion.identity, 1, transform.position, gameObject);
        }
    }
}
