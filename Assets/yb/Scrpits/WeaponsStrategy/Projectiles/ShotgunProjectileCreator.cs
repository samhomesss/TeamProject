using UnityEngine;
using yb;

namespace yb {
    public class ShotgunProjectileCreator : IProjectileCreator {
        public void Create(int defaultDamage, float projectileSpeed, Vector3 targetPos, Vector3 createPos, PlayerController player) {
            ProjectileMoveScript vfx;

            vfx = Managers.Resources.Instantiate("yb/Projectile/Default", null).GetComponent<ProjectileMoveScript>();
            vfx.transform.position = createPos;
            Quaternion original = player.RotateToMouseScript.GetRotation();
            float angle = Random.Range(-10f,10f);

            Quaternion deltaRotation = Quaternion.AngleAxis(angle, Vector3.up); // ��Ÿ ȸ�� ����
            Quaternion modifiedRotation = original * deltaRotation; // ��Ÿ ȸ�� ����
            vfx.Init(modifiedRotation, defaultDamage, player.gameObject);
        }
    }
}
