using Photon.Pun;
using System;
using System.Collections;
using UnityEngine;


public class VFXLifeController : MonoBehaviour
{
    private const float BULLET_LIFE_DEFAULT_TIME = 3.0f;
    private float _time;
    public void Init(float time = BULLET_LIFE_DEFAULT_TIME)
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