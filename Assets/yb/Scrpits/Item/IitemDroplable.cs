using UnityEngine;
namespace yb {
    public interface IItemDroplable {
        void Set(string item);
        void Drop(Vector3 pos);
    }
}
