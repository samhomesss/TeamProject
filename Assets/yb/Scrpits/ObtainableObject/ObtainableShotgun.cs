using UnityEngine;

namespace yb {
    public class ObtainableShotgun : MonoBehaviour, IObtainableObject {
        public void Pickup(PlayerController player) {
            player.WeaponController.ChangeRangedWeapon(new RangedWeapon_Shotgun(player.WeaponController.RangedWeaponsParent, player));
            Managers.Resources.Destroy(gameObject);
        }
    }
}