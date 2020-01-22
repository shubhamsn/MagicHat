using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * Attach with GameContoller.cs script 
 * 
 * minForcex = 30, maxForceY = 250
 * for right side make Y Rotation 180
 * */
public class GunBallThrower : MonoBehaviour {

	[SerializeField]
	private GameObject[] Guns;

	[SerializeField]
	private GameObject[] balls;

	[SerializeField]
	private int minForceX = 30, maxForceX = 250;			//ideal 30 to 250 from down to up

	[SerializeField]
	private int IntialWaitTime = 2;

	[SerializeField]
	private float StartWaitTime = 1f;

	[SerializeField]
	private float EndWaitTime = 2f;

	private int leftOrRight = 1;
	private float ReqGunWidth;


	// Use this for initialization
	void Start () 
	{
		ReqGunWidth = Guns[0].GetComponent<Renderer> ().bounds.extents.x * 3 / 4;
		StartCoroutine (Spawn ());	
	}

	public IEnumerator Spawn () 
	{
		yield return new WaitForSeconds (IntialWaitTime);
		GameObject instateBall;
		int gunPosition;
		//counting = true;
		while (!GameController.instance.gameOver) {
			GameObject ball = balls [Random.Range (0, balls.Length)];
			gunPosition = Random.Range (0, Guns.Length);
			leftOrRight = (Guns [gunPosition].transform.position.x <= 0) ? 1 : -1;
			Debug.Log ("leftOrrigh " + leftOrRight);
				
			Vector3 spawnPosition = new Vector3 (
				Guns[gunPosition].transform.position.x + ReqGunWidth * leftOrRight, 
				Guns[gunPosition].transform.position.y, 
				0.0f
			);
			Quaternion spawnRotation = Quaternion.identity;

			instateBall = Instantiate (ball, spawnPosition, spawnRotation) as GameObject;
			instateBall.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(leftOrRight * minForceX, leftOrRight * maxForceX),0f));
			yield return new WaitForSeconds (Random.Range (StartWaitTime, EndWaitTime));
		}
		yield return new WaitForSeconds (2.0f);
		//gameOverText.SetActive (true);
		yield return new WaitForSeconds (2.0f);
		//restartButton.SetActive (true);
	}
}
