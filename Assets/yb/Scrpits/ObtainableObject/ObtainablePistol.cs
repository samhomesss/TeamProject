using UnityEditor.Timeline;
using UnityEngine;

namespace yb {
    // Interface를 상속 받는 부모 클래스를 받아와서 하는게 제일 좋아보이긴함
    /// <summary>
    /// 획득 가능한 Pistol아이템
    /// </summary>
    public class ObtainablePistol : ObtainableObject //MonoBehaviour, IObtainableObject 
    {
        
        /// <summary>
        /// 아이템 픽업 시, 플레이어의 무기를 이 아이템으로 교체
        /// </summary>
        /// <param name="player"></param>
        public override void Pickup(PlayerController player) {
            player.WeaponController.ChangeRangedWeapon(new RangedWeapon_Pistol(player.WeaponController.RangedWeaponsParent, player));
            player.WeaponEvent?.Invoke(50);
            Managers.Resources.Destroy(gameObject);
        }

        public override void ShowName()
        {
            base.ShowName();
        }
    }
}