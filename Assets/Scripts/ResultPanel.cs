using System;
using UnityEngine;

public class ResultPanel : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI playerNameTxt;
    [SerializeField]
    private TMPro.TextMeshProUGUI resultTxt;
    [SerializeField]
    private TMPro.TextMeshProUGUI totalScoreTxt;

    public void SetResultTxt(int score, float timer)
    {
        TimeSpan time = TimeSpan.FromSeconds(timer);
        string timeStr = time.ToString(@"mm\:ss");
        string playerName = PlayerPrefs.GetString("PlayerName");

        playerNameTxt.text = "Player: " + playerName;
        resultTxt.text = "Score: " + score + "   Survived: " + timeStr;
        totalScoreTxt.text = "Total Score: " + ((int)timer + score);
    }

}
