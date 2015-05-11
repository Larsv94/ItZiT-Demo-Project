using UnityEngine;
using System.Collections;

public class SoundAttenuationBox : MonoBehaviour {

	float soundDistanceBegin;
	BoxCollider triggerCollider;
	AudioSource audioPlayer;

	bool isEnabled = false;

	void Start () {

		KeyBoardEventManager.instance.RegisterKeyDown (KeyCode.P, new KeyEvent(Enable));//Register event on the key "P"
		
		triggerCollider= GetComponent<BoxCollider>();
		soundDistanceBegin= triggerCollider.size.y;//distance determined by box height
		
		audioPlayer = GetComponent<AudioSource> ();
		audioPlayer.volume = 0;
	}
	
	void OnTriggerStay(Collider other) {
		if (isEnabled) {
			if (other.tag.Contains ("MainCamera")) {
				if (!audioPlayer.isPlaying) {
					audioPlayer.Play ();//Only play sound when sound is not yet playing

				}
				float absoluteDistance = Mathf.Abs (this.transform.position.y - other.transform.position.y);
				float volume = 1 / ((absoluteDistance / soundDistanceBegin * 100));
				volume *= 10;//Making the volume a bit louder
				audioPlayer.volume = volume;
			}
		}
	}
	
	void OnTriggerExit(Collider other) {
		if (isEnabled) {
			if (other.tag.Contains ("MainCamera")) {
				if (audioPlayer.isPlaying) {
					audioPlayer.Stop ();
				}
				audioPlayer.volume = 0;
			}
		}
		
	}

	//Disables the this component so the ground doesn't keep emitting sound if the player wants to listen to some other object
	//Fired by a keyevent
	void Enable(KeyCode K){
		isEnabled = !isEnabled;
		if (!isEnabled && audioPlayer.isPlaying)
			audioPlayer.Stop ();

	}

}
