using UnityEngine;

namespace yb {
    /// <summary>
    /// 획득 가능한 Rifle아이템
    /// </summary>
    public class ObtainableRifle : MonoBehaviour, IObtainableObject {
        public string Name => gameObject.name;

        /// <summary>
        /// 아이템 픽업 시, 플레이어의 무기를 이 아이템으로 교체
        /// </summary>
        public void Pickup(PlayerController player) {
            player.WeaponController.ChangeRangedWeapon(new RangedWeapon_Rifle(player.WeaponController.RangedWeaponsParent, player));
            Managers.Resources.Destroy(gameObject);
        }
    }
}
