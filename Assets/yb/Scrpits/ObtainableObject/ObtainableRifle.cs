using UnityEngine;

namespace yb {
    /// <summary>
    /// ȹ�� ������ Rifle������
    /// </summary>
    public class ObtainableRifle : ObtainableObject {
        public string Name => gameObject.name;

        /// <summary>
        /// ������ �Ⱦ� ��, �÷��̾��� ���⸦ �� ���������� ��ü
        /// </summary>
        public override void Pickup(PlayerController player) {
            player.WeaponController.ChangeRangedWeapon(new RangedWeapon_Rifle(player.WeaponController.RangedWeaponsParent, player));
            player.WeaponEvent?.Invoke(51);
            Managers.Resources.Destroy(gameObject);
        }

        public override void ShowName()
        {
            base.ShowName();
        }
    }
}
