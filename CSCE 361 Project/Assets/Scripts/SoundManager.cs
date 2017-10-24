using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public static SoundManager instance = null;
	public AudioSource EFXSource;
	public AudioSource musicSource;
	public float lowPitchVariation = .9f;
	public float highPitchVariation = 1.1f;
	public AudioClip[] gamePlayClips;


	// Use this for initialization
	void Start () {
		if (instance == null) {
			instance = this;
		} else {
			Destroy (gameObject);
		}
		DontDestroyOnLoad (gameObject);
	}

	public void playClip(AudioClip audioClip) {
		EFXSource.clip = audioClip;

		EFXSource.Play ();
	}

	public void randomPitchClip(AudioClip[] audioClips) {
		EFXSource.pitch = (float)Random.Range (lowPitchVariation, highPitchVariation);

		EFXSource.clip = audioClips[Random.Range(0, audioClips.Length)];

		EFXSource.Play ();
	}
}
