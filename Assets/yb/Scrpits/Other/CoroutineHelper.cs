using Photon.Pun;
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
    
    public void ProjectileCreate(int index,int number, UnityAction call) {

        StartCoroutine(CoProjectileCreate(index, call));
    }

    private IEnumerator CoProjectileCreate(int index, UnityAction call) {
        float time = index * .1f;
        yield return new WaitForSeconds(time);
        call.Invoke();
    }

    public IEnumerator CoDelayPhotonObjectSpawn(float time)
    {
        yield return new WaitForSeconds(time);
        PhotonNetwork.Instantiate("Prefabs/hw/PlayerPrefabs/Player", Vector3.zero, Quaternion.identity);

    }
    public IEnumerator CoDelayPhotonObjectDelete(GameObject go, float time)
    {
        yield return new WaitForSeconds(time);
        PhotonNetwork.Destroy(go);
    }
}
