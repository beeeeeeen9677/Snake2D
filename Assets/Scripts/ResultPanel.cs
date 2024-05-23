using System;
using UnityEngine;

public class ResultPanel : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI resultTxt;

    public void SetResultTxt(int totalScore, float timer)
    {
        TimeSpan time = TimeSpan.FromSeconds(timer);
        string timeStr = time.ToString(@"mm\:ss");
        string playerName = PlayerPrefs.GetString("PlayerName");
        resultTxt.text = "Player: " + playerName + "\nScore: " + totalScore + "\nSurvived: " + timeStr;
    }

}
