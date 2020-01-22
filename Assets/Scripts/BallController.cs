using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

	[SerializeField]
	private int color;

	[SerializeField]
	private int weight;			//weights tends to speed of fall

	[SerializeField]
	private float bouncy = 0f;

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Collider2D> ().sharedMaterial.bounciness = bouncy;
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public int GetColor()
	{
		return color;
	}
}
