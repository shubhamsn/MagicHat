using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 2 ways:
 * 1: take array of sprites, change and change color in BallController script
 * 2: take array of balls, destroy previous ball and change it with instantiate place
 * */

public class BallColorChanger : MonoBehaviour {

	[SerializeField]
	private GameObject[] balls;

	[SerializeField]
	private GameObject[] LaserGuns;

	[SerializeField]
	private AudioClip SparkSound;

	private Camera cam;

	void Start()
	{
		if (cam == null) {
			cam = Camera.main;
		}
		Vector3 upperCorner = new Vector3 (Screen.width, Screen.height, 0.0f);
		Vector3 targetWidth = cam.ScreenToWorldPoint (upperCorner);
		float laserGunWidth = LaserGuns[0].GetComponent<Renderer>().bounds.extents.x;
		//float maxWidth = targetWidth.x - ballWidth;
		LaserGuns[0].transform.position = new Vector2(-(targetWidth.x), LaserGuns[0].transform.position.y);
		LaserGuns[1].transform.position = new Vector2((targetWidth.x), LaserGuns[0].transform.position.y);
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Ball") 
		{
			AudioSource.PlayClipAtPoint (SparkSound, transform.position);
			int newColor = Random.Range (0, balls.Length);
			while (newColor == other.gameObject.GetComponent<BallController> ().GetColor ()) {
				newColor = Random.Range (0, balls.Length); 
			}
			Instantiate (balls [newColor], other.transform.position, Quaternion.identity);
			Destroy (other.gameObject);
		}
	}
}
