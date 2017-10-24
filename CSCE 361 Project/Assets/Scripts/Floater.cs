using UnityEngine;
using System.Collections;

public class Floater : MonoBehaviour {

	public float amplitude = 0.5f;
	public float frequency = 1f;

	Vector3 posOffset = new Vector3 ();
	Vector3 tempPos = new Vector3 ();

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
			if (!col.gameObject.GetComponent<PlayerScript>().HasProjectile) {
				if (this.gameObject.tag == "Crate_0") {

				} else if (this.gameObject.tag == "Crate_1") {

				} else if (this.gameObject.tag == "Crate_2") {

				}
				col.gameObject.GetComponent<PlayerScript>().HasProjectile = true;
				Destroy (this.gameObject);
			}
		}
	}
}
