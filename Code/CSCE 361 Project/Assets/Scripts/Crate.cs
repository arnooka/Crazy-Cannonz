using UnityEngine;
using System.Collections;

public class Crate : MonoBehaviour {

	[SerializeField]
	private float amplitude = 0.5f;
	[SerializeField]
	private float frequency = 1f;

	[SerializeField]
	private GameObject projectile;

	private Vector2 posOffset = new Vector2 ();
	private Vector2 tempPos = new Vector2 ();

	private PlayerScript playerScript;
	private SpawnLocation location;

	void Start () {
		posOffset = transform.position;
	}

	void Update () {

		tempPos = posOffset;
		tempPos.y += Mathf.Sin (Time.fixedTime * Mathf.PI * frequency) * amplitude;

		transform.position = tempPos;
	}
	
	private void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag.Contains("Player")) {
			playerScript = col.gameObject.GetComponent<PlayerScript>();

			if (!playerScript.GetProjectileBool()) {
				playerScript.SetProjectile(projectile);
				location.SetBool(false);
				Destroy(this.gameObject);
			}
		}
	}

	public void SetLocation(SpawnLocation position) {
		location = position;
	}
}