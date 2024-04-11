using UnityEngine;

namespace yb {
    /// <summary>
    /// ȹ�� ������ Shotgun������
    /// </summary>
    public class ObtainableShotgun : ObtainableObject
    {
        /// <summary>
        /// ������ �Ⱦ� ��, �÷��̾��� ���⸦ �� ���������� ��ü
        /// </summary>
        public override void Pickup(PlayerController player) {
            player.WeaponController.ChangeRangedWeapon(new RangedWeapon_Shotgun(player.WeaponController.RangedWeaponsParent, player));
            player.WeaponEvent?.Invoke(52);
            Managers.Resources.Destroy(gameObject);
        }

        public override void ShowName()
        {
            base.ShowName();
        }
    }
}