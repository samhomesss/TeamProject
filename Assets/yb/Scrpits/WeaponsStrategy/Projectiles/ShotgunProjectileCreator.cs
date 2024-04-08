using Photon.Pun;
using UnityEngine;
using yb;

namespace yb {
    public class ShotgunProjectileCreator : IProjectileCreator {
        public void Create(int defaultDamage, float projectileSpeed, Vector3 targetPos, Vector3 createPos, PlayerController player) {
            ProjectileMoveScript vfx;

            vfx = Managers.Resources.Instantiate("yb/Projectile/Default", null).GetComponent<ProjectileMoveScript>();
            Quaternion original = player.RotateToMouseScript.GetRotation();
            float angle = Random.Range(-10f,10f);

            Quaternion deltaRotation = Quaternion.AngleAxis(angle, Vector3.up); // 델타 회전 생성
            Quaternion modifiedRotation = original * deltaRotation; // 델타 회전 적용
            vfx.Init(player.RotateToMouseScript.GetRotation(), defaultDamage, player.gameObject);
        }
    }
}
