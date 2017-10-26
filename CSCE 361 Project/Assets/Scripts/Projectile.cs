using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	[SerializeField]
	private float speed;

	private Rigidbody2D projectile;
	private GameObject whoFired;

	// Use this for initialization
	void Start () {
		projectile = GetComponent<Rigidbody2D>();
		float dir = 1;
		if (!whoFired.GetComponent<PlayerScript>().GetDirection()) {
			dir = -1;
		}
		Vector2 vel = new Vector2(dir * speed, 0);
		projectile.velocity = vel;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Physics2D.IgnoreCollision(whoFired.GetComponent<Collider2D>(), this.GetComponent<Collider2D>());
	}

	void OnCollisionEnter2D(Collision2D col) {
		string objectTag = col.gameObject.tag;
		if (objectTag.Contains("Crate") || col.gameObject == whoFired) {
			Physics2D.IgnoreCollision(col.collider, this.GetComponent<Collider2D>());
		} else {
			// instantiate particle effect
			Destroy(this.gameObject);
		}
	}

	public GameObject GetWhoFired() {
		return whoFired;
	}

	public void SetWhoFired(GameObject player) {
		whoFired = player;
	}
}
