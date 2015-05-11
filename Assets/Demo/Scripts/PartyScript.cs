using UnityEngine;
using System.Collections;

public class PartyScript: MonoBehaviour {

	ColorLights lightMechanism;
	AudioSource radio;
	// Use this for initialization

	bool openDoor = false;

	Vector3 closed;
	Vector3 opend;
	Transform hinge;

	void Start () {
		hinge  = transform.FindChild("Hinge");//Get hinge transform
		closed = hinge.transform.eulerAngles;//Calculate default closed rotation
		opend = new Vector3 (closed.x, closed.y + 90, closed.z);//Calculate default opend rotation
		lightMechanism = GetComponentInChildren<ColorLights> ();//Get ColorLights component
		radio = GetComponentInChildren<AudioSource> ();//Get AudioSource component
	}

	void Update()
	{
		if(openDoor){
			RotateDoor(opend);
		}else{
			RotateDoor(closed);
		}
	}

	void RotateDoor(Vector3 rot){
		if(hinge){
			hinge.eulerAngles = Vector3.Slerp(hinge.eulerAngles, rot, Time.deltaTime * 2);//Rotate door based on desired vector rotation
		}
	}

	void OnTriggerEnter(Collider other){
		print("Enter");
		if (other.tag.Contains ("MainCamera")) {
			print("Enter");
			openDoor = true;
			lightMechanism.setIsEnabled(true);
			radio.Play();
		}
	}

	void OnTriggerExit(Collider other){
		if (other.tag.Contains ("MainCamera")) {
			print("Exit");
			openDoor = false;
			lightMechanism.setIsEnabled(false);
			radio.Stop();
		}
	}



}
