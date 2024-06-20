using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private InputField inputField;
    private string playerName;
    [SerializeField]
    private Ranking rankingList;
    [SerializeField]
    private Image muteBtn;
    [SerializeField]
    private Sprite[] muteBtnImg;
    [SerializeField]
    private GameObject[] langaugeBtn;

    public Action<int> OnLanguageChanged;


    private void Awake()
    {
        ChangeLangaugeBtnVisual();
        muteBtn.sprite = muteBtnImg[PlayerPrefs.GetInt("Muted")];
    }

    public void OnStartBtnPressed()
    {
        if (!ValiadateInput())
            return;

        PlayerPrefs.SetString("PlayerName", playerName);
        SceneManager.LoadScene(1);
    }

    private bool ValiadateInput()
    {
        playerName = inputField.text.Trim();
        if (playerName == "")
        {
            playerName = "Anonymous";
            return true;
        }
        if (playerName.Length > 8)
            playerName = playerName.Substring(0, 8);


        string[] invalidChar = { "'", "\"", ";", ":", "|", "=", "[", "]", "?", "/", "@" };
        foreach (string c in invalidChar)
        {
            playerName = playerName.Replace(c, "");
        }
        /*
        foreach (char c in playerName)
        {
            if (!char.IsLetterOrDigit(c))
            {
                return false;
            }
        }
        */

        return true;
    }

    public void OnRankingBtnPressed()
    {   //toggle
        rankingList.gameObject.SetActive(!rankingList.gameObject.activeInHierarchy);
        if (rankingList.gameObject.activeInHierarchy)
        {
            //show ranking
            rankingList.ShowRanking();
        }
    }

    public void OnMuteBtnPressed()
    {
        int muted = PlayerPrefs.GetInt("Muted");
        if (muted == 0) //not muted
        {
            //set to mute
            muted = 1;
            Mute();
        }
        else//muted
        {
            //set to  unmute
            muted = 0;
            Unmute();
        }

        PlayerPrefs.SetInt("Muted", muted);
        muteBtn.sprite = muteBtnImg[muted];
    }

    private void Mute()
    {
        AudioListener.volume = 0;
    }

    private void Unmute()
    {
        AudioListener.volume = 1;
    }

    public void OnLangaugeBtnPressed(int index)//0: TradChi, 1: SimpChi, 2: ENG
    {
        PlayerPrefs.SetInt("Language", index);

        ChangeLangaugeBtnVisual();
    }

    private void ChangeLangaugeBtnVisual()
    {
        int index = PlayerPrefs.GetInt("Language");

        foreach (GameObject btn in langaugeBtn)
        {
            btn.GetComponent<Image>().enabled = false;
        }
        langaugeBtn[index].GetComponent<Image>().enabled = true;

        OnLanguageChanged?.Invoke(index);
    }
}
