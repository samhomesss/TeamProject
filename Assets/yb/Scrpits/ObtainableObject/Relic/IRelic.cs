using UnityEngine;

namespace yb {
    public interface IRelic {
        Define.RelicType RelicType { get; }

        Transform MyTransform { get; }

        void SetRelic(PlayerController player);

        void DeleteRelic(PlayerController player);
    }
}
