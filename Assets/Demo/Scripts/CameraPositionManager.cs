using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CameraPositionManager : MonoBehaviour {

	[System.Serializable]
	public struct WayPoint{
		public KeyCode Key;
		public GameObject _Object;
	}

	public WayPoint[] waypoints;

	SpectatorMovement movementComponent;
	bool interp = false;
	Vector3 newLocation;


	// Use this for initialization
	void Start () {
		movementComponent = GetComponent<SpectatorMovement> ();

	}
	
	// Update is called once per frame
	void Update () {
		if(Input.anyKeyDown&&!interp){
			foreach(WayPoint point in waypoints){
				if(Input.GetKeyDown(point.Key)&&point._Object){
					newLocation = point._Object.transform.position;//Set new location if key pressed is linked to a object in the world
					interp = true;
					(GetComponent<SphereCollider> () as SphereCollider).isTrigger = true;
					movementComponent.setIsEnabled(false);
				}
			}
		}
		if (interp) {
			this.transform.position = Vector3.Lerp(this.transform.position, newLocation, 0.1f);
			if(Vector3.Distance(this.transform.position, newLocation)<0.1){
				this.transform.position = newLocation;
				interp=false;
				(GetComponent<SphereCollider> () as SphereCollider).isTrigger = false;
				movementComponent.setIsEnabled(true);
			}
		}
	
	}

}


