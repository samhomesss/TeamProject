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
            player.WeaponEvent?.Invoke(Define.WeaponType.Shotgun.ToString());
            Managers.Resources.Destroy(gameObject);
        }

        public override void ShowName(PlayerController player)
        {
            base.ShowName(player);
        }

        public override void HideName()
        {
            base.HideName();
        }
    }
}