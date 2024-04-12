using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace yb {
    /// <summary>
    /// Pistol 발사체 생성 클래스
    /// </summary>
    public class PistolProjectileCreator : IProjectileCreator {

        public void Create(int defaultDamage, float projectileSpeed, Vector3 targetPos, Vector3 createPos, PlayerController player) {
            ProjectileMoveScript vfx;
            if(IsTestMode.Instance.CurrentUser == Define.User.Hw)//0408 15:06 이희웅 테스트
            {
                vfx = PhotonNetworkUtil.CreatePhotonObject("Prefabs/yb/Projectile/Default", createPos, defaultDamage, player.gameObject, player.RotateToMouseScript.GetRotation());
            }
            else {
                vfx = Managers.Resources.Instantiate("yb/Projectile/Default", null).GetComponent<ProjectileMoveScript>();
                vfx.Init(player.RotateToMouseScript.GetRotation(), defaultDamage, createPos, player.gameObject);
            }
        }
    }
}
