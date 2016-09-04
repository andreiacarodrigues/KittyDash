using UnityEngine;
using System.Collections;



public class Sound_Manager : MonoBehaviour {

	[HideInInspector] public Sound play;

	public AudioClip jump;
	public AudioClip dead;
	public AudioClip coin;
	public AudioClip key;
	public AudioClip button;
	public AudioClip gameOver;

	public AudioSource audioSource;

	void Start () {
		play = Sound.NOTHING;
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
