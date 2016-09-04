using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public Player_Control player;
	public bool isFollowing;

	public float xOffset;
	public float yOffset;
	
	void Start () {
		player = FindObjectOfType<Player_Control> ();
		isFollowing = true;
	}

	void Update () {
		if(isFollowing)
			transform.position = new Vector3 (player.transform.position.x + xOffset, transform.position.y, -10);
	}
}
