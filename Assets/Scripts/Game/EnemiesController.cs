using UnityEngine;
using System.Collections;

public class EnemiesController : MonoBehaviour {

	public float offset;
	public float movementSpeed;
	public int dir;
	private float initPos;

	public float scale;

	Rigidbody2D rb2d;

	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		initPos = gameObject.transform.position.x;
	}

	void Update () {
		rb2d.velocity = new Vector2 (movementSpeed * dir, rb2d.velocity.y);

		if ((gameObject.transform.position.x < (initPos - offset) && dir == -1) || (gameObject.transform.position.x > (initPos + offset) && dir == 1))
			dir = -dir;

		if (dir != 1)
			transform.localScale = new Vector3 (scale, scale, 1f);
		else
			transform.localScale = new Vector3 (-scale, scale, 1f);
	}
}
