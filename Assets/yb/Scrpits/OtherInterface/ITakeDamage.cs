using UnityEngine;

namespace yb {
    public interface ITakeDamage {
        void TakeDamage(int amout, GameObject attacker);
    }
}