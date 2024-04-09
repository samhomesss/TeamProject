namespace yb {
    /// <summary>
    /// 플레이어 상태 관리 인터페이스
    /// </summary>
    public interface IPlayerState {
        void OnUpdate(PlayerController player);  //현재 상태의 맞는 행동 Update
    }
}