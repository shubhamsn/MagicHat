using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationObs : MonoBehaviour {

	Rigidbody2D obs ;
	[SerializeField]
	private float rotationFlag=20.0f;
	[SerializeField]
	private int rotationDirection = 1;

	void Start () 
	{
		obs = GetComponent<Rigidbody2D> ();
		obs.angularVelocity =  rotationFlag * rotationDirection;
	}
}
