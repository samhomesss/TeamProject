using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using yb;

public class RespawnManager : MonoBehaviourPunCallbacks {
    private const float SpawnAllowRange = 5f;
    public static RespawnManager Instance;
    public Transform RespawnPoints { get; private set; }



    private void Awake() {
        Instance = this;
    }

    private void Start() {
        RespawnPoints = GameObject.Find("@RespawnPoints").transform;
    }

    public void Respawn(int number, float time) {
        StartCoroutine(CoRespawn(number, time));
    }

    IEnumerator CoRespawn(int number, float time) {
        yield return new WaitForSeconds(time);
        RespawnObject(number);
    }

    private void RespawnObject(int number) {
        var units = FindObjectsOfType<PlayerController>();
        if(units.Length > 0) {
            foreach (Transform t in RespawnPoints.transform) {
                foreach (PlayerController p in units) {
                    if (Vector3.Distance(t.position, p.transform.position) > SpawnAllowRange) {
                        if(IsTestMode.Instance.CurrentUser == Define.User.Hw) //0415 16:22 捞锐旷
                        {
                            GameObject unit = PhotonNetwork.Instantiate("Prefabs/hw/PlayerPrefabs/Player",Vector3.zero,Quaternion.identity);
                            Debug.Log($"{unit.name}积己凳");
                            unit.transform.position = t.position;
                            break;
                        }
                        else
                        {
                            GameObject unit = Managers.Resources.Instantiate($"yb/Player/Player", null);
                            Debug.Log($"{unit.name}积己凳");
                            unit.transform.position = t.position;
                            break;
                        }
                    }
                }
                return;
            }
        }
        else {

            if(IsTestMode.Instance.CurrentUser == Define.User.Hw)
            {
                GameObject unit = PhotonNetwork.Instantiate("Prefabs/hw/PlayerPrefabs/Player", Vector3.zero, Quaternion.identity);
                Debug.Log($"{unit.name}积己凳");
                unit.transform.position = RespawnPoints.GetChild(0).transform.position;
            }
            else
            {
                GameObject unit = Managers.Resources.Instantiate($"yb/Player/Player", null);
                Debug.Log($"{unit.name}积己凳");
                unit.transform.position = RespawnPoints.GetChild(0).transform.position;
            }
        }
        
    }
}
