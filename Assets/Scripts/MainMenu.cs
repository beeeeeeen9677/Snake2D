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

}
