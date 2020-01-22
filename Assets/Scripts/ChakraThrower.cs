using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChakraThrower : MonoBehaviour {

	[SerializeField]
	private GameObject[] ball;

	private int sizeOfSticks = 8;
	private GameObject[] stickedBall; 

	[SerializeField]
	private float rotationSpeed = 50f;

	[SerializeField]
	private float initialWaitTime= 3f;
	[SerializeField]
	private float waitTime = 3f;

	[SerializeField]
	private int forceMultiplier = 500;


	// Use this for initialization
	void Start () 
	{
		stickedBall = new GameObject[sizeOfSticks];
		float chakraWidthX = GetComponent<Renderer>().bounds.extents.x;
		Debug.Log (Mathf.Sqrt (2));
		float diagChakraWidth = Mathf.Sqrt (0.5f) * chakraWidthX;

		int i = 0;
		PlaceBalls (i++, transform.position.x + chakraWidthX, transform.position.y);
		PlaceBalls (i++, transform.position.x - chakraWidthX, transform.position.y);
		PlaceBalls (i++, transform.position.x, transform.position.y + chakraWidthX);
		PlaceBalls (i++, transform.position.x, transform.position.y - chakraWidthX);
		PlaceBalls (i++, transform.position.x + diagChakraWidth, transform.position.y + diagChakraWidth);
		PlaceBalls (i++, transform.position.x + diagChakraWidth, transform.position.y - diagChakraWidth);
		PlaceBalls (i++, transform.position.x - diagChakraWidth, transform.position.y - diagChakraWidth);
		PlaceBalls (i++, transform.position.x - diagChakraWidth, transform.position.y + diagChakraWidth);

		gameObject.GetComponent<Rigidbody2D> ().angularVelocity = -rotationSpeed;
		StartCoroutine (ThrowBalls ());
	}


	private IEnumerator ThrowBalls()
	{
		int index=0;
		Vector2 ballForce; 
		yield return new WaitForSeconds (initialWaitTime);
		while (!GameController.instance.gameOver && index != sizeOfSticks) {
			ballForce = new Vector2 (stickedBall[index].transform.position.x - transform.position.x,
				stickedBall[index].transform.position.y - transform.position.y);
			stickedBall [index].GetComponent<Rigidbody2D> ().isKinematic = false;
			stickedBall [index].GetComponent<Rigidbody2D> ().AddForce (ballForce * forceMultiplier);
			stickedBall [index].GetComponent<Collider2D> ().isTrigger = false;		//so other balls does not collide dynamically
			index++;
			yield return new WaitForSeconds(Random.Range(1f, waitTime ));
		}
	}

	private void PlaceBalls(int index, float xPos, float yPos)
	{
		stickedBall[index] = Instantiate (
			ball [Random.Range (0, ball.Length)], 
			new Vector2 (xPos,  yPos), 
			Quaternion.identity,
			transform) as GameObject;
		stickedBall [index].GetComponent<Rigidbody2D> ().isKinematic = true;
		stickedBall [index].GetComponent<Collider2D> ().isTrigger = true;		//so other balls does not collide dynamically
	}

}
