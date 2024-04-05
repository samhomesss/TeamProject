using UnityEngine;

namespace yb {
    public class ObtainablePistol : MonoBehaviour, IObtainableObject {
        public void Pickup(PlayerController player) {
            player.ChangeRangedWeapon(new RangedWeapon_Pistol(player.RangedWeaponsParent, player));
            Managers.Resources.Destroy(gameObject);
        }
    }
}