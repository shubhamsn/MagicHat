using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* attach with GameController
 * Level type 0: default
 * 				 set isForceApplied = false
 * 				 set upOrDown = 1
 * 
 * Level type 1: balls from down 
 * 				 set isForceApplied = true
 * 				 set upOrDown = -1
 * 				 required: bouncy boundry walls if from down
 *				 (set bounciness level of walls)
 *				 minForce and maxFroce (default 500 to 700)
 * 				 force in x direction
 * 
 * Level type 2: balls from up
 * 				 set isForceApplied = true
 * 				 set upOrDown = 1
 * 				 required: bouncy boundry walls 
 * 						   bouncy base
 * 				 (set bounciness level of walls)
 *				 minForce and maxFroce (default __ to __)
 * 				 force in x direction
 * 
 * Attach for bomb
 * */

public class BallPooing : MonoBehaviour {

	public GameObject[] balls;

	private Camera cam;

	private float maxWidth;

	[SerializeField]
	private bool isForceApplied;
	[SerializeField]
	private int upOrDown = 1;
	[SerializeField]
	private int minForce, maxForce;			//ideal 500 to 700 from down to up

	[SerializeField]
	private int IntialWaitTime = 2;

	[SerializeField]
	private float StartWaitTime = 1f;

	[SerializeField]
	private float EndWaitTime = 2f;


	// Use this for initialization
	void Start () 
	{
		if (cam == null) {
			cam = Camera.main;
		}
		Vector3 upperCorner = new Vector3 (Screen.width, Screen.height, 0.0f);
		Vector3 targetWidth = cam.ScreenToWorldPoint (upperCorner);
		float ballWidth = balls[0].GetComponent<Renderer>().bounds.extents.x;
		maxWidth = targetWidth.x - ballWidth;
		StartCoroutine (Spawn ());	
	}
	
	public IEnumerator Spawn () 
	{
		yield return new WaitForSeconds (IntialWaitTime);
		GameObject instateBall;
		//counting = true;
		while (!GameController.instance.gameOver) {
			GameObject ball = balls [Random.Range (0, balls.Length)];
			Vector3 spawnPosition = new Vector3 (
				transform.position.x + Random.Range (-maxWidth, maxWidth), 
				upOrDown * transform.position.y, 
				0.0f
			);
			Quaternion spawnRotation = Quaternion.identity;
			instateBall = Instantiate (ball, spawnPosition, spawnRotation) as GameObject;
			if(isForceApplied)
				instateBall.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-150f, 150f),Random.Range(minForce, maxForce)));
			yield return new WaitForSeconds (Random.Range (StartWaitTime, EndWaitTime));
		}
		yield return new WaitForSeconds (2.0f);
		//gameOverText.SetActive (true);
		yield return new WaitForSeconds (2.0f);
		//restartButton.SetActive (true);
	}
}
