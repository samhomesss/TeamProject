using UnityEngine;


/// <summary>
/// 파괴 가능한 오브젝트 관리 클래스
/// </summary>
namespace yb {
    public class DestructibleObject : MonoBehaviour, ITakeDamage {
        private DestructibleObjectStatus _status;  //오브젝트 능력치 관련 변수
        private IItemDroplable _droplable = new ItemDroplable();  //오브젝트 드랍 아이템 관련 변수

        
        [SerializeField] string[] DropItemList;  //드랍할 아이템을 string형식으로 저장
        private void Start() {
            _status = GetComponent<DestructibleObjectStatus>();

            foreach (var item in DropItemList) {
                _droplable.Set(item);  //드랍할 아이템을 테이블에 저장
            }
        }
 

        public void TakeDamage(int amout, GameObject attacker) {
            if (amout <= 0)
                return;

            int hp = _status.SetHp(-amout);

            if (hp <= 0) {
                _droplable.Drop(transform.position);  //사망시 저장해둔 아이템 모두 드랍
                Managers.Resources.Destroy(gameObject);
            }
        }
    }
}