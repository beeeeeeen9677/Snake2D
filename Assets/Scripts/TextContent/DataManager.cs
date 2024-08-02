using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

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

    public TextAsset categoryList;
    public TextAsset gameSceneUITextData;
    public TextAsset mainMenuText;
    public TextAsset scenarioData;
    public TextAsset snake2dTextData;
}
