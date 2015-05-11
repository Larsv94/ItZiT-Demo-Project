using UnityEngine;
using System.Collections;


//Object requires:
//SoundSource
//Trigger
public class RoomSoundScript : MonoBehaviour {

	public AudioClip EntryRoomSound;
	public AudioClip[] InsideRoomSounds;
	public AudioClip ExitRoomSound;

	AudioSource AudioPlayer;

	int random = 0;

	// Use this for initialization
	void Start () {
		AudioPlayer = GetComponent<AudioSource> ();
		AudioPlayer.loop = false;
	}

	//Fired when trigger is entered by object
	void OnTriggerEnter(Collider other) {
		if (other.tag.Contains ("MainCamera")) {//Only do stuff when the collider is the main camera
			AudioPlayer.clip= EntryRoomSound;
			AudioPlayer.Play();
		}
	}
	//Fired when collider stays whitin the trigger
	void OnTriggerStay(Collider other) {
		if (other.tag.Contains ("MainCamera")) {//Only do stuff when the collider is the main camera
			if (!AudioPlayer.isPlaying) {
				int rnd = Random.Range (0, InsideRoomSounds.Length);
				while(rnd==random){//While loop to ensure the next sound to be played isn't the same as the previous
					rnd = Random.Range (0, InsideRoomSounds.Length);
				}
				random=rnd;
				AudioPlayer.clip= InsideRoomSounds [random];
				AudioPlayer.Play();
			}
		}
	}

	//Fired when trigger is left by object
	void OnTriggerExit(Collider other) {
		if (other.tag.Contains ("MainCamera")) {//Only do stuff when the collider is the main camera
			AudioPlayer.clip= ExitRoomSound;
			AudioPlayer.Play();
		}
	}

}
