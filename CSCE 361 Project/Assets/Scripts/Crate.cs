using UnityEngine;
using System.Collections;

public class Crate : MonoBehaviour {

	public float amplitude = 0.5f;
	public float frequency = 1f;

	Vector3 posOffset = new Vector3 ();
	Vector3 tempPos = new Vector3 ();

	private PlayerScript playerScript;

	void Start () {
		posOffset = transform.position;
	}

	void Update () {

		tempPos = posOffset;
		tempPos.y += Mathf.Sin (Time.fixedTime * Mathf.PI * frequency) * amplitude;

		transform.position = tempPos;
	}

	void OnCollisionEnter2D (Collision2D col) {
		if (col.gameObject.tag == "Player"){
			playerScript = col.gameObject.GetComponent<PlayerScript>();
			if (!playerScript.HasProjectile) {
				Debug.Log ("hoozah!");
				if (this.gameObject.tag == "Crate_0") {
					Object pPrefab = Resources.Load ("Assets/Projectiles/Large Cannon Ball");
					playerScript.projectile = (GameObject)pPrefab;
				} else if (this.gameObject.tag == "Crate_1") {
					Object pPrefab = Resources.Load ("Assets/Projectiles/Mid Cannon Ball");
					playerScript.projectile = (GameObject)pPrefab;
				} else if (this.gameObject.tag == "Crate_2") {
					Object pPrefab = Resources.Load ("Assets/Projectiles/Small Cannon Ball");
					playerScript.projectile = (GameObject)pPrefab;
				}
				playerScript.HasProjectile = true;
				Destroy (this.gameObject);
			}
		}
	}
}
