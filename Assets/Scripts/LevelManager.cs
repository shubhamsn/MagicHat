using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;
using System.Collections;

public class LevelManager : MonoBehaviour {

    private List<LevelMetaData> levelMetaDatas = new List<LevelMetaData>();

    public static LevelManager LevelManagerInstance;

    [SerializeField]
    public GameObject StartAnimPanel;

    [SerializeField]
    public GameObject LoadingPanel;

    void Awake()
    {
        if (LevelManagerInstance == null)
        {
            DontDestroyOnLoad(gameObject);
            LevelManagerInstance = this;
        }
        else if (gameObject != this)
        {
            //Destroy(gameObject);
            Destroy(LevelManagerInstance.gameObject);
            LevelManagerInstance = this;
            DontDestroyOnLoad(gameObject);
        }

        //Delete();

        levelMetaDatas = LoadData();
        Debug.Log("list size in awake " + levelMetaDatas.Count);

        //if (levelMetaDatas.Count == 0)
        //    SaveData(1, new LevelMetaData());
    }

    void Start()
    {
        StartCoroutine(DeactivateAnimPanel());
    }

    private IEnumerator DeactivateAnimPanel()
    {
        yield return new WaitForSeconds(1.0f);
        StartAnimPanel.SetActive(false);
    }

    public void SaveData(int levelNumber, LevelMetaData _levelMetaData )
    {
        int listSize = levelMetaDatas.Count;

        try
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = File.Create(Application.persistentDataPath + "/MagicHat.dat");

            LevelMetaData levelMetaData = new LevelMetaData()
            {
                HighScore = _levelMetaData.HighScore,
                BestTime = _levelMetaData.BestTime,
                StarCount = _levelMetaData.StarCount
            };

            if (listSize >= levelNumber)
            {
                levelMetaDatas[levelNumber-1] = levelMetaData;
            }
            else
            {
                levelMetaDatas.Add(levelMetaData);
            }

            Debug.Log("after saving new list size" + levelMetaDatas.Count);

            binaryFormatter.Serialize(fileStream, levelMetaDatas);
            fileStream.Close();
        }
        catch(Exception ex)
        {
            Debug.Log("SaveData Exception : " + ex.Message);
        }
    }

    public List<LevelMetaData> LoadData()
    {
        List<LevelMetaData> levelMetaData = new List<LevelMetaData>();
        try
        {
            Debug.Log("LoadData method");
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = File.Open(Application.persistentDataPath + "/MagicHat.dat", FileMode.Open);

            levelMetaData = (List<LevelMetaData>)binaryFormatter.Deserialize(fileStream);
            fileStream.Close();
        }
        catch(Exception ex)
        {
            Debug.Log("LoadData Exception : "+ ex.Message);
        }

        return levelMetaData;
    }

    public LevelMetaData GetData(int levelNumber)
    {
        int listSize = levelMetaDatas.Count;
        Debug.Log("list size in getData method " +listSize);
        if (listSize >= levelNumber)
            return levelMetaDatas[levelNumber - 1];
        else
            return null;
    }

    public int GetListSize()
    {
        Debug.Log("getListSize method");
        return levelMetaDatas.Count;
    }

    public void Delete()
    {
        try
        {
            File.Delete(Application.persistentDataPath + "/MagicHat.dat");
            Debug.Log("file Deleted");
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }

    public void StartGame()
	{
		SceneManager.LoadScene ("main");
        //SceneManager.LoadScene(EventSystem.current.currentSelectedGameObject.name);
    }
}

[Serializable]
public class LevelMetaData
{
    public int HighScore;

    public int BestTime;

    public int StarCount;
}

