using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public Camera cam;
	public GameObject[] balls;

	/* decide level
	 *	LevelType 1 : number of different colored balls needed to be caught
	 *	LevelType 2 : number of balls needed to be caught
	 *	LevelType 3 : score must be on target level
	 **/
	[SerializeField]
	private int initLevel = 1;		
	[SerializeField]
	private Text scoreText;

	[SerializeField]
	private int[] colorCount = new int[4];

	[SerializeField]
	private int collidedBallCount;

	[SerializeField]
	private int targetScore;

	[SerializeField]
	private Slider scoreMeter;
	[SerializeField]
	private int ballValueScoreMeter = 1;	//ball values for point meter for scores and stars
	[SerializeField]
	private int[] starValues;
	private int starCount = 0;
	[SerializeField]
	private int maxValueScoreMeter = 20;

	public bool gameOver;
	private int score;

	public GameObject gameOverText;
	public GameObject restartButton;
	public GameObject splashScreen;
	public GameObject startButton;
	public HatController hatController;

	private float maxWidth;
	private bool counting;

	public static GameController instance;

	void Awake()
	{
		if (instance == null)
			instance = this;
		else
			Destroy (gameObject);
	}
	// Use this for initialization
	void Start () 
	{
		gameOver = false;
		if (cam == null) {
			cam = Camera.main;
		}
		InitializeLevel ();
		Vector3 upperCorner = new Vector3 (Screen.width, Screen.height, 0.0f);
		Vector3 targetWidth = cam.ScreenToWorldPoint (upperCorner);
		float ballWidth = balls[0].GetComponent<Renderer>().bounds.extents.x;
		maxWidth = targetWidth.x - ballWidth;
		StartGame ();
		score = 0;
		scoreText.text = "Score:" + score;
		scoreMeter.value = 0;
		scoreMeter.maxValue = maxValueScoreMeter;
	}

	/*void FixedUpdate () {
		if (!gameOver) 
		{
			if(initLevel == 3)
			{
				timeLeft -= Time.deltaTime;
				if (timeLeft < 0) {
					timeLeft = 0;
					gameOver = true;
				}
				timerText.text = "Time Left:" + Mathf.RoundToInt (timeLeft);
			}
		}
	}*/


	//InitializeLevel() : initializes type of level (default is 1)
	private void InitializeLevel()
	{
		switch (initLevel) {
		case(1):
			
			break;
		}
	}

	//StartGame() : start game here
	public void StartGame () 
	{
		/*splashScreen.SetActive (false);
		startButton.SetActive (false);*/
		hatController.ToggleControl (true);
	}

	//called from HatColorChanger.cs & CatchingBallCollision.cs when ball dip in hat
	public void CheckLevelStatus(int count)			
	{
		//Debug.Log ("Check level status");
		score += 50;
		scoreText.text = "Score:" + score;
		scoreMeter.value += ballValueScoreMeter;
		//Debug.Log ("Screen meter " + scoreMeter.value);
		switch (initLevel) {
		case(1):
			bool flag = true;
			if (colorCount [count] != 0) {
				colorCount [count]--;
			}
			for (int i = 0; i < 4; i++) {
				if (colorCount [i] != 0) {
					flag = false;
					break;
				}
			}
			if (flag) {
				gameOverUpdates ();
				//level complete
			}
			break;
		case(2):
			if (collidedBallCount > 0) {
				collidedBallCount--;
			}
			if (collidedBallCount == 0) {
				//Level Complete
				gameOverUpdates ();
			}
			break;
		case(3):
			if (score >= targetScore) 
			{
				gameOverUpdates ();
			}
			break;
		}

	}

	private void gameOverUpdates()
	{
		gameOver = true;
		Debug.Log ("Level Complete\n scoremeter value "+ scoreMeter.value);

		if(scoreMeter.value >= starValues[2])
			//3 stars
			starCount = 3;
		else if(scoreMeter.value >= starValues[1])
			//2 stars
			starCount = 2;
		else if(scoreMeter.value >= starValues[0])
			//1 star
			starCount = 1;
		else
			//0 star
			starCount = 0;

		Debug.Log ("stars count " + starCount);
	}
}
