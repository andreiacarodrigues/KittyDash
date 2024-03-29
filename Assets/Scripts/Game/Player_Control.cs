using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player_Control : MonoBehaviour
{
	public GameObject trail;
	private SoundManager sound;
	private DataStorage ds;
	private float positionSave;

	// Swiping
	Vector2 startPos;
	float minSwipeDist = 150f;

	// Controls
	private bool touchActive;

	// Shield
	private bool shielded;
	private Collider2D[] enemyCol;

	// Debug Control
	public bool endless;
	int mistakeCount;
	int lastMistakeCount;

	// Control Variables

	[HideInInspector] public bool gameOver;
	[HideInInspector] public bool victory;
	[HideInInspector] public bool dead;
	[HideInInspector] public bool diedInLiquid;
	[HideInInspector] public bool fullStop;

	// The RidigBody 2D
	Rigidbody2D Rb2D;

	// The Movement Class
	Movement MV;

	// Initialization
	void Start ()
	{
		MV = GetComponent<Movement> ();
		Rb2D = GetComponent<Rigidbody2D> ();
		sound = FindObjectOfType<SoundManager> ();
		ds = FindObjectOfType<DataStorage> ();
		mistakeCount = 0;
		lastMistakeCount = 0;	
		gameOver = false;
		dead = false;
		diedInLiquid = false;
		fullStop = false;
		victory = false;
		touchActive = false;

		if (ds.shields > 0) {
			trail.SetActive (true);
			shielded = true;
			Debug.Log ("tenho um shield");
		} else {
			trail.SetActive (false);
			shielded = false;
		}
	}

	// Fixed Update of the Player
	void FixedUpdate ()
	{
		if (fullStop)
		{
			Rb2D.velocity = new Vector2 (0, 0);
			return;
		}

		// Mistake Mechanics
		processMistakes();

		if (dead) {
			if (enemyCol != null)
			{
				for(int i = 0; i < enemyCol.Length;i++)
					enemyCol[i].enabled = true;
			}

			if(diedInLiquid)
				Rb2D.velocity = new Vector2 (0, 0);
			else
				Rb2D.velocity = new Vector2 (0, Rb2D.velocity.y);
		} else {
			
			if (endless)
				Rb2D.velocity = new Vector2 (5, Rb2D.velocity.y);
			else
				MV.Horizontal_Velocity_Update (Rb2D);

			// Check for player controlled jumps
			MV.Jump (Rb2D);

			// Tells the movement script to update his axis
			MV.UpdateAxis ();
		}

		// Animate
		MV.Animate(Rb2D, dead);
	}

	void Update()
	{
		Swiping ();
	}

	public void Jump()
	{
		if(MV.slide == 1)
			MV.slide = 0;

		MV.jump = 1;
	}

	public void Slide()
	{
		sound.play = Sound.SLIDE;
		MV.slide = 1;
	}

	public void processMistakes()
	{
		if (!endless || dead)
			return;

		// Mistake Mechanics
		if (Rb2D.velocity.x < 1)
			mistakeCount++;

		if (mistakeCount == lastMistakeCount)
			mistakeCount = 0;

		lastMistakeCount = mistakeCount;

		if (mistakeCount > 1) {
			Debug.Log ("sound");
			sound.play = Sound.DEAD;
			dead = true;
		}
	}

	public void setGameOver()
	{
		gameOver = true;
	}

	public void setVictory()
	{
		victory = true;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (dead)
			return;

		GameObject go = other.gameObject;

		switch (go.tag) {
		case "Exit": 
			UnityAds.ShowAd ();
			sound.play = Sound.VICTORY;
			victory = true;
			break;
		case "Enemy":
			if (shielded) 
			{
				shielded = false;
				ds.shields--;
				ds.Save ();
				enemyCol = go.GetComponents<Collider2D> ();

				for(int i = 0; i < enemyCol.Length;i++)
					enemyCol[i].enabled = false;
				
				trail.SetActive (false);
				Debug.Log ("perdi o shield");
			} 
			else 
			{
				Debug.Log ("morri");
				sound.play = Sound.DEAD;
				dead = true;
			}
			break;
		case "Water":
			sound.play = Sound.DEAD;
			dead = true;
			diedInLiquid = true;
			break;
		}

	}

	public void Reset()
	{
		gameOver = false;
		dead = false;
		diedInLiquid = false;
		fullStop = false;
		victory = false;
		mistakeCount = 0;
		lastMistakeCount = 0;
		MV.Animate (Rb2D, dead);
		MV.ResetAxis ();
	}

	public void Swiping()
	{
		if (!ds.touchControls)
			return;
		
		if (Input.touchCount == 0)
			touchActive = true;
		
		if (!touchActive)
			return;
		
		if (Input.touchCount > 0) {

			for (int i = 0; i < Input.touchCount; i++) {
				Touch touch = Input.touches [i];

				Debug.Log ("Touch");

				switch (touch.phase) {
				case TouchPhase.Began:
					Debug.Log ("Began");
					startPos = touch.position;

					break;

				case TouchPhase.Ended:
					double swipeDist = (touch.position - startPos).magnitude;

					if (swipeDist < minSwipeDist) {
						Debug.Log ("Jump");
						Jump ();
					} else {
						Debug.Log ("Slide");
						Slide ();
					}
				
					break;
				}
			}
		} 
	}

	public void SavePosition()
	{
		positionSave = transform.position.x;
	}

	public void LoadPosition()
	{
		transform.position = new Vector3 (positionSave, transform.position.y, transform.position.z);
	}
}