using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLocation: MonoBehaviour {

	private bool hasCrate;

	// Use this for initialization
	void Start () {
		hasCrate = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetBool(bool condition) {
		hasCrate = condition;
	}

	public bool GetBool() {
		return hasCrate;
	}

}
