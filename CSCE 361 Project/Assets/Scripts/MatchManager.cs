using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchManager : MonoBehaviour {

	public Text timer;
	public GameObject panel;

	public AudioSource theMatchMusic;

	// Use this for initialization
	void Start () {

		theMatchMusic.volume = (float) MatchSettings.musicVolume;
		
	}
	
	// Update is called once per frame
	void Update () {

		//timer.text = MatchSettings.matchTime.ToString();

		//First check to see if the player wants to pause.
		if (Input.GetKey (KeyCode.P)) {
			panel.SetActive (false);
			print ("p button pressed");
		}
		
	}
}
