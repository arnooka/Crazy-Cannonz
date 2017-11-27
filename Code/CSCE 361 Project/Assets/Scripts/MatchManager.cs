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

	private static float matchTime = 10.0f, timeRemaining = 0.0f, pauseMenuOffsetTime = 5.0f;
	private static bool isActive = true, isCountdown = true;
	private static List<int> scores;

	[SerializeField]
	private EventSystem eventSystem;
	[SerializeField]
	private GameObject pauseField;
	private GameObject selectedField;

	//public static int score1, score2, score3, score4;

	public static int min = 0, sec = 0;
	public static string secStr = "";

	void Start () {
		// Set music volume and save the first selected field from the event system
		selectedField = eventSystem.firstSelectedGameObject;

		isActive = true;
		isCountdown = true;
		pauseMenuOffsetTime = 5.0f;

		//print (isCountdown);
		countdownPanel.SetActive (isCountdown);
		scores = new List<int>() {0,0,0,0};
		
		leaveMatch.onClick.AddListener (() => {
			SceneManager.LoadScene ("PostMatch");
			eventSystem.SetSelectedGameObject(pauseField);
		});

	}
	
	void Update () {
		// Initial 5 second countdown
		if (isCountdown) {
			countdownTime.text = (5 - (int) Time.timeSinceLevelLoad).ToString();
			if (Time.timeSinceLevelLoad > 5.0)
				isCountdown = !isCountdown;
		} else {
			countdownPanel.SetActive (false);
		}

		// Set active timer
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

		// Pause input check
		if (Input.GetButtonDown ("Pause")) {
			isActive = !isActive;
			panel.SetActive (!isActive);
			eventSystem.SetSelectedGameObject(pauseField);
		}
	}

	void FixedUpdate () {
		// Prevents "no button selected" issue
		if (eventSystem.currentSelectedGameObject != selectedField) {
			if (eventSystem.currentSelectedGameObject == null) {
				eventSystem.SetSelectedGameObject(selectedField);
			} else {
				selectedField = eventSystem.currentSelectedGameObject;
			}
		}
	}

	public static bool GetIsActive() {
		return isActive;
	}

	public static bool GetIsCountdown() {
		return isCountdown;
	}

	public static void SetMatchTime(float value) {
		matchTime = value;
	}

	public static int GetPlayerScore(int i) {
		return scores[i-1];
	}

	public static void SetPlayerScore(int playerNumber, int score) {
		scores[playerNumber - 1] = score;
	}
}
