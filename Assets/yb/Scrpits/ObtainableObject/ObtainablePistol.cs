using UnityEngine;

namespace yb {
    /// <summary>
    /// ȹ�� ������ Pistol������
    /// </summary>
    public class ObtainablePistol : MonoBehaviour, IObtainableObject {
        public string Name => gameObject.name;

        /// <summary>
        /// ������ �Ⱦ� ��, �÷��̾��� ���⸦ �� ���������� ��ü
        /// </summary>
        /// <param name="player"></param>
        public void Pickup(PlayerController player) {
            player.WeaponController.ChangeRangedWeapon(new RangedWeapon_Pistol(player.WeaponController.RangedWeaponsParent, player));
            Managers.Resources.Destroy(gameObject);
        }
    }
}