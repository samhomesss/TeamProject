using DG.Tweening;
using UnityEngine;

namespace yb {
    public class MeleeWeapon_Club : MeleeWeapon, IMeleeWeapon {
        public MeleeWeapon_Club(Transform centerTransform, PlayerController player) : base(centerTransform, player) {

            _weaponObject = Util.FindChild(centerTransform.gameObject, "Club", false);

            //todo:test
            _defaultDamage = 5f;
            _swingSpeed = 0.5f;
            _attackDelay = 2f;
            //todo
            //���� ���� �ٲٴ� �ִϸ��̼�
        }
        public void MeleeAttack() {
            if (_currentAttackDelay < _attackDelay)
                return;

            _currentAttackDelay = 0f;
            _weaponObject.SetActive(true);
            Vector3 currentRotation = _player.transform.eulerAngles;
            Vector3 newRotation = new Vector3(currentRotation.x, currentRotation.y + 45, currentRotation.z);
            _centerTransform.DORotate(newRotation, _swingSpeed).SetEase(Ease.Linear).OnComplete(ExitSwordAttack);
        }

        public void OnUpdate() => _currentAttackDelay += Time.deltaTime;

        protected override void ExitSwordAttack() {
            Vector3 currentRotation = _player.transform.eulerAngles;
            Vector3 newRotation = new Vector3(currentRotation.x, currentRotation.y - 45, currentRotation.z);
            _centerTransform.rotation = Quaternion.Euler(newRotation);
            _weaponObject.SetActive(false);
        }
    }
}