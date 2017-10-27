using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SliderScript : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerDownHandler, IPointerUpHandler {

	public AudioSource source;
	public Slider slideFX;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnSelect(BaseEventData eventData) {
		Debug.Log ("fx slider selected");
	}

	public void OnDeselect(BaseEventData eventData) {
		Debug.Log ("fx slider deselected");
	}

	public void OnPointerDown(PointerEventData eventData) {
		Debug.Log ("slider clicked");
		source.volume = slideFX.value;
		MatchManager.soundFXVolume = slideFX.value;
		source.Play ();
	}

	public void OnPointerUp(PointerEventData eventData) {
		Debug.Log ("slider unclicked");
		source.volume = slideFX.value;
		MatchManager.soundFXVolume = slideFX.value;
		source.Play ();
	}
}
