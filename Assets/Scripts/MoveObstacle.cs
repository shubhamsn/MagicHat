using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstacle : MonoBehaviour {

	/* move basket in reverse of direction
	 * */
	Rigidbody2D rb2d;
	public int direction;	//set by BasketPooling 

	// Use this for initialization
	void Start () 
	{
		rb2d = gameObject.GetComponent<Rigidbody2D> ();
		rb2d.velocity = new Vector2(direction * 2f , 0f);
	}
}
