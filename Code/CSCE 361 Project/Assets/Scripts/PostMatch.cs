using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PostMatch : MonoBehaviour {

	public Button mainMenuBtn, rematchBtn;
	public AudioClip menuMusic;
	private SoundManager soundManager = SoundManager.instance;

	// Use this for initialization
	void Start () {

		mainMenuBtn.onClick.AddListener (() => {
			print("return");
			soundManager.musicSource.Stop();
			soundManager.musicSource.loop = true;
			soundManager.musicSource.clip = menuMusic;
			soundManager.musicSource.Play();
			SceneManager.LoadScene ("MainMenu");
		});

		rematchBtn.onClick.AddListener (() => {
			SceneManager.LoadScene ("Arena Mid");
		});

	}
	
	// Update is called once per frame
	void Update () {


	}
}
