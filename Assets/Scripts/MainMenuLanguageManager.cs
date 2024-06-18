using UnityEngine;
using UnityEngine.UI;

public class MainMenuLanguageManager : MonoBehaviour
{
    [SerializeField]
    private TextAsset csvFile; // Assign your CSV file in the Inspector
    private string[] rows;//0: TradChi, 1: SimpChi, 2: ENG


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
        rows = csvFile.text.Split('\n');
    }

    private void ChangeTextByLanguage(int index)//0: TradChi, 1: SimpChi, 2: ENG
    {
        if (index == 2)//ENG not finished yet
        {
            Debug.Log("ENG not finished yet");
            return;
        }

        string[] languageTextArr = rows[index].Split(',');

        //Debug.Log(uiTexts.Length);
        //Debug.Log(languageTextArr.Length);


        for (int i = 0; i < uiTexts.Length; i++)
        {
            uiTexts[i].text = languageTextArr[i];
        }
    }
}
