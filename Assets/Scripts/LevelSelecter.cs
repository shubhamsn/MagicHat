using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LevelSelecter : MonoBehaviour {

	GameObject a;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void LoadLevel()
	{
		SceneManager.LoadScene (EventSystem.current.currentSelectedGameObject.name);
	}
}
