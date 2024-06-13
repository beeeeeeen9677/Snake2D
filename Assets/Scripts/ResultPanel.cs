using System;
using UnityEngine;
using UnityEngine.UI;

public class ResultPanel : MonoBehaviour
{
    [SerializeField]
    private Text playerNameTxt;
    [SerializeField]
    private Text resultTxtScore;
    [SerializeField]
    private Text resultTxtTime;
    [SerializeField]
    private Text totalScoreTxt;

    public void SetResultTxt(int score, float timer)
    {
        TimeSpan time = TimeSpan.FromSeconds(timer);
        string timeStr = time.ToString(@"mm\:ss");
        string playerName = PlayerPrefs.GetString("PlayerName");

        playerNameTxt.text += playerName;
        resultTxtScore.text += score;
        resultTxtTime.text += timeStr;
        totalScoreTxt.text += ((int)timer + score);
    }

}
