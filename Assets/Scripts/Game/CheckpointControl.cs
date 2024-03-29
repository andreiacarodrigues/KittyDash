﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

		if (go.tag == "Player")
		{
			Debug.Log ("Player Pos: " + go.transform.position.x);
			lm.checkpoint = gameObject;

			// Save player position
			go.GetComponent<Player_Control> ().SavePosition ();

			// Save enemies position
			EnemiesController[] enemies = FindObjectsOfType<EnemiesController> ();
			foreach(EnemiesController e in enemies)
			{
				Debug.Log ("Saving Enemy");
				e.SavePosition ();
			}
		}
	}
}
