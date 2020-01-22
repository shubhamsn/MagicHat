﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowerController : MonoBehaviour {

	[SerializeField]
	private float forceInX = 200f;			//checked for 200f

	void OnTriggerEnter2D (Collider2D other) 
	{
		if(other.tag == "Ball")
			other.gameObject.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (forceInX, 0f));
	}
}
