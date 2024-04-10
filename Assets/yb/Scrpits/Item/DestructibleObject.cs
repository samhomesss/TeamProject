using Photon.Pun;
using System;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
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

        [SerializeField] string[] DropItemList;  //����� �������� string�������� ����


        private void Start()
        {
            _status = GetComponent<DestructibleObjectStatus>();
            _photonView = GetComponent<PhotonView>();//0410 16:25 ����� ����� �߰� 
            foreach (var item in DropItemList)
            {
                _droplable.Set(item);  //����� �������� ���̺� ����
            }

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
               PhotonNetwork.Destroy(gameObject);
            }
        }
    }
}