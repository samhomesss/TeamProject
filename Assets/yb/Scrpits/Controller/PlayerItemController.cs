using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace yb {
    public class PlayerItemController : MonoBehaviour {
        private PlayerController _player;
        private bool _isAttackSpeedUp;
        private bool _isDamageUp;
        private bool _isMoveSpeedUp;
        private void Start() {
            _player = GetComponent<PlayerController>();
        }

        private void Update() {
            if (_player.ItemList.Count <= 0)
                return;

            if (_player.ItemList.ContainsKey(0) &&
                Input.GetKeyDown(KeyCode.Alpha1)) {
                UseItem(0, _player.ItemList[0].ItemType);
                Debug.Log("1�� ������ ���");
            }

            if (_player.ItemList.ContainsKey(1) &&
               Input.GetKeyDown(KeyCode.Alpha2)) {
                UseItem(1, _player.ItemList[1].ItemType);
                Debug.Log("2�� ������ ���");

            }

            if (_player.ItemList.ContainsKey(2) &&
               Input.GetKeyDown(KeyCode.Alpha3)) {
                UseItem(2, _player.ItemList[2].ItemType);
                Debug.Log("3�� ������ ���");

            }

            if (_player.ItemList.ContainsKey(3) &&
               Input.GetKeyDown(KeyCode.Alpha4)) {
                UseItem(3, _player.ItemList[3].ItemType);
                Debug.Log("4�� ������ ���");

            }
        }

        public void UseItem(int key, Define.ItemType type) {
            _player.ItemList[key].ItemNumber--;
            switch(type) {
                case Define.ItemType.HpPotion:
                    _player.Status.SetHp(10);
                    _player.HpEvent?.Invoke(_player.Status.CurrentHp, _player.Status.MaxHp);
                    _player.PhotonView.RPC("DrankPotion", RpcTarget.All, _player.PhotonView.ViewID, _player.Status.CurrentHp);
                    break;
                case Define.ItemType.DamageUpPotion:
                    if(!_isDamageUp)
                        StartCoroutine(CoDamageUp(5f));
                    break;
                case Define.ItemType.MoveSpeedUpPotion:
                    if(!_isMoveSpeedUp)
                        StartCoroutine(CoMoveSpeed(5f));
                    break;
                case Define.ItemType.AttackSpeedUpPotion:
                    if(!_isAttackSpeedUp)
                        StartCoroutine(CoDelayDown(5f));
                    break;
            }
            Debug.Log($"{key}�� ������ ���");
            Debug.Log($"{key}�� ������ {_player.ItemList[key].ItemNumber}�� ����");
            _player.SetItemEvent(key, _player.ItemList[key]);

            if (_player.ItemList[key].ItemNumber <= 0) {
                Debug.Log($"{key}�� ������ ����");

                _player.ItemList.Remove(key);
            }
        }
        IEnumerator CoDelayDown(float time) {
            _isAttackSpeedUp = true;
            var weapon = _player.WeaponController.RangedWeapon as RangedWeapon;
            float delay = weapon.MaxDelay;
            weapon.MaxDelay *= 0.7f;
            yield return new WaitForSeconds(time);
            weapon.MaxDelay = delay;
            _isAttackSpeedUp = false;

        }

        IEnumerator CoMoveSpeed(float time) {
            _isMoveSpeedUp = true;
            float speed = _player.Status.MoveSpeed;
            _player.Status.MoveSpeed *= 2f;
            yield return new WaitForSeconds(time);
            _player.Status.MoveSpeed = speed;
            _isMoveSpeedUp = false;
        }

        IEnumerator CoDamageUp(float time) {
            _isDamageUp = true;
            var weapon = _player.WeaponController.RangedWeapon as RangedWeapon;
            int damage = weapon.DefaultDamage;
            weapon.DefaultDamage += 10;
            yield return new WaitForSeconds(time);
            weapon.DefaultDamage = damage;
            _isDamageUp = false;
        }
    }
}