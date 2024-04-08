using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace yb {
    public class PistolProjectileCreator : IProjectileCreator {

        public void Create(int defaultDamage, float projectileSpeed, Vector3 targetPos, Vector3 createPos, PlayerController player) {
            ProjectileMoveScript vfx;
            if(IsTestMode.Instance.CurrentUser == Define.User.Hw)//0408 15:06 ÀÌÈñ¿õ Å×½ºÆ®
                vfx = PhotonNetworkUtil.CreatePhotonObject("Prefabs/yb/Projectile/Default", createPos);
            else
                vfx = Managers.Resources.Instantiate("Prefabs/yb/Projectile/Default", null).GetComponent<ProjectileMoveScript>();
            vfx.Init(player.RotateToMouseScript.GetRotation(), defaultDamage, player.gameObject);
        }
    }
}
