using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	private Vector2 pos;
	public float leftBoundary;
	public float rightBoundary;

	void Update () {
		if (Input.touchCount > 0)
		{
			Touch inputTouch = Input.GetTouch (0);
			pos = inputTouch.deltaPosition;
			float x = transform.position.x - pos.x / Screen.width * 30f;
			x = Mathf.Max (x, leftBoundary);
			x = Mathf.Min (x, rightBoundary);
			transform.position = new Vector3 (x , transform.position.y, transform.position.z);
		}
		
	}
}
