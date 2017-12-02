using UnityEngine;
using System.Collections;

public class PlatformFloat : MonoBehaviour {

	[SerializeField]
	private float amplitude = 1.2f;
	[SerializeField]
	private float frequency = 0.3f;

	private Vector2 posOffset = new Vector2 ();
	private Vector2 tempPos = new Vector2 ();

	void Start () {
		posOffset = transform.position;
	}

	void Update () {

		tempPos = posOffset;
		tempPos.y += Mathf.Sin (Time.fixedTime * Mathf.PI * frequency) * amplitude;

		transform.position = tempPos;
	}

}
