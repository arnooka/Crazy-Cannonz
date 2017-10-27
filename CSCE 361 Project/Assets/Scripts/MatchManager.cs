using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchManager : MonoBehaviour {

	public AudioSource theMatchMusic;

	// Use this for initialization
	void Start () {

		theMatchMusic.volume = (float) MatchSettings.musicVolume;
		
	}
	
	// Update is called once per frame
	void Update () {
<<<<<<< HEAD

		matchTime -= Time.deltaTime;
		//timer.text = matchTime.ToString();
		print(matchTime);

		//First check to see if the player wants to pause.
		if (Input.GetKey (KeyCode.P)) {
			panel.SetActive (false);
			print ("p button pressed");
		}
=======
>>>>>>> parent of e8cfc38... Merge branch 'master' of https://github.com/nookavish/CSCE-361
		
	}
}
