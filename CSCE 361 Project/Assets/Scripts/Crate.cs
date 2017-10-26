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
		if (col.gameObject.tag.Contains("Player")){
			playerScript = col.gameObject.GetComponent<PlayerScript>();

			if (!playerScript.GetProjectileBool()) {
				if (this.gameObject.tag == "Crate_0") {
					GameObject projectile = Resources.Load ("Large Cannon Ball") as GameObject;
					projectile.GetComponent<Projectile>().SetWhoFired(col.gameObject);
					playerScript.SetProjectile (projectile);

				} else if (this.gameObject.tag == "Crate_1") {
					GameObject projectile = Resources.Load ("Mid Cannon Ball") as GameObject;
					projectile.GetComponent<Projectile>().SetWhoFired(col.gameObject);
					playerScript.SetProjectile (projectile);

				} else if (this.gameObject.tag == "Crate_2") {
					GameObject projectile = Resources.Load ("Small Cannon Ball") as GameObject;
					projectile.GetComponent<Projectile>().SetWhoFired(col.gameObject);
					playerScript.SetProjectile(projectile);
				}
				Destroy(this.gameObject);
			}
		}
	}
}