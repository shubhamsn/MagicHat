using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LolipopStick : MonoBehaviour {

	[SerializeField]
	private AudioClip StickySound;

	void OnTriggerEnter2D (Collider2D other) 
	{
		if (other.tag == "Ball") 
		{
			AudioSource.PlayClipAtPoint (StickySound, transform.position);
			Rigidbody2D rb2d = other.gameObject.GetComponent<Rigidbody2D> ();

			rb2d.bodyType = RigidbodyType2D.Kinematic;
			rb2d.velocity = Vector2.zero;
			rb2d.angularVelocity = 0f;
		}
	}

}
