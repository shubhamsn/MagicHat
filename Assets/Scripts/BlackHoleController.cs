using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* *
 * attach with BlackHole
 * make it more realistic
 * 
 * */

public class BlackHoleController : MonoBehaviour {

	private float xVal, yVal;

	private Camera cam;

	private Vector3 targetWidth;

	[SerializeField]
	private float initialWaitTime = 10;
	// Use this for initialization
	void Start () 
	{
		if (cam == null)
			cam = Camera.main;
		Vector3 upperCorner = new Vector3 (Screen.width, Screen.height, 0.0f);
		targetWidth = cam.ScreenToWorldPoint (upperCorner);

		StartCoroutine (Movement ());
	}

	private IEnumerator Movement()
	{
		xVal = Random.Range (-0.3f, 0.3f);
		yVal = Random.Range (-0.3f, 0.3f);
		yield return new WaitForSeconds (initialWaitTime);
		while (!GameController.instance.gameOver) 
		{	
			xVal += (xVal > 0f) ? (Random.Range (-0.5f, 0.1f)) : (Random.Range (-0.1f, 0.5f));
			//xVal = (xVal > 0f) ? (Random.Range (-0.25f, 0.75f)) : (Random.Range (-0.75f, 0.25f));


			//xVal += Random.Range (-0.1f, 0.1f);
			if(xVal >= 0.6 || xVal <= -0.6)
				xVal = Random.Range (-0.3f, 0.3f);
			//Debug.Log ("val " + xVal + " " + yVal);
			yVal += (yVal > 0f) ? (Random.Range (-0.5f, 0.1f)) : (Random.Range (-0.1f, 0.5f));
			if(yVal >= 0.3 || yVal <= -0.3)
				yVal -= Random.Range (-0.3f, 0.3f);

			this.GetComponent<Rigidbody2D> ().velocity = GetVelocity ();

			yield return new WaitForSeconds (2f);
		}
	}

	private Vector2 GetVelocity()
	{
		if (transform.position.x > targetWidth.x)
			xVal = -0.5f;
		if (transform.position.x < -targetWidth.x)
			xVal = 0.5f;
		if (transform.position.y > targetWidth.y)
			yVal = -0.5f;
		if (transform.position.y < -targetWidth.y)
			yVal = 0.5f;

		Vector2 vel = new Vector2 (xVal,yVal);
		//Debug.Log ("Velocity " + vel);
		return vel;
	}

	void OnTriggerEnter2D (Collider2D other) 
	{
		if (other.tag == "Ball")
		{
			Destroy (other.gameObject);
		}
	}
}
