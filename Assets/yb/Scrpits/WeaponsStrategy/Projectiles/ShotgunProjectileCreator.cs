using Photon.Pun;
using UnityEngine;
using yb;

namespace yb {
    public class ShotgunProjectileCreator : IProjectileCreator {
        public void Create(int defaultDamage, float projectileSpeed, Vector3 targetPos, Vector3 createPos, PlayerController player) {
            ProjectileMoveScript vfx;
            if (IsTestMode.Instance.CurrentUser == Define.User.Hw)//0408 15:06 ÀÌÈñ¿õ Å×½ºÆ®
            {
                vfx = PhotonNetworkUtil.CreatePhotonObject("Prefabs/yb/Projectile/Default", createPos);
            }
            else
            {
                vfx = Managers.Resources.Instantiate("yb/Projectile/Default", null).GetComponent<ProjectileMoveScript>();
                vfx.Init(player.RotateToMouseScript.GetRotation(), defaultDamage, player.gameObject);
            }
            
            Quaternion original = player.RotateToMouseScript.GetRotation();
            float angle = Random.Range(-10f,10f);

            Quaternion deltaRotation = Quaternion.AngleAxis(angle, Vector3.up); // µ¨Å¸ È¸Àü »ý¼º
            Quaternion modifiedRotation = original * deltaRotation; // µ¨Å¸ È¸Àü Àû¿ë
            vfx.Init(player.RotateToMouseScript.GetRotation(), defaultDamage, player.gameObject);
        }
    }
}
