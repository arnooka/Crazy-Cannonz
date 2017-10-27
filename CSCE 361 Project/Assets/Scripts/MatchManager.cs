using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MatchManager : MonoBehaviour {

	public AudioSource theMatchMusic;
	public GameObject panel;
	public Text text;
	public static double matchTime = 600.0, soundFXVolume = 1.0, timeRemaining = 0.0;
	public static int min = 0, sec = 0;

	// Use this for initialization
	void Start () {
		timeRemaining = matchTime;
	}
	
	// Update is called once per frame
	void Update () {

		timeRemaining = matchTime - Time.timeSinceLevelLoad;

		if (timeRemaining <= 0.0) {
			//SceneManager.LoadScene ("blah");
		}

		min = (int) timeRemaining / 60;
		sec = (int) timeRemaining % 60;

		text.text = min.ToString () + ":" + sec.ToString ();

		//First check to see if the player wants to pause.
		if (Input.GetKey (KeyCode.P)) {
			panel.SetActive (false);
			print ("p button pressed");
		}
		
	}
}
