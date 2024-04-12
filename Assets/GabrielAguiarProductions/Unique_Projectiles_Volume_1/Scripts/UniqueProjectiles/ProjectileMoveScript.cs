//
//
//NOTES:
//
//This script is used for DEMONSTRATION porpuses of the Projectiles. I recommend everyone to create their own code for their own projects.
//THIS IS JUST A BASIC EXAMPLE PUT TOGETHER TO DEMONSTRATE VFX ASSETS.
//
//




#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using yb;

public class ProjectileMoveScript : MonoBehaviourPunCallbacks
{ //0410 17:28 이희웅 데미지 동기화를 위한 Action 인터페이스 구현

    public bool rotate = false;
    public float rotateAmount;
    public bool bounce = false;
    public float bounceForce = 10;
    public float speed;
    [Tooltip("From 0% to 100%")]
    public float accuracy;
    public float fireRate;
    public GameObject muzzlePrefab;
    public GameObject hitPrefab;
    public List<GameObject> trails;

    private bool collided;
    private Rigidbody rb;
    private int _damage;
    private GameObject _creator;

    public event Action itakeDamageAction;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void Init(int damage, GameObject creator) //0409 12:45 이희웅 함수 오버로딩 추가  
    {
        _damage = damage;
        _creator = creator;
    }

    public void Init(Quaternion rotate, int damage, GameObject creator)
    {
        transform.localRotation = rotate;
        _damage = damage;
        _creator = creator;
    }

    public void Init(Quaternion rotate, int damage, Vector3 pos, GameObject creator)
    {
        transform.position = pos;
        transform.localRotation = rotate;
        _damage = damage;
        _creator = creator;
        Debug.Log($"크리에이터는{_creator} ");

    }

    void FixedUpdate()
    {
        Vector3 dir = new Vector3(transform.forward.x, 0f, transform.forward.z);
        rb.position += (dir) * (speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision co)
    {
        if(IsTestMode.Instance.CurrentUser == Define.User.Hw) {
            if (GetComponent<PhotonView>().IsMine) //0409 17:30 이희웅 총알 소유권 추가
        {
                if (co.gameObject == _creator)
                    return;

                if (co.collider.CompareTag("Bullet"))
                    return;

                if (!collided) {
                    if (co.collider.CompareTag("Guard") &&
                    _creator.transform.parent.gameObject != co.collider.transform.parent.gameObject) {
                        Crash(co);
                        Debug.Log("투사체가 가드에 막힘");
                        return;
                    }

                    if (co.collider.CompareTag("Obstacle")) {
                        Crash(co);
                        return;
                    }

                    if (co.collider.CompareTag("Player") || co.collider.CompareTag("DestructibleObject")
                        || co.collider.CompareTag("Shield")) {
                        if (IsTestMode.Instance.CurrentUser == Define.User.Hw)//0410 17:00 이희웅 테스트 추가
                        {
                            co.collider.GetComponent<ITakeDamagePhoton>().IphotonView.RPC("TakeDamagePhoton", RpcTarget.All, _damage, gameObject.GetComponent<PhotonView>().ViewID);
                        } else {
                            co.collider.GetComponent<ITakeDamage>().TakeDamage(_damage, gameObject);
                        }
                        Crash(co);

                        if (co.collider.CompareTag("Shield"))
                            Debug.Log("투사체가 실드에 막힘");
                        return;
                    }
                }
            }
        }
        else {
            Debug.Log($"co는 {co.gameObject.name}");
            if (co.gameObject == _creator)
                return;

            if (co.collider.CompareTag("Bullet"))
                return;

            if (!collided) {
                if (co.collider.CompareTag("Guard") &&
                    _creator.transform.parent.gameObject != co.collider.transform.parent.gameObject) {
                    Crash(co);
                    Debug.Log("투사체가 가드에 막힘");
                    return;
                }

                if (co.collider.CompareTag("Obstacle")) {
                    Debug.Log("장애물과 부딪힘");
                    Crash(co);
                    return;
                }

                if (co.collider.CompareTag("Player") || co.collider.CompareTag("DestructibleObject")
                    || co.collider.CompareTag("Shield")) {
                    co.collider.GetComponent<ITakeDamage>().TakeDamage(_damage, gameObject);
                    Crash(co);
                    Debug.Log($"{co.collider.tag}와 부딪힘");
                    if (co.collider.CompareTag("Shield"))
                        Debug.Log("투사체가 실드에 막힘");
                    return;
                }
            }
        }
        
    }
    private void Crash(Collision co) {
        collided = true;

        //if (trails.Count > 0) {
        //    for (int i = 0; i < trails.Count; i++) {
        //        trails[i].transform.parent = null;
        //        var ps = trails[i].GetComponent<ParticleSystem>();
        //        if (ps != null) {
        //            ps.Stop();
        //            StartCoroutine(Util.CoActive(false, ps.gameObject, ps.main.duration + ps.main.startLifetime.constantMax));
        //            CoDestroyPhoton(ps.gameObject, ps.main.duration + ps.main.startLifetime.constantMax); 
        //        }
        //    }
        //}

        speed = 0;
        GetComponent<Rigidbody>().isKinematic = true;

        ContactPoint contact = co.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;

        if (IsTestMode.Instance.CurrentUser == Define.User.Hw) {
            if (hitPrefab != null) {
                if (_creator.GetComponent<PhotonView>().IsMine)//0409 17:12 이희웅 파티클 생성에 대한 소유권 추가
                {
                    var hitVFX = PhotonNetwork.Instantiate("Prefabs/yb/Hits/default", pos, rot) as GameObject;
                    var ps = hitVFX.GetComponent<ParticleSystem>();
                    ps.AddComponent<VFXLifeController>().Init();
                }
                //if (ps == null) {
                //    var psChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                //    CoDestroyPhoton(hitVFX, psChild.main.duration);
                //} else
                //    CoDestroyPhoton(hitVFX, ps.main.duration);
            }
        }
        else {
            if (hitPrefab != null) {
                var hitVFX = Instantiate(hitPrefab, null) as GameObject;
                hitVFX.transform.position = pos;
                hitVFX.transform.rotation = rot;
                var ps = hitVFX.GetComponent<ParticleSystem>();
                ps.AddComponent<VFXLifeController>().Init();
            }
        }
           
            StartCoroutine(DestroyParticle(0f));
 }


        public IEnumerator DestroyParticle(float waitTime)
        {
            if (transform.childCount > 0 && waitTime != 0)
            {
                List<Transform> tList = new List<Transform>();

                foreach (Transform t in transform.GetChild(0).transform)
                {
                    tList.Add(t);
                }

                while (transform.GetChild(0).localScale.x > 0)
                {
                    yield return new WaitForSeconds(0.01f);
                    transform.GetChild(0).localScale -= new Vector3(0.1f, 0.1f, 0.1f);
                    for (int i = 0; i < tList.Count; i++)
                    {
                        tList[i].localScale -= new Vector3(0.1f, 0.1f, 0.1f);
                    }
                }
            }

            yield return new WaitForSeconds(waitTime);

        if(IsTestMode.Instance.CurrentUser == Define.User.Hw) {
            if (GetComponent<PhotonView>().IsMine)//0409 17:00 이희웅 총알에 대한 소유권 추가
                PhotonNetwork.Destroy(gameObject);
        }
        else {
            Managers.Resources.Destroy(gameObject);
        }
           
        }

}
