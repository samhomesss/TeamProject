using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 발사체 관리 클래스
/// </summary>
namespace yb {
    public class ProjectileController : MonoBehaviour {
        private Rigidbody _rigid;
        private int _damage;  //발사체의 데미지
        private float _speed;  //발사체의 이동속도 
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
                c.GetComponent<ITakeDamage>().TakeDamage(_damage, gameObject);  //플레이어나 파괴 가능한 오브젝트와 접촉시 데미지를 입힘
                Managers.Resources.Destroy(gameObject);
                return;
            }
        }
    }
}

