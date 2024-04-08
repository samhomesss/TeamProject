using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace yb {
    public class PistolProjectileCreator : IProjectileCreator {

        public void Create(int defaultDamage, float projectileSpeed, Vector3 targetPos, Vector3 createPos, PlayerController player) {
            ProjectileMoveScript vfx;
            vfx = PhotonNetworkUtil.CreatePhotonObject("Prefabs/yb/Projectile/Default", createPos);
            vfx.Init(player.RotateToMouseScript.GetRotation(), defaultDamage, player.gameObject);
        }
    }
}
