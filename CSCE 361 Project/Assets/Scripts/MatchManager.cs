using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchManager : MonoBehaviour {

	public AudioSource theMatchMusic;
<<<<<<< HEAD
	public GameObject panel;
	public Text text;
	public static double matchTime = 10.0, soundFXVolume = 1.0, timeRemaining = 0.0;

	public static int min = 0, sec = 0;
	public static string secStr = "";
=======
>>>>>>> 3b3110f54eb97a7dd493c25012df2350d29fc8b3

	// Use this for initialization
	void Start () {

		//theMatchMusic.volume = (float) MatchSettings.musicVolume;
		
	}
	
	// Update is called once per frame
	void Update () {
<<<<<<< HEAD
		
		timeRemaining = matchTime - Time.timeSinceLevelLoad;

		if (timeRemaining <= 0.0) {
			SceneManager.LoadScene ("PostMatch");
		}

		min = (int) timeRemaining / 60;
		sec = (int) timeRemaining % 60;

		if (sec < 10) {
			secStr = "0" + sec.ToString ();
		} else
			secStr = sec.ToString ();

		text.text = min.ToString () + ":" + secStr;
=======
>>>>>>> 3b3110f54eb97a7dd493c25012df2350d29fc8b3

		//matchTime -= Time.deltaTime;
		//timer.text = matchTime.ToString();
		//print(matchTime);
		//
		//First check to see if the player wants to pause.
		//if (Input.GetKey (KeyCode.P)) {
		//	panel.SetActive (false);
		//	print ("p button pressed");
		//}
		
	}
}
