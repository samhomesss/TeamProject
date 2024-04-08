using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;

public class PhotonNetworkUtil : MonoBehaviourPunCallbacks
{
    public static ProjectileMoveScript CreatePhotonObject(string name,Vector3 position, Quaternion quaternion)
    {
        return PhotonNetwork.Instantiate(name, position, quaternion).GetComponent<ProjectileMoveScript>();
    }


}