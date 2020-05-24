using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SingleLevel : MonoBehaviour
{
    [SerializeField]
    private int LevelNumber = 0;

    [SerializeField]
    private GameObject[] Stars;

    [SerializeField]
    private GameObject Lock;

    [SerializeField]
    private GameObject LevelNumberText;

    int listSize;
    // Start is called before the first frame update
    void Start()
    {
        LevelMetaData levelMetaData = LevelManager.LevelManagerInstance.GetData(LevelNumber);
        Debug.Log("start singleLevel method " + LevelNumber);
        listSize = LevelManager.LevelManagerInstance.GetListSize();

        if (listSize + 1 >= LevelNumber)
        {
            Debug.Log("listSize >= LevelNumber " + LevelNumber);
            Lock.SetActive(false);
            LevelNumberText.SetActive(true);
        }

        if (levelMetaData != null)
        {
            for (int i = 0; i < levelMetaData.StarCount; i++)
            {
                Debug.Log("starts " + i + LevelNumber);
                Stars[i].SetActive(true);
            }
        }
    }

    public void StartGame()
    {
        if (listSize + 1 >= LevelNumber)
        {
            LevelManager.LevelManagerInstance.LoadingPanel.SetActive(true);
            StartCoroutine(LoadNewScene());
        }
    }

    IEnumerator LoadNewScene()
    {
        yield return new WaitForSeconds(0.5f);
        // Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
        //AsyncOperation async = SceneManager.LoadSceneAsync("Level" + LevelNumber + "Dem");
        AsyncOperation async = SceneManager.LoadSceneAsync("main");

        // While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
        while (!async.isDone)
        {
            yield return null;
        }

    }
}
