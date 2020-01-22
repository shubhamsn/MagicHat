using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblePooling : MonoBehaviour {

	[SerializeField]
	private GameObject bubble;

	private int multiplier;
	[SerializeField]
	private float initialWaitTime = 3.0f;

	// Use this for initialization
	void Start () 
	{
		multiplier = 1;
		StartCoroutine (SpawnBubble());
	}

	private IEnumerator SpawnBubble()
	{
		GameObject initBubble;
		yield return new WaitForSeconds (initialWaitTime);
		//counting = true;
		while (!GameController.instance.gameOver) 
		{
			multiplier = (Random.value > 0.5f)? 1:-1;
			if (Random.value > 0.5f) {
				Vector2 spawnPosition = new Vector2 (multiplier * 3.0f, transform.position.y - Random.Range (5.0f, 10.0f));
				Quaternion spawnRotation = Quaternion.identity;
				initBubble = Instantiate (bubble, spawnPosition, spawnRotation) as GameObject;
				initBubble.GetComponent<Rigidbody2D> ().velocity = (new Vector2(-multiplier*0.5f, 0f));
			} 
			else {
				Vector2 spawnPosition = new Vector2 (Random.Range(-3f, 3f), -6.0f);
				Quaternion spawnRotation = Quaternion.identity;
				initBubble = Instantiate (bubble, spawnPosition, spawnRotation) as GameObject;
				initBubble.GetComponent<Rigidbody2D> ().velocity =(new Vector2(0f, 0.5f));
			}

			yield return new WaitForSeconds (Random.Range (2.0f, 4.0f));
		}
	}
}
