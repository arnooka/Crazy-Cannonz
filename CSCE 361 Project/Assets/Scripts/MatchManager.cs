using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchManager : MonoBehaviour {

	public AudioSource theMatchMusic;

	// Use this for initialization
	void Start () {

		theMatchMusic.volume = (float) MatchSettings.musicVolume;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
