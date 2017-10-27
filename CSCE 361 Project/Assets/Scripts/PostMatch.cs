using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PostMatch : MonoBehaviour {

	public Button returnBtn;

	// Use this for initialization
	void Start () {

		returnBtn.onClick.AddListener (() => {
			print("return");
			SceneManager.LoadScene ("MainMenu");
		});

	}
	
	// Update is called once per frame
	void Update () {


	}
}
