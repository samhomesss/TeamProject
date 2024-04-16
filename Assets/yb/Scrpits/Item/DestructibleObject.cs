using ExitGames.Client.Photon.StructWrapping;
using Photon.Pun;
using UnityEngine;


/// <summary>
/// 파괴 가능한 오브젝트 관리 클래스
/// </summary>
namespace yb
{
    public class DestructibleObject : MonoBehaviourPunCallbacks, ITakeDamage ,ITakeDamagePhoton //0410 16:14 이희웅 MonoBehaviour ->  MonoBehaviourPunCallbacks 으로 변경
    {
        private DestructibleObjectStatus _status;  //오브젝트 능력치 관련 변수
        private IItemDroplable _droplable = new ItemDroplable();  //오브젝트 드랍 아이템 관련 변수

        private PhotonView _photonView; //0410 16:18 이희웅 포톤뷰 추가
        public PhotonView IphotonView { get => _photonView; }//0410 18:40 이희웅 인터페이스의 포톤뷰 프로퍼티 추가

        public IItemDroplable DropTable => _droplable;
        private void Start()
        {
            _status = GetComponent<DestructibleObjectStatus>();
            _photonView = GetComponent<PhotonView>();//0410 16:25 이희웅 포톤뷰 추가 
        }

        public void Init(Vector3 pos) {
            transform.position = pos;
            Define.WeaponType weaponRan = (Define.WeaponType)Random.Range(0, (int)Define.WeaponType.Count);
            Define.RelicType relicRan = (Define.RelicType)Random.Range(0, (int)Define.RelicType.Count);
            _droplable.Set(weaponRan.ToString());
            _droplable.Set(relicRan.ToString());
        }
     
        public void TakeDamage(int amout, GameObject attacker)
        {
            if (amout <= 0)
                return;

            int hp = _status.SetHp(-amout);

            if (hp <= 0)
            {
                _droplable.Drop(transform.position);  //사망시 저장해둔 아이템 모두 드랍
                Managers.Resources.Destroy(gameObject);
            }
        }

        [PunRPC]
        public void TakeDamagePhoton(int amout, int attackerViewNum) //0410 19:24 이희웅 포톤용 메서드 추가  
        {
            if (amout <= 0)
                return;

            //GameObject attacker = PhotonNetwork.GetPhotonView(attackerViewNum).gameObject;
            //PunRpc에서 GameObject를 직렬화 해서 보낼 수 없기에 직렬화 해서 보낼 수 있는 attackerViewNum을 보내고 해당 attackerViewNum을 포톤네트워크에서 찾아서 넣어준다.

            int hp = _status.SetHp(-amout);

            if (hp <= 0)
            {
                _droplable.Drop(transform.position);  //사망시 저장해둔 아이템 모두 드랍
                Managers.Resources.Destroy(gameObject);
            }
        }
    }
}