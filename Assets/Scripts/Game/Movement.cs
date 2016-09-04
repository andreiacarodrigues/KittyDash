using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
	public enum State{
		RUNNING,
		JUMPING,
		SLIDING,
		DEAD,
		DROWNED
	};

	public State state;
	private SoundManager sound;

	// Constants
	public float Jump_Force;

	// Speed
	public float Speed = 6f;
	public float Base_Speed = 6f;

	//public float Speed_Mod = 9f;
	//public float Speed_Mod_Alive_Time = 0f;

	// Axis

	// For horizontal movement
	private float horizontal = 0;

	// For jumps
	[HideInInspector] public float jump = 0;

	// For slidding
	[HideInInspector] public float slide = 0;


	// Ground Checks

	public Transform groundCheck;
	float groundRadius = 0.1f;
	public LayerMask whatIsGround;

	// Animation
	private Animator anim;

	// Use this for initialization
	void Start ()
	{
		anim = GetComponent<Animator> ();
		sound = FindObjectOfType<SoundManager> ();
		state = State.RUNNING;
	}
		
	// Updates the axis based on platform
	public void UpdateAxis ()
	{
		horizontal = Input.GetAxisRaw ("Horizontal");
		jump = Input.GetAxisRaw ("Jump");
	}

	public void Horizontal_Velocity_Update (Rigidbody2D  Rb2D)
	{
		Rb2D.velocity = new Vector2 (0, Rb2D.velocity.y);

		if (horizontal > 0)
			Rb2D.velocity = new Vector2 (Speed, Rb2D.velocity.y);

		if (horizontal < 0)
			Rb2D.velocity = new Vector2 (-Speed, Rb2D.velocity.y);
	}

	// Simple Jump
	public void Jump (Rigidbody2D  Rb2D)
	{
		if (!(jump == 1 && Grounded ()))
			return;

		sound.play = Sound.JUMP; // Talvez mudar isto para Player_Control -> Jump()
		Rb2D.velocity = new Vector2 (Rb2D.velocity.x, 0);
		Rb2D.AddForce (new Vector2 (0, Jump_Force));
	}

	// Jump after hitting an enemy
	public void Hit_Jump (Rigidbody2D  Rb2D)
	{
		Rb2D.velocity = new Vector2 (0, 0);
		Rb2D.AddForce (new Vector2 (0, 400));
	}
		
	// Checks if the player is on the ground
	public bool Grounded()
	{
		return (Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround));
	}

	// Update Animations
	public void Animate(Rigidbody2D Rb2D, bool dead)
	{
		if (dead)
		{
			anim.SetBool ("Dying", true);
			return;
		}
		else
			anim.SetBool ("Dying", false);
			
		if (!Grounded ()) 
		{
			if (Rb2D.velocity.y > 0)
				anim.SetBool ("Jumping", true);
			else if (Rb2D.velocity.y < 0) {
				anim.SetBool ("Jumping", false);
				anim.SetBool ("Falling", true);
			}
		}
		else 
		{
			if (slide == 1) 
				anim.SetBool ("Sliding", true);
			else
				anim.SetBool ("Sliding", false);
				
			anim.SetBool ("Falling", false);
			anim.SetBool ("Jumping", false);
		}
	}

	public void ResetAxis()
	{
		slide = 0;
		jump = 0;
	}
}
