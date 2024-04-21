namespace yb {
    /// <summary>
    /// �÷��̾� ������ ����
    /// </summary>
    public class PlayerState_Reload : PlayerState, IPlayerState {
        private RangedWeapon _rangedWeapon;
        public PlayerState_Reload(PlayerController player, IRangedWeapon rangedWeapon)
        {
            _rangedWeapon = rangedWeapon as RangedWeapon;
            player.ChangeTriggerAnimation(Define.PlayerState.Reload);
            player.Audio.SetSfx(Define.PlayerAudioType.Reload);

        }
        public void OnUpdate(PlayerController player) {

        }
    }
}