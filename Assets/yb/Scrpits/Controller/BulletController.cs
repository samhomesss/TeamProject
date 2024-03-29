using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace yb {
    public class BulletController : MonoBehaviour {
        private Rigidbody _rigid;
        private float _damage;
        private float _speed;
        public void Init(float damage, float speed, Vector3 targetPos, Vector3 createPos) {
            _rigid = GetComponent<Rigidbody>();
            transform.position = new Vector3(createPos.x, 1f, createPos.z);
            _damage = damage;
            _speed = speed;
            _rigid.velocity = (targetPos - transform.position).normalized * _speed;
            transform.LookAt(targetPos);
            StartCoroutine(CoDestroy(3f));
        }

        IEnumerator CoDestroy(float time) {
            yield return new WaitForSeconds(time);
            Managers.Resources.Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider c) {
            if (c.CompareTag("Player")) {
                //todo
                //데미지 로직
                Managers.Resources.Destroy(gameObject);
                return;
            }

            if (c.gameObject.layer == LayerMask.GetMask("Obstacle")) {
                Managers.Resources.Destroy(gameObject);
            }
        }
    }
}

