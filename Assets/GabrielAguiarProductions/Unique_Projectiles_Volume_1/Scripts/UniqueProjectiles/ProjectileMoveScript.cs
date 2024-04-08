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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using yb;

public class ProjectileMoveScript : MonoBehaviour {

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

	void Start () {
        rb = GetComponent<Rigidbody>();
	}

    public void Init(Quaternion rotate, int damage, GameObject creator) 
    {
        transform.localRotation = rotate;
        _damage = damage;
        _creator = creator;
    }
    
    public void Init(Quaternion rotate, int damage, Vector3 ran,  GameObject creator) {
        transform.localRotation = rotate;
        _damage = damage;
        _creator = creator;
    }
    
 

	void FixedUpdate () {
        Vector3 dir = new Vector3(transform.forward.x, 0f, transform.forward.z);
        rb.position += (dir) * (speed * Time.deltaTime);
    }


    void OnCollisionEnter(Collision co) {
        if (co.gameObject == _creator)
            return;

        if (co.collider.CompareTag("Bullet"))
            return;

        if (!collided) {
            if (co.collider.CompareTag("Obstacle")) {
                Crash(co);
                return;
            }

            if (co.collider.CompareTag("Player") || co.collider.CompareTag("DestructibleObject")) {
                co.collider.GetComponent<ITakeDamage>().TakeDamage(_damage, gameObject);
                Crash(co);
                return;
            }
        }
    }

    private void Crash(Collision co) {
        collided = true;

        if (trails.Count > 0) {
            for (int i = 0; i < trails.Count; i++) {
                trails[i].transform.parent = null;
                var ps = trails[i].GetComponent<ParticleSystem>();
                if (ps != null) {
                    ps.Stop();
                   // StartCoroutine(Util.CoActive(false, ps.gameObject, ps.main.duration + ps.main.startLifetime.constantMax));
                    Destroy(ps.gameObject, ps.main.duration + ps.main.startLifetime.constantMax);
                }
            }
        }

        speed = 0;
        GetComponent<Rigidbody>().isKinematic = true;

        ContactPoint contact = co.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;

        if (hitPrefab != null) {
            var hitVFX = Instantiate(hitPrefab, pos, rot) as GameObject;

            var ps = hitVFX.GetComponent<ParticleSystem>();
            if (ps == null) {
                var psChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(hitVFX, psChild.main.duration);
            } else
                Destroy(hitVFX, ps.main.duration);
        }
        StartCoroutine(DestroyParticle(0f));
    }

	public IEnumerator DestroyParticle (float waitTime) {

		if (transform.childCount > 0 && waitTime != 0) {
			List<Transform> tList = new List<Transform> ();

			foreach (Transform t in transform.GetChild(0).transform) {
				tList.Add (t);
			}		

			while (transform.GetChild(0).localScale.x > 0) {
				yield return new WaitForSeconds (0.01f);
				transform.GetChild(0).localScale -= new Vector3 (0.1f, 0.1f, 0.1f);
				for (int i = 0; i < tList.Count; i++) {
					tList[i].localScale -= new Vector3 (0.1f, 0.1f, 0.1f);
				}
			}
		}
		
		yield return new WaitForSeconds (waitTime);
		Destroy (gameObject);
	}

}
