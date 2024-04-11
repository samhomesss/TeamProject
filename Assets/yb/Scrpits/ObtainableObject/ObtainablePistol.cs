using UnityEditor.Timeline;
using UnityEngine;

namespace yb {
    // Interface�� ��� �޴� �θ� Ŭ������ �޾ƿͼ� �ϴ°� ���� ���ƺ��̱���
    /// <summary>
    /// ȹ�� ������ Pistol������
    /// </summary>
    public class ObtainablePistol : ObtainableObject //MonoBehaviour, IObtainableObject 
    {
        
        /// <summary>
        /// ������ �Ⱦ� ��, �÷��̾��� ���⸦ �� ���������� ��ü
        /// </summary>
        /// <param name="player"></param>
        public override void Pickup(PlayerController player) {
            player.WeaponController.ChangeRangedWeapon(new RangedWeapon_Pistol(player.WeaponController.RangedWeaponsParent, player));
            player.WeaponEvent?.Invoke(50);
            Managers.Resources.Destroy(gameObject);
        }

        public override void ShowName()
        {
            base.ShowName();
        }
    }
}