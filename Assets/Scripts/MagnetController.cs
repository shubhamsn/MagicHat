using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetController : MonoBehaviour {

	[SerializeField]
	private GameObject Hat;

	[SerializeField]
	private int Speed = 1;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Ball") {
			other.transform.position = Vector3.MoveTowards(other.transform.position, transform.position, Speed * Time.deltaTime);
			//Debug.Log ("YEs");
		}
	}
}
