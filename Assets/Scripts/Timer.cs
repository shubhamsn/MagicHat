using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	[SerializeField]
	private float timeLeft;
	[SerializeField]
	private Text timerText;

	// Use this for initialization
	void Start () 
	{
		timerText.text = "Time Left:" + Mathf.RoundToInt (timeLeft);	
	}
	
	void FixedUpdate () 
	{
		if (!GameController.instance.gameOver) 
		{
			timeLeft -= Time.deltaTime;
			if (timeLeft < 0)
			{
				timeLeft = 0;
				GameController.instance.gameOver = true;
			}
			timerText.text = "Time Left:" + Mathf.RoundToInt (timeLeft);

		}
	}
}
