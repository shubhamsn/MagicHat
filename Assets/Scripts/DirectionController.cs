using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionController : MonoBehaviour {

	private Animator anim;
	private float zAngle;

	public bool selfDestroy = false;

	private int startTime = 15;
	private int endTime = 30;

	void Start()
	{
		anim = GetComponent<Animator> ();
		zAngle = Random.Range (-90f, 90f);
		transform.rotation = Quaternion.Euler (0f, 0f, zAngle);

		if (selfDestroy)
			StartCoroutine (Disappear());
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Ball") {
			other.gameObject.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (zAngle * 2f, zAngle * 2f));
		}
	}

	private IEnumerator Disappear()
	{
		yield return new WaitForSeconds (2f);
		//yield return new WaitForSeconds (Random.Range(startTime, endTime));
		//Destroy
		anim.SetTrigger("Disappear");
		yield return new WaitForSeconds (1f);
		Destroy(gameObject);
	}
}
