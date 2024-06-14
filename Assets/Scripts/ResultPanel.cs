using System;
using System.Collections;
using System.Text;
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

        string totalScore = ((int)timer + score).ToString();

        //totalScoreTxt.text += totalScore;
        StartCoroutine(StartShowingScenario(totalScore));


        /*
        int duration = totalScore >= 100 ? 2 : 1;

        float lerp = 0;
        int startingScore = 0;

        while (lerp < 1)
        {
            lerp += Time.deltaTime / duration;
            startingScore = (int)Mathf.Lerp(startingScore, totalScore, lerp);
        }
        */

        //Debug.Log(totalScoreTxt.text.Length);

        //totalScore = "987";
        //StartCoroutine(StartShowingScenario(totalScore));

    }


    IEnumerator StartShowingScenario(string sText)
    {
        for (int i = 0; i < 3; i++)
        {
            StartCoroutine(GenerateRandomNumber(sText[i], i + 5));
            yield return new WaitForSecondsRealtime(0.2f);

        }
    }

    IEnumerator GenerateRandomNumber(char finalChar, int index)
    {

        totalScoreTxt.text += finalChar;
        StringBuilder sb;
        for (int j = 0; j < 5; j++)
        {
            sb = new StringBuilder(totalScoreTxt.text);
            sb[index] = Convert.ToChar(UnityEngine.Random.Range(48, 58));
            totalScoreTxt.text = sb.ToString();
            yield return new WaitForSecondsRealtime(0.1f);
        }

        sb = new StringBuilder(totalScoreTxt.text);
        sb[index] = finalChar;
        totalScoreTxt.text = sb.ToString();
    }

}
