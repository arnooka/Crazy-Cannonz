using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public static SoundManager instance;

	[SerializeField]
	public AudioSource EFXSource;
	[SerializeField]
	public AudioSource musicSource;
	[SerializeField]
	public float lowPitchVariation = .9f;
	[SerializeField]
	public float highPitchVariation = 1.1f;

	[SerializeField]
	public AudioClip[] musicClips;
	public bool inMenu;


	// Use this for initialization
	void Start () {
        musicSource.volume = .75f;
		if (instance == null) {
			instance = this;
		} else {
			Destroy (gameObject);
		}
		DontDestroyOnLoad (gameObject);
	}

    /// Takes an audio clip and a pitch and plays the clip
    /// using the EFX audio source
	public void playClip(AudioClip audioClip, float pitch) {
        EFXSource.pitch = pitch;
		EFXSource.clip = audioClip;

		EFXSource.Play ();
	}

    /// Takes an array of audio clips, picks a random clip, 
    /// and plays it at a pitch between a range
	public void randomPitchClip(AudioClip[] audioClips) {
		EFXSource.pitch = (float)Random.Range (lowPitchVariation, highPitchVariation);

		EFXSource.clip = audioClips[Random.Range(0, audioClips.Length)];

		EFXSource.Play ();
	}

    /// When we're not in a menu, pick a random in-game
    /// song and play it. When a song ends, start another
	public void Update() {
        if (!musicSource.isPlaying && !inMenu) {
			AudioClip clip = musicSource.clip;
			while (musicSource.clip == clip) {
				musicSource.clip = musicClips[Random.Range(0, musicClips.Length)];
			}
			musicSource.Play ();
		}
	}

}
