using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	[SerializeField]
	private Transform[] spawnLocations;
	[SerializeField]
	private GameObject[] whatToSpawn;

	private GameObject crate;
	private int lastLocation = -1;
	private int currentLocation;
	private int crateType = 0;

	[SerializeField]
	private int maxTime;
	[SerializeField]
	private int minTime;


	void Start() {
		Invoke("SpawnCrate", 3);
	}
		
	void SpawnCrate() {
		CancelInvoke ();

		currentLocation = Random.Range(0, spawnLocations.Length);
		while (lastLocation == currentLocation) {
			currentLocation = Random.Range(0, spawnLocations.Length);
		}
		lastLocation = currentLocation;

		SpawnLocation location = spawnLocations[currentLocation].GetComponent<SpawnLocation>();

		if (!location.GetBool()) {
			
			if (crateType >= whatToSpawn.Length) {
				crateType = 0;
			}
			crate = Instantiate(whatToSpawn[crateType], spawnLocations[currentLocation].transform.position, Quaternion.Euler(0, 0, 0));
			crate.GetComponent<Crate>().SetLocation(location);
			location.SetBool(true);
			crateType++;
		}
		Invoke ("SpawnCrate", Random.Range (minTime, maxTime));
	}

}
