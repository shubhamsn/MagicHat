using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiateBall : MonoBehaviour {

	[SerializeField]
	private GameObject RadiatedBall;

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Ball") 
		{
			Instantiate (RadiatedBall, other.transform.position, Quaternion.identity);
			Destroy (other.gameObject);
		}
	}
}
