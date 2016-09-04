using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour
{
	public ScenesManager sm;
	private string sceneName;
	public Slider slider;
	public Text loading;
	public Text startFront;
	public Text startBG;


	void Start()
	{
		sm = FindObjectOfType<ScenesManager> ();
		sceneName = "World" + sm.GetWorld () + "Lvl" + sm.GetLvl ();
		StartCoroutine (AsynchronousLoad (sceneName));
	}

	IEnumerator AsynchronousLoad (string scene)
	{
		yield return null;

		AsyncOperation ao = SceneManager.LoadSceneAsync(scene);
		ao.allowSceneActivation = false;

		while (! ao.isDone)
		{
			// [0, 0.9] > [0, 1]
			// Calculate Progress
			float progress = Mathf.Clamp01(ao.progress / 0.9f);
			loading.text = ((int)progress * 100) + "%";

			// Update Bar
			slider.value = progress;

			// Loading completed
			if (ao.progress == 0.9f)
			{
				startFront.text = "Press to Start";
				startBG.text = "Press to Start";
				if (Input.anyKey)
					ao.allowSceneActivation = true;
			}

			yield return null;
		}
	}
}
