using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchingBallCollision : MonoBehaviour {

	[SerializeField]
	private GameObject HatGlow;

	private Animator Anim;
	private SpriteRenderer GlowImage;

	void Start()
	{
		Anim = HatGlow.GetComponent<Animator> ();
		GlowImage = HatGlow.GetComponent<SpriteRenderer> ();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Ball") {
			GameController.instance.CheckLevelStatus (other.gameObject.GetComponent<BallController> ().GetColor ());
			Destroy (other.gameObject);
			StartCoroutine (Glow(other));
		}
	}

	private IEnumerator Glow(Collider2D other)
	{
		HatGlow.SetActive (true);
		switch(other.gameObject.GetComponent<BallController>().GetColor())
		{
			case 0:
			Debug.Log ("blue color count " + other.gameObject.GetComponent<BallController> ().GetColor ());
				GlowImage.color = Color.blue;
				break;
			case 1:
			Debug.Log ("green color count " + other.gameObject.GetComponent<BallController> ().GetColor ());

				GlowImage.color = Color.green;
				break;
			case 2:
			Debug.Log ("red color count " + other.gameObject.GetComponent<BallController> ().GetColor ());

				GlowImage.color = Color.red;
				break;
			case 3:
			Debug.Log ("yellow color count " + other.gameObject.GetComponent<BallController> ().GetColor ());

				GlowImage.color = Color.yellow;
				break;
		}
		Anim.SetTrigger ("GlowAnim");
		yield return new WaitForSeconds (0.4f);
		GlowImage.color = Color.white;
		HatGlow.SetActive (false);
	}
}
