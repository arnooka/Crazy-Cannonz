using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MatchManager : MonoBehaviour {

	public AudioSource theMatchMusic;

	public GameObject panel;
	public Text text;
	public static double matchTime = 10.0, soundFXVolume = 1.0, timeRemaining = 0.0;
	public static double musicVolume = 1.0;
	private static bool isActive = false;

	public static int min = 0, sec = 0;
	public static string secStr = "";

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {

		if (isActive) {
			timeRemaining = matchTime - Time.timeSinceLevelLoad;

			if (timeRemaining <= 0.0) {
				SceneManager.LoadScene ("PostMatch");
			}

			min = (int)timeRemaining / 60;
			sec = (int)timeRemaining % 60;

			if (sec < 10) {
				secStr = "0" + sec.ToString ();
			} else
				secStr = sec.ToString ();

			text.text = min.ToString () + ":" + secStr;
		}

		//First check to see if the player wants to pause.
		if (Input.GetKey (KeyCode.P)) {

			isActive = false;
			panel.SetActive (true);
		}
		
	}
}
