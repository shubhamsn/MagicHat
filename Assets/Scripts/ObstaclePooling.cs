using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* attach with GameController
 * Pooling of obstacle : basket, eater
 * */

public class ObstaclePooling : MonoBehaviour {

	[SerializeField]
	private GameObject obstacle;

	private int multiplier;

	// Use this for initialization
	void Start () 
	{
		multiplier = 1;
		StartCoroutine (SpawnObject ());
	}
	
	private IEnumerator SpawnObject()
	{
		GameObject initObstacle;
		yield return new WaitForSeconds (3.0f);
		//counting = true;
		while (!GameController.instance.gameOver) 
		{
			multiplier = (Random.value > 0.5f)? 1:-1;
			Vector2 spawnPosition = new Vector2 ( multiplier * 4.0f, transform.position.y - Random.Range (1.0f, 5.0f));
			Quaternion spawnRotation = Quaternion.identity;
			initObstacle = Instantiate (obstacle, spawnPosition, spawnRotation) as GameObject;
			initObstacle.GetComponent<MoveObstacle> ().direction = -multiplier;
			yield return new WaitForSeconds (Random.Range (2.0f, 4.0f));
		}
	}
}
