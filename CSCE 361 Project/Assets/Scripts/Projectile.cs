﻿using System.Collections;
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
		
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject == whoFired || col.gameObject.tag.Contains("Crate")) {
			Physics2D.IgnoreCollision(col.collider, gameObject.GetComponent<Collider2D>());
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

		if (explosionEffect.transform.name.Contains("BigExplosion")) {
			scale.x /= 100f;
			scale.y /= 100f;
			time = 1;
		} else if (explosionEffect.transform.name.Contains("SmallExplosion")) {
			scale.x /= 10f;
			scale.y /= 10f;
			time = 1;
		} else if (explosionEffect.transform.name.Contains("BulletImpactMetal")) {
			scale.x /= 20f;
			scale.y /= 20f;
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
