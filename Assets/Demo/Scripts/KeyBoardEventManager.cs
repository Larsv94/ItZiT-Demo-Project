using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//Keyevent manager. Should be placed in the world as empty object
//Though world placement is not necessery. If class is called without a object present in world a object is created on runtime
public delegate void KeyEvent (KeyCode k);
public delegate void GeneralKeyEvent (KeyCode k, KeyEventType type);

public enum KeyEventType{
	INPUT_PRESSED,
	INPUT_RELEASED
};
public class KeyBoardEventManager : MonoBehaviour {


	private static KeyBoardEventManager m_Instance;
	public static KeyBoardEventManager instance
	{
		get
		{
			if( m_Instance == null ) //create GameObject with KeyboardEventManager if world lacks this instance
			{
				m_Instance = GameObject.FindObjectOfType( typeof( KeyBoardEventManager ) ) as KeyBoardEventManager;
				if( m_Instance == null )
					m_Instance = new GameObject( "KeyBoardEventManager Instance", typeof( KeyBoardEventManager ) ).GetComponent< KeyBoardEventManager >();
				m_Instance.Initialize();
			}
			return m_Instance;
		}
	}
	
	void Awake() 
	{
		if( m_Instance == null )
		{
			m_Instance = this;
			m_Instance.Initialize();
		}
	}

	private List<KeyCode> registerdKeys;//Used to determine which keys have to be checked on Update
	private Dictionary <KeyCode, KeyEvent> keyDownEvents, keyUpEvents;//Dictionary with all register events for up or down presses
	private Dictionary <KeyCode, GeneralKeyEvent> generalKeyEvents;//Dictionary with all registered events for general key events

	public void Initialize(){
		keyUpEvents = new Dictionary<KeyCode, KeyEvent> ();
		keyDownEvents = new Dictionary<KeyCode, KeyEvent> ();
		generalKeyEvents = new Dictionary<KeyCode, GeneralKeyEvent> ();
		registerdKeys = new List<KeyCode>();
	}

	#region  KeyRegestration

	/**
		Register key event regardless whether the key is pressed or released
		@param key The keycode of the key that will fire the event
		@param _Event The generalkeyevent that will be fired if the key is pressed or released
	 */
	public void RegisterKey(KeyCode key, GeneralKeyEvent _Event){
		if(generalKeyEvents.ContainsKey(key)){
			generalKeyEvents[key] += _Event;//Add event to existing key
		}else{
			if(!registerdKeys.Contains(key)) registerdKeys.Add(key);
			generalKeyEvents.Add(key, _Event);//Add key and event in dictionary
		}
	}

	/**
		Register key event when the key is released
		@param key The keycode of the key that will fire the event
		@param _Event The KeyEvent that will be fired if the key is released
	 */
	public void RegisterKeyUp(KeyCode key, KeyEvent _Event){
		if(keyUpEvents.ContainsKey(key)){
			keyUpEvents[key] += _Event;//Add event to existing key
		}else{
			if(!registerdKeys.Contains(key)) registerdKeys.Add(key);
			keyUpEvents.Add(key, _Event);//Add key and event in dictionary
		}
	}

	/**
		Register key event when the key is pressed
		@param key The keycode of the key that will fire the event
		@param _Event The KeyEvent that will be fired if the key is pressed
	 */
	public void RegisterKeyDown(KeyCode key, KeyEvent _Event){

		if(keyDownEvents.ContainsKey(key)){
			keyDownEvents[key] += _Event;//Add event to existing key
		}else{
			if(!registerdKeys.Contains(key)) registerdKeys.Add(key);
			keyDownEvents.Add(key, _Event);//Add key and event in dictionary
		}
	}
	#endregion

	#region KeyUnRegestration

	/**
		Unregister the event
		Also unregisters the key if the key doesn't have any events attached
		@param key The key to which the event is attached
		@param _Event The event that has to be unregisterd
	 */
	public void UnRegisterKey(KeyCode key, GeneralKeyEvent _Event){
		if( generalKeyEvents.ContainsKey(key) ){
			generalKeyEvents[key] -= _Event;
			if( generalKeyEvents[key] == null )
				generalKeyEvents.Remove(key);
		}
	}

	/**
		Unregister the event
		Also unregisters the key if the key doesn't have any events attached
		@param key The key to which the event is attached
		@param _Event The event that has to be unregisterd
	 */
	public void UnRegisterKeyUp(KeyCode key, KeyEvent _Event){
		if( keyUpEvents.ContainsKey(key) ){
			keyUpEvents[key] -= _Event;
			if( keyUpEvents[key] == null )
				keyUpEvents.Remove(key);
		}
	}
	/**
		Unregister the event
		Also unregisters the key if the key doesn't have any events attached
		@param key The key to which the event is attached
		@param _Event The event that has to be unregisterd
	 */	
	public void UnRegisterKeyDown(KeyCode key, KeyEvent _Event){
		if( keyDownEvents.ContainsKey(key) ){
			keyDownEvents[key] -= _Event;
			if( keyDownEvents[key] == null )
				keyDownEvents.Remove(key);
		}
	}

	/**
		Removes the key from all types of events
		@param key The key that has to be removed
	 */
	public void RemoveKeyFromAll(KeyCode key){
		if( keyDownEvents.ContainsKey( key ) ) keyDownEvents.Remove( key );
		if( keyUpEvents.ContainsKey( key ) ) keyUpEvents.Remove( key );
		if( generalKeyEvents.ContainsKey(key)) generalKeyEvents.Remove(key);
		if( registerdKeys.Contains( key ) ) registerdKeys.Remove( key );
	}
	#endregion

	#region Detection
	// Update is called once per frame
	void Update () {
		foreach (KeyCode code in registerdKeys) {
			if( Input.GetKeyDown(code) ){
				OnKeyDown( code );
				OnKey(code, KeyEventType.INPUT_PRESSED);
			}
			
			if( Input.GetKeyUp(code) ){
				OnKeyUp( code );
				OnKey(code, KeyEventType.INPUT_RELEASED);
			}

		}
	}
	//Function that tries to get and fire a event when the key is pressed
	private void OnKeyDown( KeyCode key )
	{
		KeyEvent E = null;
		if( keyDownEvents.TryGetValue(key, out E) ) 
			if( E != null )
				E(key);
	}
	//Function that tries to get and fire a event when the key is released
	private void OnKeyUp( KeyCode key )
	{
		KeyEvent E = null;
		if( keyUpEvents.TryGetValue(key, out E) )
			if( E != null )
				E(key);
	}
	//Function that tries to get and fire a event regardless wheter the key is pressed or released
	private void OnKey(KeyCode key, KeyEventType eventType){
		GeneralKeyEvent E = null;
		if( generalKeyEvents.TryGetValue(key, out E) )
			if( E != null )
				E(key, eventType);
	}



	#endregion
}
