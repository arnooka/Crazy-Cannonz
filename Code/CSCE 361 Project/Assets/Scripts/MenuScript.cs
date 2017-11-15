using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {
	
	[SerializeField]
	private Slider matchTimerSlider;
	[SerializeField]
	private Text matchTimerText;
	[SerializeField]
	private Button optionsBtn, localMatchBtn, onlineMatchBtn, matchBackBtn, optionsBackBtn, matchStartBtn, exitBtn;
	[SerializeField]
	private Slider musicSlider, soundFXSlider;
	[SerializeField]
	private GameObject mainMenu, optionsMenu, matchMenu;
	
	[SerializeField]
	private EventSystem eventSystem;
	private GameObject selectedField;
	
	[SerializeField]
	private GameObject mainMenuField, matchField, optionsField;

	// Use this for initialization
	void Start () {
		// Save the first selected field from the event system
		selectedField = eventSystem.firstSelectedGameObject;
		
		// Get an instance of the sound manager, tell it that
		// we're in a menu, and play menu music
		SoundManager.getInstance().setInMenu(true);
		SoundManager.getInstance().getMusicSource().Play();

		// Button for returning from match menu
		matchBackBtn.onClick.AddListener(() => {
			mainMenu.SetActive(true);
			matchMenu.SetActive(false);
			eventSystem.SetSelectedGameObject(mainMenuField);
		});

		// Button for returning from options menu
		optionsBackBtn.onClick.AddListener(() => {
			mainMenu.SetActive(true);
			optionsMenu.SetActive(false);
			eventSystem.SetSelectedGameObject(mainMenuField);
		});

		// Button for entering options menu
		optionsBtn.onClick.AddListener(() => {
			optionsMenu.SetActive(true);
			mainMenu.SetActive(false);
			eventSystem.SetSelectedGameObject(optionsField);
		});

		// Button for entering local match menu
		localMatchBtn.onClick.AddListener(() => {
			matchMenu.SetActive(true);
			mainMenu.SetActive(false);
			eventSystem.SetSelectedGameObject(matchField);
		});

		// Button for entering online match menu
		onlineMatchBtn.onClick.AddListener(() =>{
			Debug.Log("Entering Online Menu");
		});


		// Button for starting a match
		matchStartBtn.onClick.AddListener(() => {
			//save time value in match settings
			MatchManager.setMatchTime(matchTimerSlider.value * 60.0f);

			// Stop the menu music and tell the sound manager
			// that we're no longer at the menu
			SoundManager.getInstance().getMusicSource().Stop();
			SoundManager.getInstance().setInMenu(false);

			// Hide the match menu
			matchMenu.SetActive(false);

			// Load the level scene
			SceneManager.LoadScene("Arena Mid");
		});

		musicSlider.onValueChanged.AddListener(delegate {
			//set music volume for sound manager and save
			SoundManager.getInstance().getMusicSource().volume = musicSlider.value;
		});

		soundFXSlider.onValueChanged.AddListener(delegate {
			//set sound effects volume for sound manager and save
			SoundManager.getInstance().getEFXSource().volume = soundFXSlider.value;
		});

		exitBtn.onClick.AddListener(() => {
			Application.Quit();
		});
	}

	// Update is called once per frame
	void Update () {
		matchTimerText.text = matchTimerSlider.value.ToString();
	}

	void FixedUpdate() {
		if (eventSystem.currentSelectedGameObject != selectedField) {
			if (eventSystem.currentSelectedGameObject == null) {
				eventSystem.SetSelectedGameObject(selectedField);
			} else {
				selectedField = eventSystem.currentSelectedGameObject;
			}
		}
	}

}