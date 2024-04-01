using Photon.Realtime;
using UnityEngine;
using yb;
namespace yb {
    public class PistolProjectileCreator : IProjectileCreator {
        public void Create(int defaultDamage, float projectileSpeed, Vector3 targetPos, Vector3 createPos, PlayerController player) {
            ProjectileMoveScript vfx;
            vfx = Managers.Resources.Instantiate("yb/Projectile/Default", null).GetComponent<ProjectileMoveScript>();
            vfx.transform.position = createPos;
            vfx.Init(player.RotateToMouseScript.GetRotation(), defaultDamage, player.gameObject);
        }
    }
}
