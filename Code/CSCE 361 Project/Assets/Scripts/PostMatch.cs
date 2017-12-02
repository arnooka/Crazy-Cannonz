using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PostMatch : MonoBehaviour {

	[SerializeField]
	private Button mainMenuBtn, rematchBtn;
	[SerializeField]
	private AudioClip menuMusic;

	[SerializeField]
	private Text Score1, Score2, Score3, Score4;

	[SerializeField]
	private EventSystem eventSystem;
	[SerializeField]
	private GameObject resultsField;
	private GameObject selectedField;

	// Use this for initialization
	void Start () {
		// Save the first selected field from event system
		selectedField = eventSystem.firstSelectedGameObject;

		// Initializing text fields
		Score1.text = 0.ToString();
		Score2.text = 0.ToString();
		Score3.text = 0.ToString();
		Score4.text = 0.ToString();

		mainMenuBtn.onClick.AddListener (() => {
			// Change current music to menu music and load "MainMenu"
			SoundManager.getInstance().getMusicSource().Stop();
			SoundManager.getInstance().getMusicSource().loop = true;
			SoundManager.getInstance().setInMenu(true);
			SoundManager.getInstance().getMusicSource().clip = menuMusic;
			SoundManager.getInstance().getMusicSource().Play();
			SceneManager.LoadScene ("MainMenu");
		});

		// Set score text panels with player scores
		Score1.text = MatchManager.GetPlayerScore(1).ToString();
		Score2.text = MatchManager.GetPlayerScore(2).ToString();
		Score3.text = MatchManager.GetPlayerScore(3).ToString();
		Score4.text = MatchManager.GetPlayerScore(4).ToString();

		rematchBtn.onClick.AddListener (() => {
            SceneManager.LoadScene (MapToggle.GetMapName());
		});

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
}
