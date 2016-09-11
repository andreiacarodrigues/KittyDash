using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	private ScenesManager sm;
	private DataStorage ds;

	public GameObject playerPrefab;
	public Player_Control player;
	private SoundManager sound;
	public GameObject checkpoint;
	public int lvl;

	// Lvl Boundaries
	public float leftBoundary;
	public float rightBoundary;

	// Keys and Coins
	[HideInInspector] public int keys;
	[HideInInspector] public int coins;
	[HideInInspector] public Text keyCounter;
	[HideInInspector] public Text coinCounter;
	public int totalKeys;
	public int totalCoins;

	// For GameOver menu - 1º unfilled stars (left to right) and 2º filled stars
	public GameObject[] starsGameOver; 
	public GameObject[] starsVictory; 
	private int nrStars;

	// Control of the GUI
	public GameObject game;
	public GameObject gameControls;
	public GameObject pauseMenu;
	public GameObject gameOverMenu;
	public GameObject victoryMenu;

	public Text scoreGameOver;
	public Text scoreGameOverBG;
	public Text bestScoreGameOver;
	public Text bestScoreGameOverBG;
	public Text scoreVictory;
	public Text scoreVictoryBG;
	public Text bestScoreVictory;
	public Text bestScoreVictoryBG;

	public Text revives;
	public Button watchAd;
	public Button revive;

	void Start () {
		player = FindObjectOfType<Player_Control> ();
		sm = FindObjectOfType<ScenesManager> ();
		ds = FindObjectOfType<DataStorage> ();
		sound = FindObjectOfType<SoundManager> ();
		keys = 0;
		coins = 0;
		if (ds.touchControls)
			gameControls.SetActive (false);
		else
			gameControls.SetActive (true);
	}

	void Update () {
	/*	if (player.transform.position.x < leftBoundary)
			player.transform.position = new Vector3 (leftBoundary, player.transform.position.y, player.transform.position.z);

		if (player.transform.position.x > rightBoundary)
			player.transform.position = new Vector3 (rightBoundary, player.transform.position.y, player.transform.position.z);
	*/
		keyCounter.text = keys + "/" + totalKeys;
		coinCounter.text = coins + "/" + totalCoins;

		if (player.gameOver) 
			GameOver ();

		if (gameOverMenu.activeSelf)
			UpdateRevives ();

		if (ds.revives == 0)
			Debug.Log ("Has revives");
		else
			Debug.Log ("Doesn't have revive");

		if (UnityAds.AdAvailable ())
			Debug.Log ("Ads Available");
		else
			Debug.Log ("Ads Unavailable");

		if (ds.revives == 0 && UnityAds.AdAvailable ())
			watchAd.interactable = true;
		else
			watchAd.interactable = false;

		if (ds.revives > 0)
			revive.interactable = true;
		else
			revive.interactable = false;

		if (player.victory)
			Victory ();
	}


	public void Pause () 
	{
		sound.play = Sound.BUTTON;
		Time.timeScale = 0;
		game.SetActive (false);
		pauseMenu.SetActive (true);
	}

	public void BackToGame()
	{
		sound.play = Sound.BUTTON;
		Time.timeScale = 1;
		game.SetActive (true);
		pauseMenu.SetActive (false);
	}

	public void GameOver () 
	{
		Time.timeScale = 0;
		game.SetActive (false);
		gameOverMenu.SetActive (true);

		CalculateScore ();

		while (nrStars > 0) {
			starsGameOver [nrStars - 1].SetActive (false);
			starsGameOver [nrStars + 2].SetActive (true);
			nrStars--;
		}

		scoreGameOver.text = coins * (1 + 1.5f * keys) +"";
		scoreGameOverBG.text = scoreGameOver.text;
		bestScoreGameOver.text = ds.score [sm.GetWorld ()-1] [lvl-1] + "";
		bestScoreGameOverBG.text = bestScoreGameOver.text;
			
		player.gameOver = false;
	}

	public void UpdateRevives()
	{
		revives.text = "(" + ds.revives + ")";
	}

	public void Victory () 
	{
		Time.timeScale = 0;
		game.SetActive (false);
		victoryMenu.SetActive (true);

		CalculateScore ();

		while (nrStars > 0) {
			starsVictory [nrStars - 1].SetActive (false);
			starsVictory [nrStars + 2].SetActive (true);
			nrStars--;
		}

		scoreVictory.text = coins * (1 + 1.5f * keys) + "";
		scoreVictoryBG.text = scoreVictory.text;
		bestScoreVictory.text = ds.score [sm.GetWorld ()-1] [lvl-1] + "";
		bestScoreVictoryBG.text = bestScoreVictory.text;

		ds.coins += coins;
		ds.Save ();

		player.victory = false;
	}

	private void CalculateScore()
	{
		float totalScore = totalCoins * (1 + 1.5f * totalKeys);
		float levelScore = coins * (1 + 1.5f * keys);
		float levelRatio = levelScore / totalScore * 100;

		if (levelRatio >= 10) 
		{
			if (levelRatio >= 50) 
			{
				if (levelRatio >= 90)
					nrStars = 3;
				else
					nrStars = 2;
			} else
				nrStars = 1;
		} else
			nrStars = 0;

		float previousScore = ds.score [sm.GetWorld ()-1] [lvl-1];

		if (levelScore > previousScore) {
			ds.score [sm.GetWorld ()-1] [lvl-1] = levelScore;
			ds.stars [sm.GetWorld () - 1] [lvl - 1] = nrStars;
			ds.Save ();
		}
	}


	public void ReloadLevel()
	{
		sound.play = Sound.BUTTON;
		player.fullStop = true;
		Time.timeScale = 1;
		StartCoroutine ("LoadNewLevel", SceneManager.GetActiveScene ().name);
	}

	public void BackToLevelSelection() // level ou world
	{
		sound.play = Sound.BUTTON;
		player.fullStop = true;
		Time.timeScale = 1;
		StartCoroutine ("LoadNewLevel", "WorldsMenu");
	}

	private IEnumerator LoadNewLevel(string name)
	{
		yield return new WaitForSeconds (0.1f);
		player.fullStop = false;
		SceneManager.LoadScene (name);
	}

	public void Revive()
	{
		sound.play = Sound.BUTTON;
		Time.timeScale = 1;
		player.transform.position = checkpoint.transform.position;

		player.Reset();
		game.SetActive (true);
		gameOverMenu.SetActive (false);
		ds.revives--;
		ds.Save ();
	}

	public void ShowRewardedAd()
	{
		UnityAds.ShowRewardedAd ("revive");
	}
}
