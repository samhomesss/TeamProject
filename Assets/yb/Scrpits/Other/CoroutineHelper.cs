using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoroutineHelper : MonoBehaviour
{
    public static CoroutineHelper Instance;
    private void Awake() {
        Instance = this;
    }

    public void ProjectileCreate(int index, UnityAction call) {

        StartCoroutine(CoProjectileCreate(index, call));
    }

    private IEnumerator CoProjectileCreate(int index, UnityAction call) {
        float time = index * .1f;
        yield return new WaitForSeconds(time);
        call.Invoke();
    }
}
