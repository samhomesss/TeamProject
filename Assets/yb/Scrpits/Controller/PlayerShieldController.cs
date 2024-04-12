using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace yb {
    public class PlayerShieldController : MonoBehaviour, ITakeDamage, ITakeDamagePhoton
    {
        private const float ShieldDefaultAlpha = 0.5f;
        private const float ColorR = 0.3f;
        private const float ColorG = 0.7f;
        private const float ColorB = 0.8f;
        private PlayerController _player;
        private ShieldStatus _status;
        private MeshRenderer _mesh;
        private SphereCollider _collider;
        private Color _color;
        private float _hpBuffer;
        private bool _isTakeDamage;
        private float _shieldTimer;

        private PhotonView _photonView;

        public PhotonView IphotonView => _photonView;

        private void Start() {
            _player = GetComponentInParent<PlayerController>();
            _status = GetComponent<ShieldStatus>();
            _mesh = GetComponent<MeshRenderer>();
            _collider = GetComponent<SphereCollider>();
            _color = new Color(ColorR, ColorG, ColorB, 0.5f);
            _photonView = GetComponent<PhotonView>();
        }

        private void Update() {
            if(_isTakeDamage) {
                _shieldTimer += Time.deltaTime;

                if (_shieldTimer >= _status.ShieldRecoveryTime) {
                    _isTakeDamage = false;
                }
            }

            if(!_isTakeDamage && _status.CurrentHp <= _status.MaxHp) {
                _hpBuffer += Time.deltaTime;
                if(_hpBuffer >= 1f) {
                    _status.SetHp(1);
                    SetMaterial();
                    _hpBuffer -= 1f;
                    _collider.enabled = true;
                }
            }
        }

        private void SetMaterial() {
            if(_status.CurrentHp <= 0) {
                _color.a = 0f;
            }
            else
            {
                float ratio = (float)_status.CurrentHp / _status.MaxHp;
                _color.a = ratio * ShieldDefaultAlpha;
            }
            Debug.Log($"실드 알파값을 {_color.a}로 조정");
            _mesh.material.color = _color;
        }

        public void TakeDamage(int amout, GameObject attacker) {
            int hp = _status.SetHp(-amout);
            Debug.Log($"실드가 데미지를{amout}만큼 입음");
            Debug.Log($"실드 남은 체력 {hp}");
            SetMaterial();
            _isTakeDamage = true;
            _shieldTimer = 0;
            if (hp <= 0) {
                _collider.enabled = false;
            }
        }

        public void TakeDamagePhoton(int amout, int attackerViewNum)
        {
            int hp = _status.SetHp(-amout);
            Debug.Log($"실드가 데미지를{amout}만큼 입음");
            Debug.Log($"실드 남은 체력 {hp}");
            SetMaterial();
            _isTakeDamage = true;
            _shieldTimer = 0;
            if (hp <= 0)
            {
                _collider.enabled = false;
            }
        }
    }
}
