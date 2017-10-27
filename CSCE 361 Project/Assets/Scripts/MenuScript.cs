using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

	public Slider matchTimerSlider;
	public Text matchTimerText;
	public Button optionsBtn, matchBtn, matchBackBtn, optionsBackBtn, matchStartBtn, exitBtn;
	public Slider musicSlider, soundFXSlider;
	public GameObject mainMenu, optionsMenu, matchMenu;


	// Use this for initialization
	void Start () {
        /// Get an instance of the sound manager, tell it that
        /// we're in a menu, and play menu music
        SoundManager soundManager = SoundManager.instance;
        soundManager.inMenu = true;
        soundManager.musicSource.Play();

        /// Button for returning from match menu
		matchBackBtn.onClick.AddListener (() => {
			mainMenu.SetActive(true);
			matchMenu.SetActive(false);
		});

        /// Button for returning from options menu
		optionsBackBtn.onClick.AddListener (() => {
			mainMenu.SetActive(true);
			optionsMenu.SetActive(false);
		});

        /// Button for entering options menu
		optionsBtn.onClick.AddListener (() => {
			optionsMenu.SetActive(true);
			mainMenu.SetActive(false);
		});

        /// Button for entering match menu
		matchBtn.onClick.AddListener (() => {
			matchMenu.SetActive (true);
			mainMenu.SetActive (false);
		});

        /// Button for starting a match
		matchStartBtn.onClick.AddListener (() => {
            //save time value in match settings
			MatchManager.matchTime = matchTimerSlider.value;

            /// Stop the menu music and tell the sound manager
            /// that we're no longer at the menu
            soundManager.musicSource.Stop();
            soundManager.inMenu = false;

            /// Hide the match menu
			matchMenu.SetActive(false);
			
            /// Load the level scene
            SceneManager.LoadScene ("Arena Mid");
		});

		musicSlider.onValueChanged.AddListener (delegate {
            //set music volume for sound manager and save
            soundManager.musicSource.volume = musicSlider.value;
			MatchManager.musicVolume = musicSlider.value;
		});

        soundFXSlider.onValueChanged.AddListener(delegate {
            //set sound effects volume for sound manager and save
            soundManager.EFXSource.volume = soundFXSlider.value;
            MatchManager.soundFXVolume = soundFXSlider.value;
        });

		exitBtn.onClick.AddListener (() => {
			Application.Quit ();
		});

	}

	// Update is called once per frame
	void Update () {

		matchTimerText.text = matchTimerSlider.value.ToString();

	}
}
