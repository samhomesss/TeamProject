using UnityEngine;

namespace yb {
    public class ObtainableRifle : MonoBehaviour, IObtainableObject {
        public void Pickup(PlayerController player) {
            player.ChangeRangedWeapon(new RangedWeapon_Rifle(player.RangedWeaponsParent, player));
            Managers.Resources.Destroy(gameObject);
        }
    }
}
