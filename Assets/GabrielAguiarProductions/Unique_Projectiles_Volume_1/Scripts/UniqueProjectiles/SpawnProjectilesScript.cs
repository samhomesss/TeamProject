//
//NOTES:
//This script is used for DEMONSTRATION porpuses of the Projectiles. I recommend everyone to create their own code for their own projects.
//This is just a basic example.
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnProjectilesScript : MonoBehaviour {

    public bool useTarget;
	public bool use2D;
	public RotateToMouseScript rotateToMouse;
	public GameObject cameras;
    public GameObject target;

	private List<Camera> camerasList = new List<Camera> ();
	private Camera singleCamera;

	void Start () {

		if (cameras.transform.childCount > 0) {
			for (int i = 0; i < cameras.transform.childCount; i++) {
				camerasList.Add (cameras.transform.GetChild (i).gameObject.GetComponent<Camera> ());
			}
			if(camerasList.Count == 0){
				Debug.Log ("Please assign one or more Cameras in inspector");
			}
		} else {
			singleCamera = cameras.GetComponent<Camera> ();
			if (singleCamera != null)
				camerasList.Add (singleCamera);
			else
				Debug.Log ("Please assign one or more Cameras in inspector");
		}

		if (camerasList.Count > 0) {
			rotateToMouse.SetCamera (camerasList [camerasList.Count - 1]);
			if(use2D)
				rotateToMouse.Set2D (true);
			rotateToMouse.StartUpdateRay ();
		}
		else
			Debug.Log ("Please assign one or more Cameras in inspector");

        if (useTarget && target != null)
        {
            var collider = target.GetComponent<BoxCollider>();
            if (!collider)
            {
                target.AddComponent<BoxCollider>();
            }
        }
    }
}
