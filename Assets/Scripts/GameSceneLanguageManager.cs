using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneLanguageManager : MonoBehaviour //change UI language in game scene
{
    [SerializeField]
    private Text[] uiTexts;
    [SerializeField]
    private string[] objLabel;
    private string[,] gameSceneUITextData;//0: TradChi, 1: SimpChi, 2: ENG



    private void Awake()
    {
        string[] temp = DataManager.instance.gameSceneUITextData.text.Trim().Split('\n');
        temp = temp.Skip(1).ToArray();//skip the row of column name
        objLabel = new string[temp.Length];
        for (int i = 0; i < objLabel.Length; i++)
        {
            objLabel[i] = temp[i].Trim().Split(';').ToArray()[0];
        }

        gameSceneUITextData = new string[temp.Length, temp[0].Trim().Split(';').ToArray().Length - 1];//[13,3]
        for (int i = 0; i < gameSceneUITextData.GetLength(0); i++)
        {
            string[] rowData = temp[i].Trim().Split(';').ToArray();
            for (int j = 0; j < gameSceneUITextData.GetLength(1); j++)
            {
                gameSceneUITextData[i, j] = rowData[j + 1].Trim();//first column is Key
            }
        }



        for (int i = 0; i < objLabel.Length; i++)
        {
            objLabel[i] = objLabel[i].Trim();
        }

        SetText();
    }

    private void SetText()
    {
        int languageIndex = PlayerPrefs.GetInt("Language");
        //string[] textData = gameSceneUITextData[languageIndex].Split(';');
        string[] textData = new string[gameSceneUITextData.GetLength(0)];
        for (int i = 0; i < textData.Length; i++)
        {
            textData[i] = gameSceneUITextData[i, languageIndex];
        }

        foreach (Text uiText in uiTexts)
        {
            /*
            Debug.Log("uiText: " + uiText.gameObject.name.Length);
            Debug.Log("array: " + columnName[12].Trim().Length);
            Debug.Log(Array.IndexOf(columnName, uiText.gameObject.name));
            */

            uiText.text = textData[Array.IndexOf(objLabel, uiText.gameObject.name)];
        }
    }
}
