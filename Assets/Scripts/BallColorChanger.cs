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

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Ball") 
		{
			int newColor = Random.Range (0, balls.Length);
			while (newColor == other.gameObject.GetComponent<BallController> ().GetColor ()) {
				newColor = Random.Range (0, balls.Length); 
			}
			Instantiate (balls [newColor], other.transform.position, Quaternion.identity);
			Destroy (other.gameObject);
		}
	}
}
