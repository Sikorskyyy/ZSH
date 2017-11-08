using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public static AudioManager Instance { get; private set;}

	[SerializeField]
	private AudioSource audioSounds;

	[SerializeField]
	private AudioSource audioBackground;


	void Awake()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
			return;
		}
		Instance = this;
		DontDestroyOnLoad(gameObject);
	}


	public static bool soundMute
	{
		get { return AudioListener.pause; } 
		set{ AudioListener.pause = value; }
	}

		
	public void PlaySoundOneShot(AudioClip clip)
	{
		audioSounds.PlayOneShot (clip);
	}


	public void PlaySound( AudioClip clip)
	{
		audioSounds.clip = clip;
		audioSounds.Play();
	}

    public void PlaySound()
    {
        audioSounds.Play();
    }

	public void PlayMusic()
	{
		audioBackground.Play();
	}

}
