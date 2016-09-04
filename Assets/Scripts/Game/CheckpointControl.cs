using UnityEngine;
using System.Collections;

public class CheckpointControl : MonoBehaviour {

	public LevelManager lm;

	void Start () {
		lm = FindObjectOfType<LevelManager> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		GameObject go = other.gameObject;

		if (go.tag == "Player") {
			lm.checkpoint = gameObject;
		}
	}
}
