using UnityEngine;

namespace yb {
    public interface IObtainableObject {
        public string Name { get; }
        void Pickup(PlayerController player);
    }
}