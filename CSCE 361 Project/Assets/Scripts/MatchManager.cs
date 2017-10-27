using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchManager : MonoBehaviour {

	public AudioSource theMatchMusic;
	public GameObject panel;
	public Text text;
	public static double matchTime = 600.0, soundFXVolume = 1.0;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		matchTime -= Time.deltaTime;
		text.text = matchTime.ToString();

		//First check to see if the player wants to pause.
		if (Input.GetKey (KeyCode.P)) {
			panel.SetActive (false);
			print ("p button pressed");
		}
		
	}
}
