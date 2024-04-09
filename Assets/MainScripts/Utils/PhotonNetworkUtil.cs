using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;

public class PhotonNetworkUtil : MonoBehaviourPunCallbacks
{
    public static ProjectileMoveScript CreatePhotonObject(string name,Vector3 position)
    {
        return PhotonNetwork.Instantiate(name, position, Quaternion.identity).GetComponent<ProjectileMoveScript>();
    }


}