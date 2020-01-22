using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Summary:
 * 		have stick bar which defines color the hat can accept and 
 * 		it changes with time when time bar reintitializes 
 * 		if hat catches wrong colored ball lifeCount -- 
 * 		and if lifeCount == 0 --> game Over
 * 
 * attach with Hat object	(attach it or either CatchingBallCollision.cs script)
 * Detect collision :1) in case of accepting color of hat is changing 
 * 						check haveTimeBar false
 * 					 2) in case of time bar of accepting color
 * 						check haveTimeBar true
 * 						
 * maintain colorSticker gameObject as color of hat
 * */


public class HatColorChanger : MonoBehaviour {

	[SerializeField]
	private bool haveTimeBar;
	[SerializeField]
	private Slider timeBar;
	[SerializeField]
	private Image FillColor;
	private float maxValue = 20f;
	private float curValue;

	[SerializeField]
	private GameObject colorSticker;
	[SerializeField]
	private int stickColor;

	[SerializeField]
	private int hatLifeCount;
	private int ballColor;

	private int ballCount = 4;

	void Start()
	{
		curValue = maxValue;
		ChangeColor ();
	}

	void Update()
	{
		if (haveTimeBar) 
		{
			curValue -= Time.deltaTime;
			timeBar.value = curValue;
			if (curValue <= 0) 
			{
				curValue = maxValue;
				ChangeColor ();
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Ball") {
			Debug.Log ("wrong");
			ballColor = other.gameObject.GetComponent<BallController> ().GetColor ();

			if (ballColor == stickColor)
				GameController.instance.CheckLevelStatus (ballColor);
			else {
				hatLifeCount--;
				if (hatLifeCount == 0)
					GameController.instance.gameOver = true;
			}
			if (!haveTimeBar)
				ChangeColor ();
			Destroy (other.gameObject);
		}
	}

	//TODO : need to update switch
	private void ChangeColor()
	{
		int newColor = Random.Range (0, ballCount);
		switch (newColor) {
		case 0:
			FillColor.color = Color.blue;
			break;
		case 1:
			FillColor.color = Color.green;
			break;
		case 2:
			FillColor.color = Color.red;
			break;
		case 3:
			FillColor.color = Color.yellow;
			break;			
		}
		if (stickColor == newColor)
			ChangeColor ();
		else
			stickColor = newColor;
		switch (stickColor) {
		case (0):
			break;
		}
	}
}
