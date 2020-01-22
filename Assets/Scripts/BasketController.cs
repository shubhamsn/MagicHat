using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketController : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Ball" || other.tag == "Bomb") {
			other.tag = "Untagged";
		}
	}
}
