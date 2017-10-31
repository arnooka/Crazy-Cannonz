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
	private EventSystem ES;
	[SerializeField]
	private GameObject resultsField;
	private GameObject selectedField;

	// Use this for initialization
	void Start () {
		selectedField = ES.firstSelectedGameObject;
		mainMenuBtn.onClick.AddListener (() => {
			//print("return");
			SoundManager.instance.musicSource.Stop();
			SoundManager.instance.musicSource.loop = true;
			SoundManager.instance.inMenu = true;
			SoundManager.instance.musicSource.clip = menuMusic;
			SoundManager.instance.musicSource.Play();
			SceneManager.LoadScene ("MainMenu");
		});

		Score1.text = MatchManager.getPlayerScore(1).ToString();
		Score2.text = MatchManager.getPlayerScore(2).ToString();
		Score3.text = MatchManager.getPlayerScore(3).ToString();
		Score4.text = MatchManager.getPlayerScore(4).ToString();

		rematchBtn.onClick.AddListener (() => {
			SceneManager.LoadScene ("Arena Mid");
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
	}
}
