using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MatchManager : MonoBehaviour {

	[SerializeField]
	private GameObject panel, countdownPanel;
	[SerializeField]
	private Text activeTime, countdownTime;
	[SerializeField]
	private Button leaveMatch;

	private static double matchTime = 10.0, soundFXVolume = 1.0, timeRemaining = 0.0, pauseMenuOffsetTime = 5.0;
	private static double musicVolume = 1.0;
	private static bool isActive = true, isCountdown = true;
	private static List<int> scores;

	[SerializeField]
	private EventSystem ES;
	[SerializeField]
	private GameObject pauseField;
	private GameObject selectedField;

	//public static int score1, score2, score3, score4;

	public static int min = 0, sec = 0;
	public static string secStr = "";

	// Use this for initialization
	void Start () {
		selectedField = ES.firstSelectedGameObject;
		isActive = true;
		isCountdown = true;
		pauseMenuOffsetTime = 5.0;

		//print (isCountdown);
		countdownPanel.SetActive (isCountdown);
		scores = new List<int>() {0,0,0,0};
		
		leaveMatch.onClick.AddListener (() => {
			SceneManager.LoadScene ("PostMatch");
			ES.SetSelectedGameObject(pauseField);
		});

	}
	
	// Update is called once per frame
	void Update () {
		if (ES.currentSelectedGameObject != selectedField) {
			if (ES.currentSelectedGameObject == null) {
				ES.SetSelectedGameObject(selectedField);
			} else {
				selectedField = ES.currentSelectedGameObject;
			}
		}

		if (isCountdown) {
			countdownTime.text = (5 - (int) Time.timeSinceLevelLoad).ToString();
			if (Time.timeSinceLevelLoad > 5.0)
				isCountdown = !isCountdown;
		} else {
			countdownPanel.SetActive (false);
		}

		if (isActive && !isCountdown) {
			timeRemaining = matchTime - Time.timeSinceLevelLoad + pauseMenuOffsetTime;

			if (timeRemaining <= 0.0) {
				SceneManager.LoadScene ("PostMatch");
			}

			min = (int)timeRemaining / 60;
			sec = (int)timeRemaining % 60;

			if (sec < 10) {
				secStr = "0" + sec.ToString();
			} else {
				secStr = sec.ToString();
			}

			activeTime.text = min.ToString () + ":" + secStr;
		} else if (!isActive) {
			pauseMenuOffsetTime += Time.deltaTime;
		}

		//First check to see if the player wants to pause.
		if (Input.GetButtonDown ("Pause")) {
			isActive = !isActive;
			panel.SetActive (!isActive);
			ES.SetSelectedGameObject(pauseField);
		}
		
	}

	public static bool getIsActive() {
		return isActive;
	}

	public static bool getIsCountdown() {
		return isCountdown;
	}

	public static void setMatchTime(double value) {
		matchTime = value;
	}

	public static void setSoundFXVolume(double value) {
		soundFXVolume = value;
	}

	public static void setMusicVolume(double value) {
		musicVolume = value;
	}

	public static int getPlayerScore(int i) {
		return scores[i-1];
	}

	public static void setPlayerScore(int playerNumber, int score) {
		scores[playerNumber - 1] = score;
	}
}
