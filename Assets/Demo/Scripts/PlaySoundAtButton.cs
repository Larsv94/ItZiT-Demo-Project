using UnityEngine;
using System.Collections;


//To use this script the object requires a AudioSource Component
public class PlaySoundAtButton : MonoBehaviour {

	AudioSource sound;
	// Use this for initialization
	void Start () {
		KeyBoardEventManager.instance.RegisterKeyDown (KeyCode.T, new KeyEvent( PlaySound));
		sound = GetComponent<AudioSource> ();
	}
	/**
	Event for KeyboardEventManager
	@param key The keycode from the key that has been pressed when the event is fired
	 */
	void PlaySound(KeyCode key){
		if (!sound.isPlaying) {
			sound.Play ();
		}
	}

}
