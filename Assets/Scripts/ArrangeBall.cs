using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Attach with GameController
 * 
 * Level where mannually set all balls which starts falling one by one 
 * set all balls mannually in level 
 * set balls at lower index which have to fall first
 *  
 * Types: 1) balls fall one by one linearly based on index (here minus)
 * 		     set canRandom false
 * 
 * 		  2) balls fall randomly of a certain set
 *           set canRandom true
 * 			 <<<<Note>>>>  number of balls must be devisible by rangeLimit
 * 
 * */

public class ArrangeBall : MonoBehaviour {

	[SerializeField]
	private Rigidbody2D[] ball;
	[SerializeField]
	private bool canRandom;
	[SerializeField]
	private int rangeLimit;

	private int[] numbers;
	// Use this for initialization
	void Awake()
	{
		for (int i = 0; i < ball.Length; i++) 
		{
			ball [i].isKinematic = true;
		}
	}

	void Start () 
	{
		if (canRandom) {
			numbers = new int[ball.Length];
			for (int i = 0; i < ball.Length; i++) {
				numbers [i] = i;
			}
		}
		StartCoroutine (SpawnBalls());
	}
	
	private IEnumerator SpawnBalls()
	{
		int minus=0, temp, rand, count =0;
		yield return new WaitForSeconds (3f);
		if (canRandom) 					//set ranodm rangeLimit to fall random indexed ball of a certain set
		{
			while (count * rangeLimit != ball.Length) 
			{
				rand = count * rangeLimit + Random.Range (0, rangeLimit - minus);
				//Debug.Log ("rand " + rand + " num "+ numbers[rand] );
				ball [numbers [rand]].isKinematic = false;
				temp = numbers [rand];
				numbers [rand] = numbers [(count + 1) * rangeLimit - minus -1];
				numbers [(count + 1) * rangeLimit - minus - 1] = temp;
				minus++;
				if (minus >= rangeLimit) 
				{
					minus = 0;
					count++;
				}
				yield return new WaitForSeconds (3f);
			}
		}
		else
		{
			while (minus != ball.Length) 
			{
				ball [minus++].isKinematic = false;
				yield return new WaitForSeconds (3f);
			}
		}		
	}

}
