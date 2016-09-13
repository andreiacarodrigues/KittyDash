using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using System.Collections.Generic;

public class DataStorage : MonoBehaviour {

	static GameObject go = null;
	public List<List<float>> score = new List<List<float>>();
	public List<List<int>> stars = new List<List<int>> ();
	public int coins;
	public int revives;
	public int shields;
	public bool backgroundMusic;
	public float volume;
	public bool touchControls;

	void Start () {
		if (go == null)
			go = gameObject;
		else
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject);

		if (!Load ())
			initInfo ();
	}

	private void initInfo()
	{
		coins = 0;
		revives = 0;
		shields = 0;
		backgroundMusic = true;
		volume = 1;
		touchControls = false;

		for (int i = 0; i < 5; i++)
		{
			List<float> line = new List<float> ();

			for (int j = 0; j < 5; j++) 
				line.Add (0);

			score.Add (line);
		}

		for (int i = 0; i < 5; i++)
		{
			List<int> line = new List<int> ();

			for (int j = 0; j < 5; j++) 
				line.Add (0);

			stars.Add (line);
		}
	}

	public void Save()
	{
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Open (Application.persistentDataPath + "/gameInfo.dat", FileMode.OpenOrCreate);

		GameData gd = new GameData (score, stars, coins, revives, shields, backgroundMusic, volume, touchControls);

		bf.Serialize (file, gd);
		file.Close ();

	}

	public bool Load()
	{
		if (File.Exists (Application.persistentDataPath + "/gameInfo.dat"))
		{
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/gameInfo.dat", FileMode.Open);

			GameData gd = (GameData)bf.Deserialize (file);
			file.Close ();

			score = gd.score;
			stars = gd.stars;
			coins = gd.coins;
			revives = gd.revives;
			shields = gd.shields;
			backgroundMusic = gd.backgroundMusic;
			volume = gd.volume;
			touchControls = gd.touchControls;

			return true;
		}

		return false;
	}
}

[Serializable]
class GameData
{
	public List<List<float>> score = new List<List<float>> ();
	public List<List<int>> stars = new List<List<int>>();
	public int coins;
	public int revives;
	public int shields;
	public bool backgroundMusic;
	public float volume;
	public bool touchControls;

	public GameData(List<List<float>> s, List<List<int>> t, int c, int r, int sd, bool bg, float v, bool tc)
	{
		score = s;
		stars = t;
		coins = c;
		revives = r;
		shields = sd;
		backgroundMusic = bg;
		volume = v;
		touchControls = tc;
	}
}
