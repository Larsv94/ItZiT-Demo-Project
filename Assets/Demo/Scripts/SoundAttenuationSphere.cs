using UnityEngine;
using System.Collections;

//Object requires:
//SoundSource
//SphereTrigger

public class SoundAttenuationSphere : MonoBehaviour {

	float soundDistanceBegin;
	SphereCollider triggerCollider;
	AudioSource audioPlayer;


	// Use this for initialization
	void Start () {
		triggerCollider= GetComponent<SphereCollider>();
		soundDistanceBegin= triggerCollider.radius;//distance determined by sphere radius

		audioPlayer = GetComponent<AudioSource> ();
		audioPlayer.volume = 0;//Make sure audio volume starts at zero
	}

	void OnTriggerStay(Collider other) {
		if (other.tag.Contains ("MainCamera")) {
			if(!audioPlayer.isPlaying){
				audioPlayer.Play();//Only play sound when sound is not yet playing
			}
			float absoluteDistance = Mathf.Abs (Vector3.Distance (this.transform.position, other.transform.position));
			float volume = 1 / ((absoluteDistance / soundDistanceBegin * 100));//Reverse percentage and convert to value between 0 and 1
			volume *= 10;//Making the volume a bit louder
			audioPlayer.volume = volume;
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.tag.Contains ("MainCamera")) {
			if(audioPlayer.isPlaying){
				audioPlayer.Stop();
			}
			audioPlayer.volume = 0;//Make sure volume starts at zero next time the camera enters the detection sphere
		}

	}

}
