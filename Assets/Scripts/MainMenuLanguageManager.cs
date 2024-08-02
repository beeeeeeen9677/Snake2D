using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuLanguageManager : MonoBehaviour // change the UI lanaguage in main menu
{

    private string[] mainMenuDataRows;//0: TradChi, 1: SimpChi, 2: ENG

    [SerializeField]
    private DBTSOList[] dbtsoListArr;
    private string[] scenarioColData;
    [SerializeField]
    private MenuDropdown dropdownList;


    [SerializeField]
    private MainMenu mainMenu;
    [SerializeField]
    private Text[] uiTexts;

    private bool isFinishedInit;


    private void OnEnable()//get notice when any languageBTN is pressed
    {
        mainMenu.OnLanguageChanged += ChangeTextByLanguage;
    }
    private void OnDestroy()
    {
        mainMenu.OnLanguageChanged -= ChangeTextByLanguage;
    }


    private void Awake()
    {
        isFinishedInit = false;
    }


    private void InitData()
    {
        mainMenuDataRows = DataManager.instance.mainMenuText.text.Trim().Split('\n');
        mainMenuDataRows = mainMenuDataRows.Skip(1).ToArray();//skip the row of column name


        scenarioColData = DataManager.instance.scenarioData.text.Trim().Split('\n');

        isFinishedInit = true;
    }


    //change UI language by selected langauge
    private void ChangeTextByLanguage(int languageIndex)//0: TradChi, 1: SimpChi, 2: ENG
    {
        if (!isFinishedInit)
            InitData();



        string[] scenarioTextArr = new string[scenarioColData.Length - 1]; //for storing the scenario data of one of the column

        //Debug.Log(uiTexts.Length);
        //Debug.Log(mainMenuTextArr.Length);


        for (int i = 0; i < uiTexts.Length; i++)
        {
            string[] rowData = mainMenuDataRows[i].Split(';');
            uiTexts[i].text = rowData[languageIndex + 1];//add 1 to skip the first col(Key)
        }


        for (int i = 1; i < scenarioColData.Length; i++)//skip the row storing column name
        {
            string[] scenarioRowData = scenarioColData[i].Split(';');
            scenarioTextArr[i - 1] = scenarioRowData[languageIndex + 1].Trim();//add 1 to skip the first col(Key)
        }

        //set the mood and scenario for DBTSO list
        for (int i = 0; i < dbtsoListArr.Length; i++)
        {
            dbtsoListArr[i].mood = scenarioTextArr[i * 2];
            dbtsoListArr[i].scenario = scenarioTextArr[i * 2 + 1];
        }
        dropdownList.updateDropdownList(languageIndex);
    }
}
