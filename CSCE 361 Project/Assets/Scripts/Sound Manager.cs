using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public static SoundManager instance = null;

	public AudioSource EFXSource;
	public AudioSource musicSource;
	public float lowPitchVariation;
	public float highPitchVariation;


	// Use this for initialization
	void Start () {
		
	}

	public void playClip(AudioClip clip) {
		EFXSource.clip = clip;

		EFXSource.Play ();
	}

	public void randomPitchClip(AudioClip clip) {

	}
}
