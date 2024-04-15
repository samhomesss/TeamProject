using ExitGames.Client.Photon.StructWrapping;
using Photon.Pun;
using UnityEngine;


/// <summary>
/// �ı� ������ ������Ʈ ���� Ŭ����
/// </summary>
namespace yb
{
    public class DestructibleObject : MonoBehaviourPunCallbacks, ITakeDamage ,ITakeDamagePhoton //0410 16:14 ����� MonoBehaviour ->  MonoBehaviourPunCallbacks ���� ����
    {
        private DestructibleObjectStatus _status;  //������Ʈ �ɷ�ġ ���� ����
        private IItemDroplable _droplable = new ItemDroplable();  //������Ʈ ��� ������ ���� ����

        private PhotonView _photonView; //0410 16:18 ����� ����� �߰�
        public PhotonView IphotonView { get => _photonView; }//0410 18:40 ����� �������̽��� ����� ������Ƽ �߰�

        public IItemDroplable DropTable => _droplable;
        private void Start()
        {
            _status = GetComponent<DestructibleObjectStatus>();
            _photonView = GetComponent<PhotonView>();//0410 16:25 ����� ����� �߰� 
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
                _droplable.Drop(transform.position);  //����� �����ص� ������ ��� ���
                Managers.Resources.Destroy(gameObject);
            }
        }

        [PunRPC]
        public void TakeDamagePhoton(int amout, int attackerViewNum) //0410 19:24 ����� ����� �޼��� �߰�  
        {
            if (amout <= 0)
                return;

            //GameObject attacker = PhotonNetwork.GetPhotonView(attackerViewNum).gameObject;
            //PunRpc���� GameObject�� ����ȭ �ؼ� ���� �� ���⿡ ����ȭ �ؼ� ���� �� �ִ� attackerViewNum�� ������ �ش� attackerViewNum�� �����Ʈ��ũ���� ã�Ƽ� �־��ش�.

            int hp = _status.SetHp(-amout);

            if (hp <= 0)
            {
                _droplable.Drop(transform.position);  //����� �����ص� ������ ��� ���
                Managers.Resources.Destroy(gameObject);
            }
        }
    }
}