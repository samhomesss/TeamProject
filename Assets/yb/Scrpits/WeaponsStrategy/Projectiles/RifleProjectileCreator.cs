using Photon.Pun;
using UnityEngine;
using yb;

namespace yb
{
    public class RifleProjectileCreator : IProjectileCreator
    {
        PhotonView _photonView;
        public void Create(int defaultDamage, float projectileSpeed, Vector3 targetPos, Vector3 createPos, PlayerController player)
        {
            ProjectileMoveScript vfx;
            if (IsTestMode.Instance.CurrentUser == Define.User.Hw)//0408 15:06 ÀÌÈñ¿õ Å×½ºÆ®
            {
                _photonView = player.GetComponent<PhotonView>();
                if (_photonView.IsMine)
                {
                    vfx = PhotonNetworkUtil.CreatePhotonObject("Prefabs/yb/Projectile/Default", createPos, player.RotateToMouseScript.GetRotation());
                }
            }
            else
            {
                vfx = Managers.Resources.Instantiate("yb/Projectile/Default", null).GetComponent<ProjectileMoveScript>();
                vfx.Init(player.RotateToMouseScript.GetRotation(), defaultDamage, player.gameObject);
            }

        }
    }
}
