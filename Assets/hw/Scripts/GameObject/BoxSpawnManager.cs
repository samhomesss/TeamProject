using Photon.Pun;
using System.Collections;
using UnityEngine;
using yb;

public class BoxSpawnManager : Obj
{
    const int ITEM_BOX_PCS = 13;
    PhotonView _photonView;
    WaitForSeconds waitsecond = new WaitForSeconds(5f);
    private Transform _itembox;
    void Start() {
        _itembox = GameObject.Find("ItemBox").GetComponent<Transform>();
        StartCoroutine(WaitPlayerLoded());
    }
    public IEnumerator BoxSpawn()
    {
        while (true)
        {
            //���ڰ� �� ��������{
            if (_itembox.childCount <= 0)
            {
                yield return waitsecond;
                if (PhotonNetwork.IsMasterClient)
                {
                    _photonView.RPC("SetItemBox", RpcTarget.All, ITEM_BOX_PCS);
                }
            }
            yield return null;
        }

    }

    IEnumerator WaitPlayerLoded()
    {
        // �÷��̾��� �ε��� ��ٸ��ϴ�.
        bool allPlayersLoaded = false;
        while (!allPlayersLoaded)
        {
            allPlayersLoaded = GameObject.Find($"Player{PhotonNetwork.LocalPlayer.ActorNumber}/Model").GetComponent<PhotonView>();
            yield return waitsecond;
        }
        _photonView = GameObject.Find($"Player{PhotonNetwork.LocalPlayer.ActorNumber}/Model").GetComponent<PhotonView>();
        yield return StartCoroutine(BoxSpawn());
    }
}
