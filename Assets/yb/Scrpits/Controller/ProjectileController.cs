using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace yb {
    public class ProjectileController : MonoBehaviour {
        private Rigidbody _rigid;
        private int _damage;
        private float _speed;
        private PlayerController _creator;
        public void Init(int damage, float speed, Vector3 targetPos, Vector3 createPos, PlayerController creator) {
            _rigid = GetComponent<Rigidbody>();
            transform.position = new Vector3(createPos.x, 1f, createPos.z);
            _damage = damage;
            _speed = speed;
            _creator = creator;
            _rigid.velocity = (targetPos - transform.position).normalized * _speed;
            transform.LookAt(targetPos);
            StartCoroutine(CoDestroy(3f));
        }

        IEnumerator CoDestroy(float time) {
            yield return new WaitForSeconds(time);
            Managers.Resources.Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider c) {
            if (c.gameObject == _creator.gameObject)
                return;

            if(c.CompareTag("Obstacle"))
            {
                Managers.Resources.Destroy(gameObject);
                return;
            }

            if (c.CompareTag("Player") || c.CompareTag("DestructibleObject")) {
                c.GetComponent<ITakeDamage>().TakeDamage(_damage, gameObject);
                Managers.Resources.Destroy(gameObject);
                return;
            }
        }
    }
}

