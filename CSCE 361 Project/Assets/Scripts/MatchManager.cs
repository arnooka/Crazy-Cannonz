using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchManager : MonoBehaviour {

	public static double matchTime = 600.0, soundFXVolume = 1.0;
	public static float musicVolume = 1.0f;

	public Text timer;
	public GameObject panel;

	public AudioSource theMatchMusic;

	// Use this for initialization
	void Start () {

		theMatchMusic.volume = musicVolume;
		
	}
	
	// Update is called once per frame
	void Update () {

		matchTime -= Time.deltaTime;
		//timer.text = matchTime.ToString();
		print(matchTime);

		//First check to see if the player wants to pause.
		if (Input.GetKey (KeyCode.P)) {
			panel.SetActive (false);
			print ("p button pressed");
		}
		
	}
}
