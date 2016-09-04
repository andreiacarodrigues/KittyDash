using UnityEngine;
using System.Collections;

public class F3DTrailExample : MonoBehaviour
{
    public float Mult;
    public float TimeMult;

	private TrailRenderer tr;

	public Transform player;

    // Use this for initialization
    void Start ()
	{
		tr = gameObject.GetComponent<TrailRenderer> ();
		tr.sortingLayerName = "Player";
		tr.sortingOrder = 1;
    }
    
    // Update is called once per frame
    void Update ()
    {
        // Used in the example scene
        // Moves the trail by circular trajectory 
		Debug.Log(Time.time * TimeMult);
		transform.position = player.position + new Vector3(Mathf.Sin(Time.time * TimeMult), Mathf.Cos(Time.time * TimeMult), 10f);    
    }
}
