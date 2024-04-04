using UnityEngine;

namespace yb {
    public class DestructibleObject : MonoBehaviour, ITakeDamage {
        private DestructibleObjectStatus _status;
        private IItemDroplable _droplable = new ItemDroplable();

        [SerializeField] string[] DropItemList;
        private void Start() {
            _status = GetComponent<DestructibleObjectStatus>();

            foreach (var item in DropItemList) {
                _droplable.Set(item);
            }
        }
 

        public void TakeDamage(int amout, GameObject attacker) {
            if (amout <= 0)
                return;

            int hp = _status.SetHp(-amout);

            if (hp <= 0) {
                _droplable.Drop(transform.position);
                Managers.Resources.Destroy(gameObject);
            }
        }
    }
}