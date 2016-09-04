using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class WorldButtons : MonoBehaviour {

	private ScenesManager sm;
	public int world;
	SpriteRenderer spr;

	public ButtonControl bc;

	void Start () {
		sm = FindObjectOfType<ScenesManager> ();
		spr = GetComponent<SpriteRenderer> ();
	}

	public void GoToLvlMenu()
	{
		sm.SetWorld (world);
		bc.LoadLevelMenu ();
	}
}
