using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchingBombCollision : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Bomb") {
			GameController.instance.CheckLevelStatus (other.gameObject.GetComponent<BallController> ().GetColor ());
			Destroy (other.gameObject);
		}
	}
}
