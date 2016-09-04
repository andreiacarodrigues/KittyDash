using UnityEngine;
using System.Collections;

public class AudioSourceScript : MonoBehaviour {

	static GameObject go = null;

	void Start () {
		if (go == null)
			go = gameObject;
		else
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject);
	}

}
