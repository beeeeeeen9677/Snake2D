using UnityEngine;
using UnityEngine.UI;

public class MainMenuLanguageManager : MonoBehaviour
{
    [SerializeField]
    private TextAsset mainmenuCsvFile; // Text in different language for main menu
    private string[] mainMenuDataRows;//0: TradChi, 1: SimpChi, 2: ENG
    [SerializeField]
    private TextAsset scenarioCsvFile; // scenario data in different language 
    [SerializeField]
    private DBTSOList[] dbtsoListArr;
    private string[] scenarioColData;
    [SerializeField]
    private MenuDropdown dropdownList;


    [SerializeField]
    private MainMenu mainMenu;
    [SerializeField]
    private Text[] uiTexts;


    private void OnEnable()//get notice when BTN is pressed
    {
        mainMenu.OnLanguageChanged += ChangeTextByLanguage;
    }
    private void OnDestroy()
    {
        mainMenu.OnLanguageChanged -= ChangeTextByLanguage;
    }


    private void Awake()
    {
        mainMenuDataRows = mainmenuCsvFile.text.Split('\n');

        scenarioColData = scenarioCsvFile.text.Trim().Split('\n');
    }

    private void ChangeTextByLanguage(int languageIndex)//0: TradChi, 1: SimpChi, 2: ENG
    {
        /*
        if (languageIndex == 2)//ENG not finished yet
        {
            Debug.Log("ENG not finished yet");
            return;
        }
        */

        string[] mainMenuTextArr = mainMenuDataRows[languageIndex].Split(';');
        string[] scenarioTextArr = new string[scenarioColData.Length - 1]; //for storing the scenario data of one of the column

        //Debug.Log(uiTexts.Length);
        //Debug.Log(mainMenuTextArr.Length);


        for (int i = 0; i < uiTexts.Length; i++)
        {
            uiTexts[i].text = mainMenuTextArr[i];
        }


        for (int i = 1; i < scenarioColData.Length; i++)//skip the row storing column name
        {
            string[] scenarioRowData = scenarioColData[i].Split(';');
            scenarioTextArr[i - 1] = scenarioRowData[languageIndex].Trim();
        }




        for (int i = 0; i < dbtsoListArr.Length; i++)
        {
            dbtsoListArr[i].mood = scenarioTextArr[i * 2];
            dbtsoListArr[i].scenario = scenarioTextArr[i * 2 + 1];
        }
        dropdownList.updateDropdownList(languageIndex);
    }
}
