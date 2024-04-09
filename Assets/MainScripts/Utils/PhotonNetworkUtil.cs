using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;

public class PhotonNetworkUtil : MonoBehaviourPunCallbacks
{
    public static ProjectileMoveScript CreatePhotonObject(string name,Vector3 position,int damage,GameObject creator, Quaternion quaternion = default)
    {
        var _shotBullet = PhotonNetwork.Instantiate(name, position, quaternion).GetComponent<ProjectileMoveScript>();

        return _shotBullet;

    }


}