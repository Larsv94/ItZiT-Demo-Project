using UnityEngine;
using System.Collections;

public class CameraPositionManager : MonoBehaviour {

	public GameObject BeginTarget;
	public KeyCode BeginKey;
	public GameObject ScriptSoundTarget;
	public KeyCode ScriptKey;
	public GameObject ThreeDeeSoundTarget;
	public KeyCode ThreeDeeKey;
	public GameObject RoomTarget;
	public KeyCode RoomKey;

	SpectatorMovement movementComponent;
	bool interp = false;
	Vector3 newLocation;


	// Use this for initialization
	void Start () {
		movementComponent = GetComponent<SpectatorMovement> ();

		//Register all key events
		KeyBoardEventManager.instance.RegisterKeyDown (BeginKey,new KeyEvent(MoveToBegin));
		KeyBoardEventManager.instance.RegisterKeyDown (ScriptKey,new KeyEvent(MoveToScript));
		KeyBoardEventManager.instance.RegisterKeyDown (ThreeDeeKey,new KeyEvent(MoveTo3D));
		KeyBoardEventManager.instance.RegisterKeyDown (RoomKey,new KeyEvent(MoveToRoom));
	}
	
	// Update is called once per frame
	void Update () {
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
	#region KeyEvents
	public void MoveToBegin(KeyCode key){
		newLocation = BeginTarget.transform.position;
		interp = true;
		(GetComponent<SphereCollider> () as SphereCollider).isTrigger = true;
		movementComponent.setIsEnabled(false);
	}

	public void MoveToScript(KeyCode key){
		newLocation = ScriptSoundTarget.transform.position;
		interp = true;
		(GetComponent<SphereCollider> () as SphereCollider).isTrigger = true;
		movementComponent.setIsEnabled(false);
	}

	public void MoveTo3D(KeyCode key){
		newLocation = ThreeDeeSoundTarget.transform.position;
		interp = true;
		(GetComponent<SphereCollider> () as SphereCollider).isTrigger = true;
		movementComponent.setIsEnabled (false);
	}

	public void MoveToRoom(KeyCode key){
		newLocation = RoomTarget.transform.position;
		interp = true;
		(GetComponent<SphereCollider> () as SphereCollider).isTrigger = true;
		movementComponent.setIsEnabled (false);
	}
	#endregion
}
