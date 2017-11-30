using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapToggle : MonoBehaviour {
	[SerializeField]
	private ToggleGroup toggles;
	[SerializeField]
	private Toggle toggle1, toggle2, toggle3, toggle4;
	[SerializeField]
	private Image mapImage;
	[SerializeField]
	private Sprite mapS, mapM, mapL;

	private static string mapName;

	// Use this for initialization
	void Start () {

		toggle1.onValueChanged.AddListener ((value) => {
			if (value) {
				mapImage.sprite = mapS;
				mapName = mapS.name;
			}
		});

		toggle2.onValueChanged.AddListener ((value) => {
			if (value) {
				mapImage.sprite = mapM;
				mapName = mapM.name;
			}
		});

		toggle3.onValueChanged.AddListener ((value) => {
			if (value) {
				mapImage.sprite = mapL;
				mapName = mapL.name;
			}
		});

		toggle4.onValueChanged.AddListener ((value) => {
			if (value) {
				int temp = Random.Range(1, 3);
				if (temp == 1) {
					mapName = mapS.name;
				} else if (temp == 2) {
					mapName = mapM.name;
				} else {
					mapName = mapL.name;
				}
			}
		});
	}

	public static string GetMapName() {
		return mapName;
	}
}