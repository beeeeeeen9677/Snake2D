using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneLanguageManager : MonoBehaviour
{
    [SerializeField]
    private TextAsset gameSceneTextCsvFile; // Text in different language for game scene
    [SerializeField]
    private Text[] uiTexts;
    [SerializeField]
    private string[] columnName;
    private string[] gameSceneDataRows;//0: TradChi, 1: SimpChi, 2: ENG



    private void Awake()
    {
        string[] temp = gameSceneTextCsvFile.text.Trim().Split('\n');
        columnName = temp[0].Split(';');
        gameSceneDataRows = temp.Skip(1).ToArray();

        for (int i = 0; i < columnName.Length; i++)
        {
            columnName[i] = columnName[i].Trim();
        }

        SetText();
    }

    private void SetText()
    {
        int languageIndex = PlayerPrefs.GetInt("Language");
        string[] textData = gameSceneDataRows[languageIndex].Split(';');

        foreach (Text uiText in uiTexts)
        {
            /*
            Debug.Log("uiText: " + uiText.gameObject.name.Length);
            Debug.Log("array: " + columnName[12].Trim().Length);
            Debug.Log(Array.IndexOf(columnName, uiText.gameObject.name));
            */

            uiText.text = textData[Array.IndexOf(columnName, uiText.gameObject.name)];
        }
    }
}
