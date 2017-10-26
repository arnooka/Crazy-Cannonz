using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapToggle : MonoBehaviour {

	public ToggleGroup toggles;
	public Toggle toggle1, toggle2, toggle3;
	public Image mapImage;
	public Sprite mapS, mapM, mapL;

	// Use this for initialization
	void Start () {

		toggle1.onValueChanged.AddListener ((value) => {
			if(value) mapImage.sprite = mapS;
		});


		toggle2.onValueChanged.AddListener ((value) => {
			if(value) mapImage.sprite = mapM;
		});


		toggle3.onValueChanged.AddListener ((value) => {
			if(value) mapImage.sprite = mapL;
		});

		
	}
	
	// Update is called once per frame
	void Update () {
			
	}

}