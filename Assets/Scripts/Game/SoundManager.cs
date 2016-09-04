using UnityEngine;
using System.Collections;

public enum Sound
{
	NOTHING,
	JUMP,
	SLIDE,
	DEAD,
	COIN,
	KEY,
	VICTORY,
	BUTTON
}

public class SoundManager : MonoBehaviour {

	static GameObject go = null;

	public bool backgroundMusic;

	void Start () {
		if (go == null)
			go = gameObject;
		else
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject);

		backgroundMusic = true;

		LoadResources ();
	}
		
	[HideInInspector] public Sound play;

	private AudioClip jump;
	private AudioClip dead;
	private AudioClip coin;
	private AudioClip key;
	private AudioClip button;
	private AudioClip gameOver;

	public AudioSource audioSource;
	public float volume;

	void LoadResources () {
		play = Sound.NOTHING;
		jump = Resources.Load<AudioClip> ("Audio/jump");
		dead = Resources.Load<AudioClip> ("Audio/dead");
		coin = Resources.Load<AudioClip> ("Audio/coin");
		key = Resources.Load<AudioClip> ("Audio/key");
		button = Resources.Load<AudioClip> ("Audio/bip");
		gameOver = Resources.Load<AudioClip> ("Audio/end");
		audioSource = FindObjectOfType<AudioSource> ();
	}

	void Update () {
		if (play != Sound.NOTHING)
			PlaySound();
	}
		

	public void PlaySound()
	{
		switch (play) {
		case Sound.JUMP: 
			audioSource.PlayOneShot (jump);
			play = Sound.NOTHING;
			break;

		case Sound.COIN: 
			audioSource.PlayOneShot (coin);
			play = Sound.NOTHING;
			break;

		case Sound.KEY: 
			audioSource.PlayOneShot (key);
			play = Sound.NOTHING;
			break;

		case Sound.DEAD: 
			audioSource.PlayOneShot (dead);
			play = Sound.NOTHING;
			break;

		case Sound.BUTTON: 
			audioSource.PlayOneShot (button);
			play = Sound.NOTHING;
			break;

		case Sound.VICTORY: 
			audioSource.PlayOneShot (gameOver);
			play = Sound.NOTHING;
			break;
		}
	}
}
