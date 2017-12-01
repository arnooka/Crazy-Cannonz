using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Back2MenuButton : MonoBehaviour {

	[SerializeField]
	private Button back2MenuBtn;

	// Use this for initialization
	void Start () {

		back2MenuBtn.onClick.AddListener(() => {
			SceneManager.LoadScene("MainMenu");
			DestroyObject(this.gameObject);
		});

	}

}