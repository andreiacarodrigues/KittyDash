using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ScoresHandler : MonoBehaviour {

	public ButtonControl bc;
	public GameObject[] lvlSelection; 

	private DataStorage ds;
	private ScenesManager sm;

	private int world;
	private List<int> starsScore;

	void Start () {
		ds = FindObjectOfType<DataStorage> ();
		sm = FindObjectOfType<ScenesManager> ();
		PlaceButtons ();
	}

	void PlaceButtons()
	{
		world = sm.GetWorld ();
		starsScore = ds.stars[world-1];

		for (int i = 0; i < starsScore.Count; i++)
		{
			if (i != 0) // Se não for o 1º
			{ 
				if (starsScore [i - 1] < 2) // Se já tiver chegado ao final dos que nao estão bloqueados
					return;
				else 
					lvlSelection [i-1].SetActive (false);
			}

			int nr = starsScore[i];

			while (nr > 0) {
				GameObject.Find ("Lvl" + (i+1) + "/UnfilledStar" + nr).SetActive (false);
				nr--;
			}
		}
	}

	public void LoadLevel(int n)
	{
		sm.SetLvl (n);
		bc.LoadLevel("LoadingMenu");
	}
}
