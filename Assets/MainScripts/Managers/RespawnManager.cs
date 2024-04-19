using Photon.Pun;
using System.Collections;
using UnityEngine;
using yb;

public class RespawnManager : MonoBehaviourPunCallbacks
{
    private const float SpawnAllowRange = 5f;
    public static RespawnManager Instance;
    public Transform RespawnPoints { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        RespawnPoints = GameObject.Find("@RespawnPoints").transform;//0419 ÀÌÈñ¿õ start -> awake·Î ¼öÁ¤
    }

    public void Respawn(int number, float time)
    {
        StartCoroutine(CoRespawn(number, time));
    }

    IEnumerator CoRespawn(int number, float time)
    {
        yield return new WaitForSeconds(time);
        RespawnObject(number);
    }

    private void RespawnObject(int number)
    {
        var units = FindObjectsOfType<PlayerController>();
        if (units.Length > 0)
        {
            foreach (Transform t in RespawnPoints.transform)
            {
                foreach (PlayerController p in units)
                {
                    if (Vector3.Distance(t.position, p.transform.position) > SpawnAllowRange)
                    {
                        if (IsTestMode.Instance.CurrentUser == Define.User.Hw) //0415 16:22 ÀÌÈñ¿õ
                        {
                            GameObject unit = PhotonNetwork.Instantiate($"Prefabs/hw/PlayerPrefabs/Player{PhotonNetwork.LocalPlayer.ActorNumber}", Vector3.zero, Quaternion.identity);
                            Debug.Log($"{unit.name}»ý¼ºµÊ");
                            unit.transform.position = t.position;
                            break;
                        }
                        else
                        {
                            GameObject unit = Managers.Resources.Instantiate($"yb/Player/Player", null);
                            Debug.Log($"{unit.name}»ý¼ºµÊ");
                            unit.transform.position = t.position;
                            break;
                        }
                    }
                }
                return;
            }
        }
        else
        {

            if (IsTestMode.Instance.CurrentUser == Define.User.Hw)
            {
                GameObject unit = PhotonNetwork.Instantiate($"Prefabs/hw/PlayerPrefabs/Player{PhotonNetwork.LocalPlayer.ActorNumber}", Vector3.zero, Quaternion.identity);
                Debug.Log($"{unit.name}»ý¼ºµÊ");
                unit.transform.position = RespawnPoints.GetChild(0).transform.position;
            }
            else
            {
                GameObject unit = Managers.Resources.Instantiate($"yb/Player/Player", null);
                Debug.Log($"{unit.name}»ý¼ºµÊ");
                unit.transform.position = RespawnPoints.GetChild(0).transform.position;
            }
        }

    }
}
