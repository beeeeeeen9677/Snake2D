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
    private string[] scenarioDataRows;
    [SerializeField]
    private MenuDropdown dropdownList;


    [SerializeField]
    private MainMenu mainMenu;
    [SerializeField]
    private Text[] uiTexts;
    private void OnEnable()
    {
        mainMenu.OnLanagueChanged += ChangeTextByLanguage;
    }
    private void OnDestroy()
    {
        mainMenu.OnLanagueChanged -= ChangeTextByLanguage;
    }


    private void Awake()
    {
        mainMenuDataRows = mainmenuCsvFile.text.Split('\n');
        scenarioDataRows = scenarioCsvFile.text.Split('\n');
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

        string[] mainMenuTextArr = mainMenuDataRows[languageIndex].Split(',');
        string[] scenarioTextArr = scenarioDataRows[languageIndex].Split(',');

        //Debug.Log(uiTexts.Length);
        //Debug.Log(mainMenuTextArr.Length);


        for (int i = 0; i < uiTexts.Length; i++)
        {
            uiTexts[i].text = mainMenuTextArr[i];
        }

        for (int i = 0; i < dbtsoListArr.Length; i++)
        {
            dbtsoListArr[i].mood = scenarioTextArr[i * 2];
            dbtsoListArr[i].scenario = scenarioTextArr[i * 2 + 1];
        }
        dropdownList.updateDropdownList(languageIndex);
    }
}
