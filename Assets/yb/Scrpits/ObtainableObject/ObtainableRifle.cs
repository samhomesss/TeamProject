using UnityEngine;

namespace yb {
    public class ObtainableRifle : MonoBehaviour, IObtainableObject {
        public void Pickup(PlayerController player) {
            player.ChangeRangedWeapon(new RangedWeapon_Rifle(player.RangedWeaponsParent));
            Managers.Resources.Destroy(gameObject);
        }
    }
}
