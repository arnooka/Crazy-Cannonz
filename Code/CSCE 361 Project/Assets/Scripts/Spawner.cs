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
	private int maxTime = 5;
	[SerializeField]
	private int minTime = 2;

	void Start() {
		int time = Random.Range(minTime, maxTime);
		Invoke("SpawnCrate", time);
	}
		
	void SpawnCrate() {
		CancelInvoke ();

		// Get random crate spawn location
		currentLocation = Random.Range(0, spawnLocations.Length);
		while (lastLocation == currentLocation) {
			currentLocation = Random.Range(0, spawnLocations.Length);
		}
		lastLocation = currentLocation;
		SpawnLocation location = spawnLocations[currentLocation].GetComponent<SpawnLocation>();

		// Spawn crate if no crate in current location
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
