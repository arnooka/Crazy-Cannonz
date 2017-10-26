using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour 
{
	public Transform[] spawnLocations;
	public GameObject[] whatToSpawnPrefab;
	public GameObject[] whatToSpawnClone;

	private int currentLocation;
	private int crateType;

	public int maxTime = 8;
	public int minTime = 2;
	public int spawnTime;

	private GameObject crate;

	void Start() {
		Invoke("SpawnCrate", 3);
	}
		
	void SpawnCrate() {
		CancelInvoke ();
		currentLocation = Random.Range (0, spawnLocations.Length);
		crateType = Random.Range(0, whatToSpawnPrefab.Length);
		whatToSpawnClone [currentLocation] = Instantiate (whatToSpawnPrefab [crateType], spawnLocations [currentLocation].transform.position, Quaternion.Euler (0, 0, 0));
		Invoke ("SpawnCrate", Random.Range (minTime, maxTime));
	}
		

}
