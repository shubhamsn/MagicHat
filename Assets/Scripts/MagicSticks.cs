using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSticks : MonoBehaviour {

	[SerializeField]
	private GameObject[] secondSticks;

	private float distance;

	void OnTriggerEnter2D(Collider2D other)
	{
		int rand = Random.Range (0, secondSticks.Length);
		distance = other.transform.position.x - transform.position.x;
		other.transform.position = new Vector2(secondSticks[rand].transform.position.x + distance, secondSticks[rand].transform.position.y);
	}

}
