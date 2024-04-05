using UnityEngine;

namespace yb {
    public class ObtainablePistol : MonoBehaviour, IObtainableObject {
        public string Name => gameObject.name;

        public void Pickup(PlayerController player) {
            player.WeaponController.ChangeRangedWeapon(new RangedWeapon_Pistol(player.WeaponController.RangedWeaponsParent, player));
            Managers.Resources.Destroy(gameObject);
        }
    }
}