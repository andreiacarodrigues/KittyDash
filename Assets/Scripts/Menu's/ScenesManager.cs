using UnityEngine;
using System.Collections;

public class ScenesManager : MonoBehaviour {

	static GameObject go = null;

	static int world;
	static int lvl;

	void Start () {
		if (go == null)
			go = gameObject;
		else
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject);

		world = 0;
	}
		
	public void SetWorld(int nr)
	{
		world = nr;
	}

	public void SetLvl(int nr)
	{
		lvl = nr;
	}

	public int GetWorld()
	{
		return world;
	}

	public int GetLvl()
	{
		return lvl;
	}
}
