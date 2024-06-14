using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RankingData : MonoBehaviour
{
    [SerializeField]
    private Text playerName;
    [SerializeField]
    private TextMeshProUGUI score;
    [SerializeField]
    private TextMeshProUGUI time;
    [SerializeField]
    private TextMeshProUGUI totalScore;


    public void SetData(string name, int score, string time, int totalScore)
    {
        this.playerName.text = name;
        this.score.text = score.ToString();
        this.time.text = time;
        this.totalScore.text = totalScore.ToString();
    }
}
