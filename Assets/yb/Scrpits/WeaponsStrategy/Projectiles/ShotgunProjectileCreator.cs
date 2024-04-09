using Photon.Pun;
using UnityEngine;
using yb;

namespace yb {
    /// <summary>
    /// Shotgun 발사체 생성 클래스
    /// </summary>
    public class ShotgunProjectileCreator : IProjectileCreator {
        public void Create(int defaultDamage, float projectileSpeed, Vector3 targetPos, Vector3 createPos, PlayerController player) {
            ProjectileMoveScript vfx;
            if (IsTestMode.Instance.CurrentUser == Define.User.Hw)//0408 15:06 이희웅 테스트
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

            Quaternion deltaRotation = Quaternion.AngleAxis(angle, Vector3.up); // 델타 회전 생성
            Quaternion modifiedRotation = original * deltaRotation; // 델타 회전 적용
            vfx.Init(player.RotateToMouseScript.GetRotation(), defaultDamage, player.gameObject);
        }
    }
}
