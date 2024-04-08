using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;

public class PhotonNetworkUtil : MonoBehaviourPunCallbacks
{
    public static ProjectileMoveScript CreatePhotonObject(string name,Vector3 position,int damage,GameObject creator, Quaternion quaternion = default)
    {
        var _shotBullet = PhotonNetwork.Instantiate(name, position, Quaternion.identity).GetComponent<ProjectileMoveScript>();
        _shotBullet.Init(quaternion,damage,creator);

        return _shotBullet;

    }


}