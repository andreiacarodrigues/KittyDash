using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonControl : MonoBehaviour {

	private SoundManager sm;
	private Fading f;

	void Start()
	{
		sm = FindObjectOfType<SoundManager> ();
		f = FindObjectOfType<Fading> ();
	}

	public void LoadPlayMenu(){
		sm.play = Sound.BUTTON;
		StartCoroutine("LoadNewLevel", "WorldsMenu");
	}

	public void LoadMainMenu(){
		sm.play = Sound.BUTTON;
		StartCoroutine("LoadNewLevel", "MainMenu");
	}

	public void LoadLevelMenu(){
		sm.play = Sound.BUTTON;
		StartCoroutine("LoadNewLevel", "LvlSelectMenu");
	}

	public void LoadInfoMenu()
	{
		sm.play = Sound.BUTTON;
		StartCoroutine("LoadNewLevel", "Info");
	}

	public void LoadAchievementsMenu()
	{
		sm.play = Sound.BUTTON;
		StartCoroutine("LoadNewLevel", "Achievements");
	}
		
	public void LoadShopMenu()
	{
		sm.play = Sound.BUTTON;
		StartCoroutine("LoadNewLevel", "Shop");
	}

	public void LoadSettingsMenu()
	{
		sm.play = Sound.BUTTON;
		StartCoroutine("LoadNewLevel", "Settings");
	}

	public void LoadDictionary()
	{
		sm.play = Sound.BUTTON;
		StartCoroutine("LoadNewLevel", "Dictionary");
	}

	public void LoadAboutTheGame()
	{
		sm.play = Sound.BUTTON;
		//StartCoroutine("LoadNewLevel", "Dictionary");
	}

	public void LoadCredits()
	{
		sm.play = Sound.BUTTON;
		//StartCoroutine("LoadNewLevel", "Dictionary");
	}

	public void LoadLevel(string name)
	{
		sm.play = Sound.BUTTON;
		StartCoroutine("LoadNewLevel", name);
	}

	private IEnumerator LoadNewLevel(string name)
	{
		//yield return new WaitForSeconds (0.1f);

		float fadeTime = f.BeginFade (1);

		yield return new WaitForSeconds (fadeTime);

		f.ResetBeginFade ();

		SceneManager.LoadScene (name);
	}
}
