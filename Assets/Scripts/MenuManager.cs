using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	public void StartGame()
	{		
		SceneManager.LoadScene ("LevelBG");
	}

	public void ExitGame()
	{
		Application.Quit();
	}

	public void OpneSettings()
	{
		
	}
}
