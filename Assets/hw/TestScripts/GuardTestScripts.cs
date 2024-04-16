using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardTestScripts : MonoBehaviourPunCallbacks
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
           GameObject go = PhotonNetwork.Instantiate("Prefabs/hw/PlayerPrefabs/Guard", Vector3.zero, Quaternion.identity);
            go.transform.SetParent(transform.parent);
        }
            
        
    }
}
