namespace yb {
    public interface IRelic {
        Define.RelicType RelicType { get; }

        void SetRelic(PlayerController player);

        void DeleteRelic(PlayerController player);
    }
}
