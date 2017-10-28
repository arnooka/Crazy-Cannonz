using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PostMatch : MonoBehaviour {

	public Button mainMenuBtn, rematchBtn;
	public AudioClip menuMusic;
	private SoundManager soundManager = SoundManager.instance;
	[SerializeField]
	private Text Score1, Score2, Score3, Score4;

	// Use this for initialization
	void Start () {

		mainMenuBtn.onClick.AddListener (() => {
			print("return");
			soundManager.musicSource.Stop();
			soundManager.musicSource.loop = true;
			soundManager.inMenu = true;
			soundManager.musicSource.clip = menuMusic;
			soundManager.musicSource.Play();
			SceneManager.LoadScene ("MainMenu");
		});

		Score1.text = MatchManager.score1.ToString();
		Score2.text = MatchManager.score2.ToString();
		Score3.text = MatchManager.score3.ToString();
		Score4.text = MatchManager.score4.ToString();

		rematchBtn.onClick.AddListener (() => {
			SceneManager.LoadScene ("Arena Mid");
		});

	}
	
	// Update is called once per frame
	void Update () {


	}
}
