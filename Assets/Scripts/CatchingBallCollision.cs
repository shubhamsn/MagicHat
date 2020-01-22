using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchingBallCollision : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Ball") {
			GameController.instance.CheckLevelStatus (other.gameObject.GetComponent<BallController> ().GetColor ());
			Destroy (other.gameObject);
		}
	}
}
