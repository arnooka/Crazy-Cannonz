using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	[SerializeField]
	private float speed;
	[SerializeField]
	private GameObject explosionEffect;

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
			explosionEffect = Instantiate(explosionEffect, transform.position, transform.rotation);
			adjustEffectScale();
			Destroy(this.gameObject);
		}
	}

	private void adjustEffectScale() {
		Vector2 scale = explosionEffect.transform.localScale;
		float time = 1;
		Debug.Log(explosionEffect.transform.name);
		if (explosionEffect.transform.name.Contains("BigExplosionEffect")) {
			scale.x = scale.x / 50;
			scale.y = scale.y / 50;
			time = 3;
		} else if (explosionEffect.transform.name.Contains("SmallExplosionEffect")) {
			scale.x = scale.x / 20;
			scale.y = scale.y / 20;
			time = 3;
		} else if (explosionEffect.transform.name.Contains("BulletImpactMetalEffect")) {
			scale.x = scale.x / 20;
			scale.y = scale.y / 20;
			time = 1;
		}
		explosionEffect.transform.localScale = scale;
		Destroy(explosionEffect, time);
	}

	public GameObject GetWhoFired() {
		return whoFired;
	}

	public void SetWhoFired(GameObject player) {
		whoFired = player;
	}

}
