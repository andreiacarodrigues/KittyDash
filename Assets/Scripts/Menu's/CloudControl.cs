using UnityEngine;
using System.Collections;

public class CloudControl : MonoBehaviour {

	public GameObject cloud;
	public Transform spawnPoint;

	private Transform cam;

	void Start () {
		cam = FindObjectOfType<Camera> ().transform;
	}
		
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.tag == "Cloud") {
			float posY = other.gameObject.transform.position.y;

			Destroy (other.gameObject);

			GameObject go = (GameObject) Instantiate (cloud, new Vector3 (spawnPoint.position.x, posY), Quaternion.identity);
			go.transform.SetParent (cam);
		}
	}

}
