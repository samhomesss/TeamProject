using UnityEngine;

namespace yb {
    public class ObtainableSword : MonoBehaviour, IObtainableObject {
        public void Pickup(PlayerController player) {
            player.ChangeMeleeWeapon(new MeleeWeapon_Sword(player.MeleeWeaponsParent, player));
            Managers.Resources.Destroy(gameObject);
        }
    }
}