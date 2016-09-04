using UnityEngine;
using System.Collections;

public class CollectiblesController : MonoBehaviour {

	public LevelManager lm;
	private SoundManager sound;

	void Start()
	{
		sound = FindObjectOfType<SoundManager> ();
		lm = FindObjectOfType<LevelManager> ();

	}
	void OnTriggerEnter2D(Collider2D other)
	{
		GameObject go = other.gameObject;

		if (go.tag == "Player") {

			if (gameObject.tag == "Key") {
				sound.play = Sound.KEY;
				lm.keys++;
			}

			if (gameObject.tag == "Coin") {
				sound.play = Sound.COIN;
				lm.coins++;
			}

			gameObject.SetActive (false);
		}
	}
}
