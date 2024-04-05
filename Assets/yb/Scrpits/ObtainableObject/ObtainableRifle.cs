using UnityEngine;

namespace yb {
    public class ObtainableRifle : MonoBehaviour, IObtainableObject {
        public void Pickup(PlayerController player) {
            player.WeaponController.ChangeRangedWeapon(new RangedWeapon_Rifle(player.WeaponController.RangedWeaponsParent, player));
            Managers.Resources.Destroy(gameObject);
        }
    }
}
