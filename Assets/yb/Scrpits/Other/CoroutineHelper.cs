using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoroutineHelper : MonoBehaviour
{
    public static CoroutineHelper Instance;
    public PhotonView _photonView;

    private void Awake() {
        Instance = this;
    }

    public void ProjectileCreate(int index, UnityAction call) {

        StartCoroutine(CoProjectileCreate(index, call));
    }
    
    public void ProjectileCreate(int index,int number, UnityAction call) {

        StartCoroutine(CoProjectileCreate(index, call));
    }

    private IEnumerator CoProjectileCreate(int index, UnityAction call) {
        float time = index * .1f;
        yield return new WaitForSeconds(time);
        call.Invoke();
    }

    public IEnumerator CoAudioDestroy(float time, GameObject go) {
        yield return new WaitForSeconds(time);
        PhotonNetwork.Destroy(go);
    }

}
