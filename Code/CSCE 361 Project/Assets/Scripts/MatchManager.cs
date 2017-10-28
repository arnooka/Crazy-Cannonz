using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MatchManager : MonoBehaviour {

	public AudioSource theMatchMusic;
	public GameObject panel;
	public Text activeTime;
	public Button leaveMatch;
	public static double matchTime = 10.0, soundFXVolume = 1.0, timeRemaining = 0.0, pauseMenuOffsetTime = 0.0;
	public static double musicVolume = 1.0;
	private static bool isActive = true;

	public static int min = 0, sec = 0;
	public static string secStr = "";

	// Use this for initialization
	void Start () {

		leaveMatch.onClick.AddListener (() => {
			isActive = true;
			pauseMenuOffsetTime = 0.0;
			SceneManager.LoadScene ("PostMatch");
		});

	}
	
	// Update is called once per frame
	void Update () {

		if (isActive) {
			timeRemaining = matchTime - Time.timeSinceLevelLoad + pauseMenuOffsetTime;

			if (timeRemaining <= 0.0) {
				SceneManager.LoadScene ("PostMatch");
			}

			min = (int)timeRemaining / 60;
			sec = (int)timeRemaining % 60;

			if (sec < 10) {
				secStr = "0" + sec.ToString ();
			} else
				secStr = sec.ToString ();

			activeTime.text = min.ToString () + ":" + secStr;
		} else if (!isActive) {
			pauseMenuOffsetTime += Time.deltaTime;
		}

		//First check to see if the player wants to pause.
		if (Input.GetKeyUp (KeyCode.P)) {

			isActive = !isActive;
			panel.SetActive (!isActive);

		}
		
	}

	public static bool getIsActive() {
		return isActive;
	}
}
