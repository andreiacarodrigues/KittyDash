using UnityEngine;
using System.Collections;

public class GlowControl : MonoBehaviour
{
	ParticleSystem glow;
	float startSize;

	// Use this for initialization
	void Start ()
	{
		glow = GetComponent<ParticleSystem> ();
		startSize = 2.8f;
	}
	
	// Update is called once per frame
	void Update ()
	{
		float ratio = (float) Screen.width / Screen.height;
		glow.startSize = startSize * ratio;
	}
}
