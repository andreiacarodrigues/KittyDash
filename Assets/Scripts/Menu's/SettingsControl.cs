using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SettingsControl : MonoBehaviour {

	private DataStorage dt;
	private SoundManager sm;
	public Toggle backgroundMusic;
	public Slider volume;
	public Button touchControls;
	public Button buttonControls;

	void Start () {
		dt = FindObjectOfType<DataStorage> ();
		sm = FindObjectOfType<SoundManager> ();
		backgroundMusic.isOn = dt.backgroundMusic;
		volume.value = dt.volume;

		if (dt.touchControls) {
			touchControls.interactable = false;
			buttonControls.interactable = true;
		} 
		else 
		{
			touchControls.interactable = true;
			buttonControls.interactable = false;
		}
	}

	public void Controls()
	{
		sm.play = Sound.BUTTON;
		touchControls.interactable = !touchControls.interactable;
		buttonControls.interactable = !buttonControls.interactable;

		if (touchControls.IsInteractable()) {
			dt.touchControls = false;
			buttonControls.interactable = false;
		}
		else 
		{
			dt.touchControls = true;
			buttonControls.interactable = true;
		}

		dt.Save ();
	}
		
	public void BGMusic()
	{
		sm.play = Sound.BUTTON;
		dt.backgroundMusic = backgroundMusic.isOn;

		if (dt.backgroundMusic)
			sm.backgroundMusic = true;
		else
			sm.backgroundMusic = false;
		
		dt.Save ();
	}

	public void Volume()
	{
		dt.volume = volume.value;
		sm.audioSource.volume = dt.volume;
		dt.Save ();
	}
}
