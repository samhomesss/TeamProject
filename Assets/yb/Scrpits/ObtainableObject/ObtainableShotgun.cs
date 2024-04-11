using UnityEngine;

namespace yb {
    /// <summary>
    /// 획득 가능한 Shotgun아이템
    /// </summary>
    public class ObtainableShotgun : ObtainableObject
    {
        /// <summary>
        /// 아이템 픽업 시, 플레이어의 무기를 이 아이템으로 교체
        /// </summary>
        public override void Pickup(PlayerController player) {
            player.WeaponController.ChangeRangedWeapon(new RangedWeapon_Shotgun(player.WeaponController.RangedWeaponsParent, player));
            player.WeaponEvent?.Invoke(52);
            Managers.Resources.Destroy(gameObject);
        }

        public override void ShowName()
        {
            base.ShowName();
        }
    }
}