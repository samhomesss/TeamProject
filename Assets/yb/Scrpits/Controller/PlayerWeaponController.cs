using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using yb;

/// <summary>
/// �÷��̾� ���� ���� Ŭ����
/// </summary>
namespace yb
{
    public class PlayerWeaponController : MonoBehaviour
    {
        private PlayerController _player;
        private IRangedWeapon _rangeWeapon;  //���� �÷��̾ �������� ����
        public IRangedWeapon RangedWeapon => _rangeWeapon;
        private Transform _rangedWeaponsParent;  //���̾��Ű �󿡼� �÷��̾��� ���� ������Ʈ���� �θ� ������Ʈ
        public Transform RangedWeaponsParent => _rangedWeaponsParent;

        private PhotonView _photonview;//0409 08:06 ����� �ڵ� ���� �Ѿ� ����ȭ�� ���� ����� ����

        private void Awake() => _player = GetComponent<PlayerController>();

        
        private void Start()
        {
            _photonview = GetComponent<PhotonView>();//0409 08:06 ����� �ڵ� ���� �Ѿ� ����ȭ�� ���� �ڵ�
            _rangedWeaponsParent = Util.FindChild(gameObject, "RangedWeapons", true).transform;
            
            foreach (Transform t in _rangedWeaponsParent)
                t.localScale = Vector3.zero;  //��� ������ ũ�⸦ zero�� �ʱ�ȭ

            _rangeWeapon = new RangedWeapon_Pistol(_rangedWeaponsParent, _player);  //�⺻ ���⸦ Pistol�� �Ҵ�
        }

        private void Update() => _rangeWeapon.OnUpdate();  //�������� ���⿡ �´� Update�Լ� ȣ��

        public void OnShotUpdate()
        {
            if (IsTestMode.Instance.CurrentUser == Define.User.Hw)
            {
                if (_photonview.IsMine) //0409 08:06 ����� �ڵ� ���� �Ѿ� ����ȭ�� ���� �ڵ�
                    _rangeWeapon.Shot(Vector3.zero, _player);//�������� ���⿡ �´� Shot�Լ� ȣ��
            }
        }
        public void OnReloadUpdate() => _rangeWeapon.Reload(_player);//�������� ���⿡ �´� Reload�Լ� ȣ��

        public void SetRelic(IRelic relic) => _rangeWeapon.OnUpdateRelic(_player);  //���� ����� ���⿡ ����ȿ�� �ο�.

        /// <summary>
        /// ���� ��ü �Լ�
        /// </summary>
        /// <param name="weapon"></param>
        public void ChangeRangedWeapon(IRangedWeapon weapon)
        {
            foreach (Transform t in _rangedWeaponsParent)
            {
                if (t.name == weapon.WeaponType.ToString())
                    t.localScale = weapon.DefaultScale;
                else
                    t.localScale = Vector3.zero;
            }
            _rangeWeapon = weapon;
        }
    }

}
