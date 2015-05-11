using UnityEngine;
using System.Collections;

public class SpectatorMovement : MonoBehaviour {

	public float lookSpeed = 5.0f;
	public float moveSpeed = 1.0f;
	public bool requireMouseButton; //Used to determine wheter the press of a mouse button is required to move the camera

	float RotationX;
	float RotationY;

	bool isEnabled=true;

	public void setIsEnabled(bool value){
		isEnabled = value;
	}
	void Start(){

		//Quit game when Escape is pressed
		KeyBoardEventManager.instance.RegisterKeyDown (KeyCode.Escape, delegate(KeyCode k) {
			Application.Quit();
	});
	}

	// Update is called once per frame
	void Update () {
		
		if(!requireMouseButton&&isEnabled)//Move Camera when button click is not required
		{
			moveCamera();
		}
		else if(requireMouseButton && Input.GetAxis("Fire2")>0&&isEnabled)//Move camera only on right mouseclick if button click is required
		{
			moveCamera();
		}

	}

	void moveCamera(){
		//Get rotation from mouse
		RotationX += Input.GetAxis("Mouse X")*lookSpeed;
		RotationY += Input.GetAxis("Mouse Y")*lookSpeed;
		RotationY = Mathf.Clamp (RotationY, -90, 90);
		
		transform.localRotation = Quaternion.AngleAxis(RotationX, Vector3.up);
		transform.localRotation *= Quaternion.AngleAxis(RotationY, Vector3.left);
		
		transform.position += transform.forward*moveSpeed*Input.GetAxis("Vertical");
		transform.position += transform.right*moveSpeed*Input.GetAxis("Horizontal");
	}
}
