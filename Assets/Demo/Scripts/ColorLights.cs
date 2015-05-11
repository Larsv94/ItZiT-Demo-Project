using UnityEngine;
using System.Collections;

public class ColorLights : MonoBehaviour {

	Light[] lights ;
	// Use this for initialization

	float timer = 1;
	bool isEnabled=false;

	void Start () {
		lights = GetComponentsInChildren<Light> ();//Get all light components in the children of this object

	}
	public void setIsEnabled(bool Value){

		isEnabled = Value;
	}

	
	// Update is called once per frame
	void Update () {
		if (isEnabled) {
			timer -= Time.deltaTime;//Count down timer
			if (timer <= 0) {
				newColor ();
				timer = 1;//Restore timer
			}
		}
	}
	

	//Sets new color for all lights
	void newColor(){
		for (int i = 0; i<lights.Length; i++) {
			lights[i].color = new Color(Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f));
		
		}
	}
}
