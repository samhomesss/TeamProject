using DG.Tweening;
using UnityEngine;

namespace yb {
    public class MeleeWeapon_Sword : MeleeWeapon, IMeleeWeapon {
        public MeleeWeapon_Sword(Transform centerTransform, PlayerController player) : base(centerTransform, player) {
            _weaponObject = Util.FindChild(centerTransform.gameObject, "Sword", false);

            //todo:test
            _defaultDamage = 10f;
            _swingSpeed = 0.3f;
            _attackDelay = 2f;
            _currentAttackDelay = _attackDelay;

            //todo
            //대충 무기 바꾸는 애니메이션
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