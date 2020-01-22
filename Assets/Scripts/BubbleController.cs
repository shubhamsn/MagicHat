using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * attach with Bubbles
 * make it more realistic
 * 
 * 
 * misbehaving:: possible solution : destroy collided ball and instantiate a new simple sprite of the ball
 * 									 make it child of bubble set position 
 * */

public class BubbleController : MonoBehaviour {

	private float xVal, yVal;
	Vector2 vel;

	private bool isHaveBall = false;

	[SerializeField]
	private Sprite[] ballSprite;

	// Use this for initialization
	void Start () 
	{
		StartCoroutine (Movement ());
	}

	private IEnumerator Movement()
	{
		xVal = Random.Range (-0.5f, 0.5f);
		yield return new WaitForSeconds (1f);
		while (!GameController.instance.gameOver) 
		{	
			xVal = (xVal > 0f) ? (Random.Range (-0.25f, 0.75f)) : (Random.Range (-0.75f, 0.25f));

			//Debug.Log ("val " + xVal + " " + yVal);
			vel = new Vector2 (xVal,(Random.Range (0f, 0.5f)));
			this.GetComponent<Rigidbody2D> ().velocity = vel;
			/*if (ballTwo != null) 
			{
				ballTwo.velocity = vel;
				ballOne.velocity = vel;
				ballTwo.transform.position = new Vector2(transform.position.x - 0.15f, transform.position.y);
				ballOne.transform.position = new Vector2(transform.position.x + 0.15f, transform.position.y);
			}
			else if (ballOne != null) {
				ballOne.velocity = vel;
				ballOne.transform.position = transform.position;
			}*/
			//this.GetComponent<Rigidbody2D> ().AddForce(Random.insideUnitCircle);

			yield return new WaitForSeconds (2f);
		}
	}

	void OnTriggerEnter2D (Collider2D other) 
	{
		if (other.tag == "Ball" && !isHaveBall)
		{
			/*other.transform.position = transform.position;
			if (ballOne == null) {
				ballOne = other.gameObject.GetComponent<Rigidbody2D> ();
				ballOne.isKinematic = true;
				//ballOne.transform.position = transform.position;
			} else if (ballTwo == null) {
				
				ballTwo = other.gameObject.GetComponent<Rigidbody2D> ();
				ballTwo.isKinematic = true;
				ballTwo.transform.position = new Vector2(transform.position.x - 0.15f, transform.position.y);
				ballOne.transform.position = new Vector2(transform.position.x + 0.15f, transform.position.y);
			}*/
			//StartCoroutine (CarryBall(ball));
			Debug.Log("color ball" + other.GetComponent<BallController>().GetColor());
			transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = ballSprite[other.GetComponent<BallController>().GetColor()];
			isHaveBall = true;
			Destroy (other.gameObject);
		}
	}

	private IEnumerator CarryBall(Rigidbody2D ballL)
	{
		ballL.isKinematic = true;
		while (!GameController.instance.gameOver)
		{
			ballL.velocity = vel;
			yield return new WaitForEndOfFrame ();
		}
	}

	/*private float amplitude = .4f;
	private float speed = 1.5f;
	private float tempY;
	private float tempX;
	private Vector3 tempPos;
	private float startTime;


	void Start ()
	{
		tempY = transform.position.y;
		tempX = transform.position.x;
		startTime = Time.time;
	}

	void Update ()
	{        

		float newTime = Time.time - startTime;
		tempPos.y = tempY + amplitude * Mathf.Sin (speed * newTime);
		tempPos.x += .05f;
		transform.position = tempPos;    
	}*/
}
