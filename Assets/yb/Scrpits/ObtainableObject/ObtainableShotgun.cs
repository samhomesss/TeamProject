using UnityEngine;

namespace yb {
    public class ObtainableShotgun : MonoBehaviour, IObtainableObject {
        public void Pickup(PlayerController player) {
            player.ChangeRangedWeapon(new RangedWeapon_Shotgun(player.RangedWeaponsParent));
            Managers.Resources.Destroy(gameObject);
        }
    }
}