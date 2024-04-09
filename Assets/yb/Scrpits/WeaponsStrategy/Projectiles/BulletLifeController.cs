using Photon.Pun;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class BulletLifeController : MonoBehaviour
{
    private const float BULLET_LIFE_DEFAULT_TIME = 3.0f;
    private float _time;
    private PhotonView _photonView;
    public void Init(float time = BULLET_LIFE_DEFAULT_TIME)
    {
        _time = time;
    }
    private void Start()
    {
        _photonView = GetComponent<PhotonView>();
        StartCoroutine(CoDestroyPhoton(_time));
    }

    public IEnumerator CoDestroyPhoton(float time)
    {
        yield return new WaitForSeconds(time);
        if(_photonView.IsMine)
        PhotonNetwork.Destroy(gameObject);
    }
}