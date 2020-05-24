using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    [SerializeField]
    private GameObject MenuStartSceneAnim;

    [SerializeField]
    private GameObject MenuEndSceneAnim;

    [SerializeField]
    private GameObject ExitPopUpLabel;

    [SerializeField]
    private GameObject ExitButton;

    void Start()
    {
        StartCoroutine(DeactivateAnimPanel());
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            ExitPopUp();
        }
    }

    private IEnumerator DeactivateAnimPanel()
    {
        yield return new WaitForSeconds(1.0f);
        MenuStartSceneAnim.SetActive(false);
    }

    public void StartGame()
	{
        StartCoroutine(LoadNewScene());
	}

    public void ExitPopUp()
    {
        ExitButton.SetActive(false);
        ExitPopUpLabel.SetActive(true);
    }

    public void CancelExit()
    {
        StartCoroutine(CancelExitCoroutine());
    }

    private IEnumerator CancelExitCoroutine()
    {
        try
        {
            Animator PopUpAnimator = ExitPopUpLabel.GetComponent<Animator>();

            if (PopUpAnimator != null)
            {
                PopUpAnimator.SetTrigger("EndPopUp");
            }
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }

        yield return new WaitForSeconds(0.75f);
        ExitButton.SetActive(true);
        ExitPopUpLabel.SetActive(false);
    }

    public void ExitGame()
	{
		Application.Quit();
	}

	public void OpneSettings()
	{
		
	}

    IEnumerator LoadNewScene()
    {
        MenuEndSceneAnim.SetActive(true);

        yield return new WaitForSeconds(1f);
        // Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
        //AsyncOperation async = SceneManager.LoadSceneAsync("Level" + LevelNumber + "Dem");
        AsyncOperation async = SceneManager.LoadSceneAsync("LevelBG");

        // While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
        while (!async.isDone)
        {
            yield return null;
        }

    }
}
