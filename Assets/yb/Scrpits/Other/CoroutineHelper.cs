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

    public IEnumerator CoDelayPhotonObjectSpawn(float time, UnityAction call)
    {
        yield return new WaitForSeconds(time);
        GameObject go = PhotonNetwork.Instantiate("Prefabs/hw/PlayerPrefabs/Player", Vector3.zero, Quaternion.identity); 
        _photonView = Util.FindChild(go, "Model").GetComponent<PhotonView>();
        if (_photonView.IsMine) //ī�޶�, �� ����� �ٽ� ����
        {
            _photonView.RPC("RenamePlayer", RpcTarget.All, _photonView.ViewID); //�̸� �缳��
            Util.FindChild(go, "Camera", true).SetActive(true);
            Util.FindChild(go, "Camera", true).GetComponent<AudioListener>().enabled = true;
        }

        call.Invoke();

    }
    public IEnumerator CoDelayPhotonObjectDelete(GameObject go, float time)
    {
        yield return new WaitForSeconds(time);
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Destroy(go);
            Util.LogRed("�׾����� ��Ȱ��");
        }
            
    }

}
