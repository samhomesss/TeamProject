using UnityEngine;

namespace yb {
    public class ObtainablePistol : MonoBehaviour, IObtainableObject {
        public void Pickup(PlayerController player) {
            player.ChangeRangedWeapon(new RangedWeapon_Pistol(player.RangedWeaponsParent));
            Managers.Resources.Destroy(gameObject);
        }
    }
}