using Photon.Pun;
using System;
using System.Collections;
using UnityEngine;


public class VFXLifeController : MonoBehaviour
{
    private float _time;
    public void Init(float time)
    {
        _time = time;
    }
    private void Start()
    {
        StartCoroutine(CoDestroyPhoton(_time));
    }

    public IEnumerator CoDestroyPhoton(float time)
    {
        yield return new WaitForSeconds(time);
        PhotonNetwork.Destroy(gameObject);
    }
}