using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {

	public Slider mainSlider;
	public Text text;

	// Use this for initialization
	void Start () {


		
	}
	
	// Update is called once per frame
	void Update () {
				
		string x = mainSlider.value.ToString();
		text.text = x;

	}
}
