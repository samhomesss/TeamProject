//
//NOTES:
//This script is used for DEMONSTRATION porpuses of the Projectiles. I recommend everyone to create their own code for their own projects.
//This is just a basic example.
//

using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RotateToMouseScript : MonoBehaviour {

	public float maximumLenght;

	private bool use2D;
	private Ray rayMouse;
	private Vector3 pos;
	private Vector3 direction;
	private Quaternion rotation;
	private Camera cam;
	private WaitForSeconds updateTime = new WaitForSeconds (0.01f);
	private PhotonView _photonView;//0408 11:30 이희웅 동기화를 위한 PhotonView 추가

	public void StartUpdateRay (){
		StartCoroutine (UpdateRay());
	}



	IEnumerator UpdateRay (){
		if (cam != null) {
			if (use2D) {
				Vector2 direction = Camera.main.ScreenToWorldPoint (Input.mousePosition) - transform.position;
				float angle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg;
				if (angle > 180) angle -= 360;
				rotation.eulerAngles = new Vector3 (-angle, 90, 0); // use different values to lock on different axis
				transform.rotation = rotation;
			} else {
				RaycastHit hit;
				var mousePos = Input.mousePosition;
				rayMouse = cam.ScreenPointToRay (mousePos);
				if (Physics.Raycast (rayMouse.origin, rayMouse.direction, out hit, maximumLenght)) {
					RotateToMouse (gameObject, hit.point);
				} else {	
					var pos = rayMouse.GetPoint (maximumLenght);
					RotateToMouse (gameObject, pos);
				}
			}
			yield return updateTime;
			StartCoroutine (UpdateRay ());
		} else
			Debug.Log ("Camera not set");
	}

    private void Start()
    {
        _photonView = GetComponent<PhotonView>();
    }
    public void RotateToMouse (GameObject obj, Vector3 destination ) {
		//todo
		if(IsTestMode.Instance.CurrentUser == Define.User.Hw)
		{
            if (_photonView.IsMine)//0408 11:30 이희웅 개별동작을 위한 조건부 추가
            {
                direction = destination - obj.transform.position;
                Vector3 dir = new Vector3(direction.x, 0f, direction.z);
                rotation = Quaternion.LookRotation(dir);
                obj.transform.localRotation = Quaternion.Lerp(obj.transform.rotation, rotation, 1);
            }
        }
	}



    public void Set2D (bool state){
		use2D = state;
	}

	public void SetCamera (Camera camera){
		cam = camera;
	}

	public Vector3 GetDirection () {
		return direction;
	}

	public Quaternion GetRotation () {
		return rotation;
	}
}
