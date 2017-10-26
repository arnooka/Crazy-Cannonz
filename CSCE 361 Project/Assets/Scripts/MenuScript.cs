using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

	public Slider matchTimerSlider;
	public Text matchTimerText;
	public Button optionsBtn, matchBtn, matchBackBtn, optionsBackBtn, matchStartBtn;
	public GameObject mainMenu, optionsMenu, matchMenu;

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
			SceneManager.LoadScene ("Arena Mid");
		});


	}

	// Update is called once per frame
	void Update () {

		string x = matchTimerSlider.value.ToString();
		matchTimerText.text = x;

	}
}
