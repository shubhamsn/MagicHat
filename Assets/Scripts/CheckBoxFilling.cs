using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBoxFilling : MonoBehaviour {

	/* Summary : 
	 * 		a collider box if balls in the floor reach to exit that collider game over
	 * */
	void OnTriggerExit2D (Collider2D other)
	{
		if(other.tag == "Ball")
			GameController.instance.gameOver = true;
	}
}
