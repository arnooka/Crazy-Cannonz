using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

	public Slider matchTimerSlider;
	public Text matchTimerText;
	public Button optionsBtn, matchBtn, matchBackBtn, optionsBackBtn, matchStartBtn;
	public Slider musicSlider, soundFXSlider;
	public GameObject mainMenu, optionsMenu, matchMenu;
	public AudioSource menuMusic;

	// Use this for initialization
	void Start () {

		matchBackBtn.onClick.AddListener (() => {
			mainMenu.SetActive(true);
			matchMenu.SetActive(false);
		});

		optionsBackBtn.onClick.AddListener (() => {
			mainMenu.SetActive(true);
			optionsMenu.SetActive(false);
		});

		optionsBtn.onClick.AddListener (() => {
			optionsMenu.SetActive(true);
			mainMenu.SetActive(false);
		});

		matchBtn.onClick.AddListener (() => {
			matchMenu.SetActive (true);
			mainMenu.SetActive (false);
		});

		matchStartBtn.onClick.AddListener (() => {
			MatchManager.matchTime = (double) matchTimerSlider.value * 60.0;
			matchMenu.SetActive(false);

			//This is only because menuMusic.Pause() and menuMusic.Stop() both seem to crash Unity for some reason...
			menuMusic.volume = 0;
			SceneManager.LoadScene ("Arena Mid");
		});

		musicSlider.onValueChanged.AddListener (delegate {
			menuMusic.volume = musicSlider.value;
			MatchManager.musicVolume = (float) musicSlider.value;
		});

	}

	// Update is called once per frame
	void Update () {

		matchTimerText.text = matchTimerSlider.value.ToString();

	}
		
}
