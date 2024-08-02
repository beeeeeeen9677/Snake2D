using UnityEngine;

public class CSVReader : MonoBehaviour
{
    public static CSVReader instance;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /*
    private void Start()
    {
        ReadAllCSV();
    }
    */

    public void ReadAllCSV()// call by MainMenu.cs
    {
        ReadCSV("categoryList");
        ReadCSV("gameSceneUITextData");
        ReadCSV("mainmenuText");
        ReadCSV("scenarioData");
        ReadCSV("snake2dTextData");
    }

    private void ReadCSV(string csvName)
    {
        if (csvName == "categoryList")
        {
            DataManager.instance.categoryList = Resources.Load<TextAsset>(csvName);
        }
        else if (csvName == "gameSceneUITextData")
        {
            DataManager.instance.gameSceneUITextData = Resources.Load<TextAsset>(csvName);
        }
        else if (csvName == "mainmenuText")
        {
            DataManager.instance.mainMenuText = Resources.Load<TextAsset>(csvName);
        }
        else if (csvName == "scenarioData")
        {
            DataManager.instance.scenarioData = Resources.Load<TextAsset>(csvName);
        }
        else if (csvName == "snake2dTextData")
        {
            DataManager.instance.snake2dTextData = Resources.Load<TextAsset>(csvName);
        }
        else
        {
            Debug.LogError("CSV Reader: no such csv file. ");
        }
    }
}
