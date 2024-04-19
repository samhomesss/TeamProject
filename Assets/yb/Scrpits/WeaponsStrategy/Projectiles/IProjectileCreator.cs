using UnityEngine;
namespace yb {
    /// <summary>
    /// 발사체 관련 인터페이스
    /// </summary>
    public interface IProjectileCreator {
        void Create(int defaultDamage, float projectileSpeed, Vector3 targetPos, Vector3 createPos, PlayerController player, float range);  //발사체 생성
    }
}
