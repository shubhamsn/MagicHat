using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public class GameController : MonoBehaviour
{

	public Camera cam;
	
    /* decide level
	 *	LevelType 1 : number of different colored balls needed to be caught
	 *	LevelType 2 : number of balls needed to be caught
	 *	LevelType 3 : score must be on target level
     *	LevelType 4 : time
	 **/
    [SerializeField]
    [Tooltip("Level number for each level (set it manually)")]
    private int LevelNumber = 0;

    [SerializeField]
    [Tooltip("Type of levels")]
	private int initLevel = 1;

    public GameObject[] balls;

    [SerializeField]
	private Text scoreText;

	[SerializeField]
    [Tooltip("For level type 1: set numbers for each ball should catch to complete level\n0: Blue\n1: Green\n2: Red\n3: Yellow")]
    private int[] colorCount = new int[4];

	[SerializeField]
    [Tooltip("For level type 2: set how many balls collect to complete the level")]
    private int targetCollidedBallCount;

	[SerializeField]
    [Tooltip("For level Type 3: set target score")]
    private int targetScore;

	[SerializeField]
    [Tooltip("scoremeter bottom right corner")]
    private Slider scoreMeter;
	[SerializeField]
    [Tooltip("Value of each ball for point meter for score and stars")]
    private int ballValueScoreMeter = 1;	//ball values for point meter for scores and stars
	[SerializeField]
    [Tooltip("Number of balls catch to collect each star")]
    private int[] starValues;
	private int starCount = 0;
	[SerializeField]
    [Tooltip("star object (image) on score meter")]
    private GameObject StarIndicator1;
	[SerializeField]
    [Tooltip("star object (image) on score meter")]
    private GameObject StarIndicator2;
	[SerializeField]
    [Tooltip("star object (image) on score meter")]
    private GameObject StarIndicator3;
	[SerializeField]
    [Tooltip("set of star images (after achieved (colored))")]
    private Sprite[] StarIndicatorOnAchieve;
	[SerializeField]
    [Tooltip("maximum value of score meter (should be ballValueScoreMeter * number of balls)(Recheck)")]
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

	[SerializeField]
	private GameObject PauseMenu;

    [SerializeField]
    private TextMeshProUGUI LevelText;

    [SerializeField]
	private GameObject PauseButton;

	public bool isPause;

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

        LevelText.SetText("Level {0}", LevelNumber);

		isPause = false;
		float xValue = ((160/maxValueScoreMeter) * starValues[0]) - 80;
		StarIndicator1.transform.localPosition = new Vector2(xValue, 40f);

		xValue = ((160/maxValueScoreMeter) * starValues[1]) - 80;
		StarIndicator2.transform.localPosition = new Vector2(xValue, 40f);

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

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            if (gameOver)
                EndGame();
            else
                PauseGame();
        }
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

		//Update StarIndicator
		if(scoreMeter.value >= starValues[2])
			//3 stars
			StarIndicator3.GetComponent<Image>().sprite = StarIndicatorOnAchieve[2];	
		else if(scoreMeter.value >= starValues[1])
			//2 stars
			StarIndicator2.GetComponent<Image>().sprite = StarIndicatorOnAchieve[1];
		else if(scoreMeter.value >= starValues[0])
			//1 star
			StarIndicator1.GetComponent<Image>().sprite = StarIndicatorOnAchieve[0];
		
		
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
			if (targetCollidedBallCount > 0) {
				targetCollidedBallCount--;
			}
			if (targetCollidedBallCount == 0) {
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

        //Set level status (level metadata)
        int newHighScore = score;

        LevelMetaData levelMetaData = LevelManager.LevelManagerInstance.GetData(LevelNumber);

        if (levelMetaData != null)
        {
            newHighScore = (score > levelMetaData.HighScore) ? score : levelMetaData.HighScore;
            Debug.Log("old Highscore " + levelMetaData.HighScore);
            
        }
        LevelMetaData levelMetaDataNew = new LevelMetaData()
        {
            HighScore = newHighScore,
            StarCount = starCount,
            BestTime = 0  //TODO
        };
        Debug.Log("new Highscore " + levelMetaDataNew.HighScore);

        LevelManager.LevelManagerInstance.SaveData(LevelNumber, levelMetaDataNew);
        Debug.Log("data saved");
        SceneManager.LoadScene("LevelBG");
    }

	#region Buttons
    //Do not write any other code in PauseGame(), write in PauseGameCoroutine()
    public void PauseGame()
    {
        StartCoroutine(PauseGameCoroutine());
    }

	public IEnumerator PauseGameCoroutine()
	{
        PauseButton.SetActive(false);
        PauseMenu.SetActive(true);
        isPause = true;

        yield return new WaitForSeconds(0.75f);
        Time.timeScale = 0;
	}

    public void RestartLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

	public void ContinueGame()
	{
		StartCoroutine(ContinueGameCoroutine());
	}

    private IEnumerator ContinueGameCoroutine()
    {
        isPause = false;
        try
        {
            Animator PopUpAnimator = PauseMenu.GetComponent<Animator>();

            if(PopUpAnimator != null)
            {
                PopUpAnimator.SetTrigger("EndPopUp");
            }
        }
        catch(Exception ex)
        {
            Debug.LogException(ex);
        }
        
        yield return new WaitForSeconds(0.75f);
        PauseButton.SetActive(true);
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void EndGame()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene ("Menu");
	}

	#endregion
}
