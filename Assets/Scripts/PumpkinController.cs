using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinController : MonoBehaviour {

	[SerializeField]
	private AudioClip pumpkinSound;

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Ball") {
			AudioSource.PlayClipAtPoint (pumpkinSound, transform.position);
			Destroy (other.gameObject);
		}
	}
}
